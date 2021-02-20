/*
 * By Alex Pickrell
 * This project is built using a basic factory class contained in the factory folder to create either items with sales tax or without sales tax,
 * both of which inherit from the Item base class
 * The user input is read from the console, parsed using the static Helper class in the helper folder, and the values are passed into the factory to create the objects
 * Once the user is finished entering items and enters 'submit' into the console, the list of items is passed to the Helper class in order to generate the output
 * 
 * 
 * 
 * 
 */

using DealerOnSalesTax.Factory;
using DealerOnSalesTax.Helpers;
using DealerOnSalesTax.Models;
using System;
using System.Collections.Generic;

namespace DealerOnSalesTax
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter each transaction followed by the enter key. Enter the word 'submit' to finish and calculate the total.");
            bool finished = false;
            List<Item> items = new List<Item>();
            while (!finished)
            {
                var userInput = Console.ReadLine();
                if (userInput.ToLower() == "submit")
                {                   
                    Helper.GenerateOutput(items);                   
                    finished = true;
                }
                else
                {
                    var item = ItemFactory.Build(Helper.RetrieveCategory(userInput), Helper.RetrieveIsImported(userInput), Helper.RetriveName(userInput), Helper.RetrieveShelfPrice(userInput));
                    items.Add(item);
                }

            }
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

        }

    }

}
