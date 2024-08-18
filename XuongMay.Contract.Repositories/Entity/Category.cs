using System.ComponentModel.DataAnnotations;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class Category : BaseEntity
    {
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
