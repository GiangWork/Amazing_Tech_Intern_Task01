using System.ComponentModel.DataAnnotations.Schema;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; } = string.Empty;

        // Foreign key
        public string CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
