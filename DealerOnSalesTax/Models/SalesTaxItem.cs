using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerOnSalesTax.Models
{
    public class SalesTaxItem : Item
    {
        private const decimal SALES_TAX_RATE = 0.10m;
        private const decimal IMPORT_TAX_RATE = 0.05m;

        public SalesTaxItem(bool isImported, string name, decimal shelfPrice)
        {
            this.IsImported = isImported;
            this.Name = name;
            this.ShelfPrice = shelfPrice;
            this.TotalTaxRate = this.IsImported ? SALES_TAX_RATE + IMPORT_TAX_RATE: SALES_TAX_RATE;
        }
    }
}
