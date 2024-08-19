using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class Category : BaseEntity
    {
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
