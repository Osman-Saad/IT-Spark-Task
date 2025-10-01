using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpark.DAL.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public InvoiceType Type { get; set; }
        public decimal TotalPrice => Items.Sum(I => I.Total);
        public ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}
