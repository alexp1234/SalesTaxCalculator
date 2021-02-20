using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerOnSalesTax.Models
{
    public abstract class Item
    {
        public bool  IsImported { get; set; }
        public string Name { get; set; }
        public decimal ShelfPrice { get; set; }
        public decimal TotalTaxRate { get; set; }
       

    }
}
