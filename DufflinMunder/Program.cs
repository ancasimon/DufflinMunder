using DufflinMunder.Employees;
using System;
using System.Collections.Generic;
using DufflinMunder.Transactions;
using System.Security.Cryptography.X509Certificates;

namespace DufflinMunder
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee1 = new SalesStaff("Dwight", "Hyte", 3023);
            var employee2 = new SalesStaff("Tim", "Halbert", 3246);
            var employee3 = new SalesStaff("Phyllis", "Leaf", 5342);

            var accountant1 = new Accountant("Angelina", "Kinsey", 2340);
            var accountant2 = new Accountant("Oscar", "Martinez", 4235);
            var accountant3 = new Accountant("Kevin", "Malone", 3223);

            var salesDwight = new List<Sale>
            {
                new Sale{ SalesAgent = "Dwight T", ClientName = "ClientA", ClientId = 12345, SaleAmount = 15000, Recurring = Cycle.Monthly, ContractLength = 6},
                new Sale{ SalesAgent = "Dwight T", ClientName = "ClientB", ClientId = 1000, SaleAmount = 4000, Recurring = Cycle.Monthly, ContractLength = 3},
                new Sale{ SalesAgent = "Dwight T", ClientName = "ClientC", ClientId = 345, SaleAmount = 1200, Recurring = Cycle.Annually, ContractLength = 3},
             };

            var salesTim = new List<Sale>
            {
                new Sale{ SalesAgent = "Tim", ClientName = "ClientD", ClientId = 500, SaleAmount = 15000, Recurring = Cycle.Monthly, ContractLength = 6},
                new Sale{ SalesAgent = "Tim", ClientName = "ClientE", ClientId = 600, SaleAmount = 4000, Recurring = Cycle.Monthly, ContractLength = 3},
                new Sale{ SalesAgent = "Tim", ClientName = "ClientF", ClientId = 700, SaleAmount = 1200, Recurring = Cycle.Annually, ContractLength = 3},
             };

            var salesPhyllis = new List<Sale>
            {
                new Sale{ SalesAgent = "Phyllis", ClientName = "ClientG", ClientId = 800, SaleAmount = 15000, Recurring = Cycle.Monthly, ContractLength = 6},
                new Sale{ SalesAgent = "Phyllis", ClientName = "ClientH", ClientId = 900, SaleAmount = 4000, Recurring = Cycle.Monthly, ContractLength = 3},
                new Sale{ SalesAgent = "Phyllis", ClientName = "ClientI", ClientId = 910, SaleAmount = 1200, Recurring = Cycle.Annually, ContractLength = 3},
             };

            
            var userSelection = 0;
            string input = null;
            bool showWelcome = true;
            bool validUserSelection = false;

            do
            {
                if (showWelcome)
                {
                    Console.WriteLine(@"Welcome to DufflinMunder Cardboard Company Sales Portal.");
                }
                Console.WriteLine(@"
1.Enter Sales
2.Generate Report for Accountant.
3.Add New Sales Employee.
4.Find a Sale.
5.Exit.
");
                input = Console.ReadLine();

                validUserSelection = Int32.TryParse(input, out userSelection);

                if (validUserSelection)
                {
                    switch (userSelection)
                    {
                        case 1:
                            Console.WriteLine("Enter Sales");
                            showWelcome = false;
                            break;
                        case 2:
                            Console.WriteLine("Generate Report for Account");
                            showWelcome = false;
                            break;
                        case 3:
                            Console.WriteLine("Add New Sales Employee.");
                            showWelcome = false;
                            break;
                        case 4:
                            Console.WriteLine("Find a Sale.");
                            showWelcome = false;
                            break;
                    }
                    Console.WriteLine("Would you like to choose another option?");
                }
                else Console.WriteLine("Please enter a valid option!");
                continue;
                
            }
            while (userSelection != 5);

        }
    }
}

