using DealerOnSalesTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerOnSalesTax.Factory
{
    public static class ItemFactory
    {
        public static Item Build(string type, bool isImported, string name, decimal shelfPrice)
        {
            switch (type)
            {
                case "salestax":
                    return new SalesTaxItem(isImported, name, shelfPrice);
                case "notax":
                    return new NoSalesTaxItem(isImported, name, shelfPrice);
                default:
                     return new SalesTaxItem(isImported, name, shelfPrice);
            }
        }
    }
}
