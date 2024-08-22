using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class Category : BaseEntity
    {
        // Tên danh mục với độ dài tối đa 255 ký tự
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        // Danh sách sản phẩm thuộc danh mục này, không được serialize trong JSON
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
