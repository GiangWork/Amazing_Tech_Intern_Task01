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
        public static void ConfigRoute(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("MyCnn"));
            });
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
            })
             .AddEntityFrameworkStores<DatabaseContext>()
             .AddDefaultTokenProviders();
        }
        public static void AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IOrderTaskService, OrderTaskService>()
                .AddScoped<IProductionLineService, ProductionLineService>()
                .AddScoped<IOrderService, OrderService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);
        }

        public static void AddAuthorization(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Xuong May API", Version = "v1" });

                // Cấu hình Authorization
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: \"Bearer 12345abcdef\""
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
                        new string[] {}
                    }
                });
            });
        }

        public static void AddDefaultRole(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            Task.Run(async () =>
            {
                var roles = new List<ApplicationRole>
        {
            new ApplicationRole
            {
                Name = "Admin",
                CreatedBy = "System"
            },
            new ApplicationRole
            {
                Name = "User",
                CreatedBy = "System"
            },
            new ApplicationRole
            {
                Name = "Line Manager",
                CreatedBy = "System"
            }
        };

                foreach (var role in roles)
                {
                    var roleExist = await roleManager.RoleExistsAsync(role.Name);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
            }).GetAwaiter().GetResult();
        }

        public static void AddAdminAccount(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            Task.Run(async () =>
            {
                // Tạo tài khoản admin nếu chưa tồn tại
                var adminUserName = configuration["AdminSettings:UserName"];
                var adminPassword = configuration["AdminSettings:Password"];
                var adminRole = "Admin";

                if (await userManager.FindByNameAsync(adminUserName) == null)
                {
                    ApplicationUser adminUser = new ApplicationUser
                    {
                        UserName = adminUserName,
                        Password = adminPassword
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
            }).GetAwaiter().GetResult();
        }
    }
}
