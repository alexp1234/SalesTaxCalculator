using DealerOnSalesTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerOnSalesTax.Helpers
{
    public static class Helper
    {
        public static bool RetrieveIsImported(string input)
        {
            if (input.ToLower().Contains("imported"))
                return true;
            else
                return false;
        }
        public static string RetrieveCategory(string input)
        {
            if (input.ToLower().Contains("book") || input.ToLower().Contains("chocolate") || input.ToLower().Contains("pills"))
                return "notax";
            else
                return "tax";
        }

        public static string RetriveName(string input)
        {
            string newName = input.Substring(2);         
            for(int i = newName.Length-1; i > 0; i--)
            {
                if (newName[i] == 't' && (newName[i - 1] == 'a' || newName[i-1] == 'A') && newName[i - 2] == ' ')
                {
                    var charToEndCount = newName.Length - (i - 1);

                    newName = newName.Remove(i - 1, charToEndCount);
                    break;
                }                
            }
            StringBuilder sb = new StringBuilder(newName);
            sb.Append(":");
            sb.Replace(" :", ":");
            return sb.ToString();

        }

        public static decimal RetrieveShelfPrice(string input)
        {
            decimal shelfPrice = 0.00m;
            for (int i = input.Length - 1; i > 0; i--)
            {
                if(input[i] == ' ' && input[i-1] == 't')
                {
                    var end = input.Length - i;
                    
                   Decimal.TryParse(input.Substring(i, end), out shelfPrice);
                    return shelfPrice;                
                }
            }
            return shelfPrice;
        }

        private static decimal RoundUpAfterTaxPrice(decimal input)
        {
            var roundedDecimalString = Decimal.Round(input, 2).ToString("F");
            var lastDigit = roundedDecimalString[roundedDecimalString.Length - 1];
            if (Decimal.Round(input, 1) > input)
                return Math.Round(input, 1);
            else if (lastDigit == '3' || lastDigit == '4' || lastDigit == '5' || lastDigit == '6' || lastDigit == '7' || lastDigit == '8' || lastDigit == '9')
            {
                decimal newDecimal;
                var sb = new StringBuilder(roundedDecimalString);
                sb.Remove(sb.Length - 1, 1);
                sb.Append("5");
                if (Decimal.TryParse(sb.ToString(), out newDecimal))
                    return newDecimal;
                else
                    return input;

            }
            else
                return input;
            
        }

        public static void GenerateOutput(List<Item> items)
        {
            // Dictionary to hold occurences of each item by name
            var occurenceDictionary = new Dictionary<string, int>();

            foreach (var item in items)
            {
                if (occurenceDictionary.ContainsKey(item.Name))
                    occurenceDictionary[item.Name] += 1;
                else
                    occurenceDictionary.Add(item.Name, 1);

            }
            decimal finalBill = 0.00m;
            decimal taxTotal = 0.00m;
            foreach(var item in items)
            {
              
                if (occurenceDictionary.ContainsKey(item.Name))
                {
                    // total cost equals the tax multiplier plus one times the shelf price times number of items purchased
                    decimal afterTaxPrice;
                    if (item.TotalTaxRate > 0.00m)
                    {
                        afterTaxPrice = RoundUpAfterTaxPrice(item.ShelfPrice * (1 + item.TotalTaxRate));
                        taxTotal += (afterTaxPrice - item.ShelfPrice) * occurenceDictionary[item.Name];

                    }
                    else
                    {
                        afterTaxPrice = item.ShelfPrice;

                    }

                    var totalCost = afterTaxPrice * occurenceDictionary[item.Name];
                    if (occurenceDictionary[item.Name] > 1)
                    {
                        Console.WriteLine($"{item.Name} {totalCost.ToString("F")} ({occurenceDictionary[item.Name]} @ {item.ShelfPrice.ToString("F")})");
                    }
                    else
                    {
                        Console.WriteLine($"{item.Name} {totalCost.ToString("F")} ");
                    }
                    occurenceDictionary.Remove(item.Name);
                    finalBill += totalCost;
                }
            }
            Console.WriteLine("Sales Tax: " + taxTotal.ToString("F"));
            Console.WriteLine(Environment.NewLine + "Total: " + finalBill.ToString("F"));
        }
        
        
    }
}
