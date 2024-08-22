using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.Repositories.Context;
using XuongMay.Repositories.Entity;
using XuongMay.Services;
using XuongMay.Services.Service;
using XuongMayBE.API.AutoMapper;

namespace XuongMayBE.API
{
    public static class DependencyInjection
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigRoute();
            services.AddDatabase(configuration);
            services.AddIdentity();
            services.AddInfrastructure(configuration);
            services.AddServices();
            services.AddAutoMapper();
            services.AddAuthorization();
            services.AddDefaultRole();
            services.AddAdminAccount(configuration);
        }

        private static void ConfigRoute(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("MyCnn")));
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IOrderTaskService, OrderTaskService>()
                .AddScoped<IProductionLineService, ProductionLineService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IRoleService, RoleService>();
        }

        // Đăng ký Automapper
        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);
        }

        // Thêm chức năng xác thực vào swagger
        private static void AddAuthorization(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Xuong May API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        // Tạo role mặc định khi chạy hệ thống
        private static void AddDefaultRole(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            Task.Run(async () =>
            {
                var roles = new[]
                {
                    new ApplicationRole { Name = "Admin", CreatedBy = "System" },
                    new ApplicationRole { Name = "User", CreatedBy = "System" },
                    new ApplicationRole { Name = "Line Manager", CreatedBy = "System" }
                };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name ?? string.Empty))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
            }).GetAwaiter().GetResult();
        }

        // Tạo tài khoản admin khi chạy hệ thống
        private static void AddAdminAccount(this IServiceCollection services, IConfiguration configuration)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            Task.Run(async () =>
            {
                var adminUserName = configuration["AdminSettings:UserName"];
                var adminPassword = configuration["AdminSettings:Password"];
                const string adminRole = "Admin";

                if (!string.IsNullOrWhiteSpace(adminUserName) && !string.IsNullOrWhiteSpace(adminPassword))
                {
                    if (await userManager.FindByNameAsync(adminUserName) == null)
                    {
                        var adminUser = new ApplicationUser
                        {
                            UserName = adminUserName,
                            Password = adminPassword // Xác minh cách lưu trữ mật khẩu, có thể là hashed
                        };

                        var result = await userManager.CreateAsync(adminUser, adminPassword);
                        if (result.Succeeded)
                        {
                            if (!await roleManager.RoleExistsAsync(adminRole))
                            {
                                await roleManager.CreateAsync(new ApplicationRole { Name = adminRole });
                            }

                            await userManager.AddToRoleAsync(adminUser, adminRole);
                        }
                    }
                }
            }).GetAwaiter().GetResult();
        }
    }
}
