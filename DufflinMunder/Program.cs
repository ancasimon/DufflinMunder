using DufflinMunder.Employees;
using System;
using System.Collections.Generic;
using DufflinMunder.Transactions;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.CodeDom.Compiler;

namespace DufflinMunder
{
    class Program
    {
        //NOTE FROM ANCA: I added this Indent method in order to help me format the report. Just fyi.
        public static string Indent(int count)
        {
            return "".PadLeft(count);
        }

        static void Main(string[] args)
        {

            var salesTeam = new List<SalesStaff>();
            var accountingTeam = new List<Accountant>();

            var employee1 = new SalesStaff("Dwight", "Hyte", 3023, "Through concentration, I can raise and lower my cholesterol at will.");
            var employee2 = new SalesStaff("Tim", "Halbert", 3246, "I've always subscribed to the idea that if you really want to impress your boss, you go in there and you do mediocre work, half-heartedly.");
            var employee3 = new SalesStaff("Phyllis", "Leaf", 5342, "This is the first Christmas party I am throwing as head of the party planning committee. The theme is 'Night in Morocco'. This isn't your grandmother's Christmas party. Unless of course, she is from Morocco, in which case it's very accurate.");

            var accountant1 = new Accountant("Angelina", "Kinsey", 2340, "If you pray enough, you can change yourself into a cat person.");
            var accountant2 = new Accountant("Oscar", "Martinez", 4235, "I consider myself a good person, but I'm going to make him cry.");
            var accountant3 = new Accountant("Kevin", "Malone", 3223, "I wanted to eat a pig in a blanket, in a blanket.");

            //there must be a better way to automatically add any accountants to the accounting department?? or sales folks??
            salesTeam.Add(employee1);
            salesTeam.Add(employee2);
            salesTeam.Add(employee3);
            accountingTeam.Add(accountant1);
            accountingTeam.Add(accountant2);
            accountingTeam.Add(accountant3);

            //NOTE FROM ANCA: FYI, Moniq and Beth: I updated these lists to have them get stored in the TotalSalesClosed property on each sales employee!! Previously: 1 - they were just independent lists with no relationship to that property and 2 - that property was called just TotalSale. 
            employee1.TotalSalesClosed = new List<Sale>
            {
                new Sale{ SalesAgent = "Dwight Hyte", ClientName = "Cafe Boeuf", ClientId = 100, SaleAmount = 1500.50m, Recurring = Cycle.Monthly, ContractLength = 6},
                new Sale{ SalesAgent = "Dwight Hyte", ClientName = "The American Duct Tape Council", ClientId = 200, SaleAmount = 4000.45m, Recurring = Cycle.Weekly, ContractLength = 3},
                new Sale{ SalesAgent = "Dwight Hyte", ClientName = "Earl's Academy of Accents", ClientId = 300, SaleAmount = 1200.05m, Recurring = Cycle.Annually, ContractLength = 3},
             };

            employee2.TotalSalesClosed = new List<Sale>

            {
                new Sale{ SalesAgent = "Tim Halbert", ClientName = "The Fearmonger's Shop", ClientId = 400, SaleAmount = 1000.00m, Recurring = Cycle.Weekly, ContractLength = 6},
                new Sale{ SalesAgent = "Tim Halbert", ClientName = "Ralph's Pretty Good Grocery", ClientId = 500, SaleAmount = 100.99m, Recurring = Cycle.Annually, ContractLength = 3},
             };

            employee3.TotalSalesClosed = new List<Sale>

            {
                new Sale{ SalesAgent = "Phyllis Leaf", ClientName = "The Federation of Associated Organizations", ClientId = 600, SaleAmount = 15000.00m, Recurring = Cycle.Monthly, ContractLength = 6},
                new Sale{ SalesAgent = "Phyllis Leaf", ClientName = "Mournful Oatmeal", ClientId = 700, SaleAmount = 5000.99m, Recurring = Cycle.Weekly, ContractLength = 3},
                new Sale{ SalesAgent = "Phyllis Leaf", ClientName = "The Professional Organization of English Majors (P.O.E.M.)", ClientId = 800, SaleAmount = 100.99m, Recurring = Cycle.Annually, ContractLength = 3},
                new Sale{ SalesAgent = "Phyllis Leaf", ClientName = "Fritz Electronics", ClientId = 900, SaleAmount = 2000.55m, Recurring = Cycle.Monthly, ContractLength = 3},

             };



            //USER WORKFLOW BEGINS HERE:
            
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
1.Enter Sales.
2.Generate Report for Accountant.
3.Add New Sales Employee.
4.Find a Sale.
5.Exit.
");
                input = Console.ReadLine();

                validUserSelection = Int32.TryParse(input, out userSelection);

                if (validUserSelection && userSelection < 6)
                {
                    switch (userSelection)
                    {
                        case 1:
                            Console.WriteLine("Enter Sales");
                            showWelcome = false;
                            break;

                        case 2:
                            showWelcome = false;
                            string accountantSelection;
                            int accountantEmployeeId;
                            bool validAccountantSelection;
                            int salesTeamOrder = 0;
                            int saleTransactionOrder = 0;
                            decimal totalSalesTeamMemberValue = 0.00m;
                            var accountantsIds = new List<int>();
                            bool validAccountantId;

                            Console.WriteLine("Generate Report for Accountant");
                            Console.WriteLine("Which accountant would you like to see a report for? (Please type in their employee ID.)");

                            foreach (var person in accountingTeam)
                            {
                                Console.WriteLine($"* {person.EmployeeId} - {person.FirstName} {person.LastName}");
                                accountantsIds.Add(person.EmployeeId);
                            }
                            accountantSelection = Console.ReadLine();
                            validAccountantSelection = Int32.TryParse(accountantSelection, out accountantEmployeeId);
                            validAccountantId = accountantsIds.Contains(accountantEmployeeId);

                            if (validAccountantSelection && validAccountantId)
                            {
                                Console.WriteLine(accountantEmployeeId);

                                Accountant selectedAccountant = accountingTeam.First(person => person.EmployeeId == accountantEmployeeId);

                                Console.WriteLine(Indent(20) + $@"Monthly Sales Report
For: {selectedAccountant.FirstName}
");
                                foreach (var person in salesTeam)
                                {
                                    salesTeamOrder++;
                                    saleTransactionOrder = 0;
                                    totalSalesTeamMemberValue = 0; //Anca: We need to reset these values to 0 every time we start with a new sales person; otherwise, they keep incrementing across the whole sales team.
                                    Console.WriteLine(Indent(4) + $@"{salesTeamOrder}. {person.FirstName.ToUpper()} {person.LastName.ToUpper()}: '{person.Quote}'
        Clients:
");
                                    foreach (var saleTransaction in person.TotalSalesClosed)
                                    {
                                        saleTransactionOrder++;
                                        Console.WriteLine(Indent(10) + $"{saleTransactionOrder}. {saleTransaction.ClientName}\n");
                                        totalSalesTeamMemberValue = totalSalesTeamMemberValue + saleTransaction.SaleAmount;
                                    }

                                    Console.WriteLine(Indent(10) + $"Total: {totalSalesTeamMemberValue:C2}\n");
                                }
                            }
                            else Console.WriteLine("Please enter a valid accounting team member's employee ID!");

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

