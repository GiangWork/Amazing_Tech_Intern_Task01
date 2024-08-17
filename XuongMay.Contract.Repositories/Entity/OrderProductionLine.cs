using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuongMay.Contract.Repositories.Entity
{
    [PrimaryKey(nameof(OrderID), nameof(LineID))]
    public class OrderProductionLine
    {
        public int OrderID { get; set; }
        public int LineID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; } = default!;
        [ForeignKey("LineID")]
        public virtual ProductionLine ProductionLine { get; set; } = default!;
    }
}
