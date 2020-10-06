using DufflinMunder.Employees;
using System;
using System.Collections.Generic;
using DufflinMunder.Transactions;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;


namespace DufflinMunder
{
    class Program
    {
        //NOTE FROM ANCA: I added this Indent method in order to help me format the report. Also eventually learned I could use /t so will be doing that going forward!
        public static string Indent(int count)
        {
            return "".PadLeft(count);
        }

        static void Main(string[] args)
        {

            var salesTeam = new List<SalesStaff>();
            var accountingTeam = new List<Accountant>();
            var totalSalesTransactions = new List<Sale>();

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
                new Sale( "Dwight Hyte", "Cafe Boeuf", 100, 1500.50m, Cycle.Monthly, 6),
                new Sale("Dwight Hyte","The American Duct Tape Council",  200, 4000.45m, Cycle.Weekly, 3),
                new Sale("Dwight Hyte", "Earl's Academy of Accents", 300, 1200.05m, Cycle.Annually,3),
             };

            employee2.TotalSalesClosed = new List<Sale>
            {
                new Sale( "Tim Halbert", "The Fearmonger's Shop",  400, 1000.00m, Cycle.Weekly, 6),
                new Sale( "Tim Halbert", "Ralph's Pretty Good Grocery", 500, 100.99m, Cycle.Annually, 3),
             };

            employee3.TotalSalesClosed = new List<Sale>
            {
                new Sale("Phyllis Leaf", "The Federation of Associated Organizations", 600, 15000.00m, Cycle.Monthly, 6),
                new Sale("Phyllis Leaf","Mournful Oatmeal", 700, 5000.99m, Cycle.Weekly,3),
                new Sale("Phyllis Leaf","The Professional Organization of English Majors (P.O.E.M.)", 800, 100.99m, Cycle.Annually, 3),
                new Sale("Phyllis Leaf", "Fritz Electronics", 900, 2000.55m, Cycle.Monthly, 3),

             };

            //ANCA: Added current sales to a single collection for all sales - QUESTION: How do we do this on the fly for new sales??? Need a method in the Sale class and call it int he constructor??
            totalSalesTransactions.AddRange(employee1.TotalSalesClosed);
            totalSalesTransactions.AddRange(employee2.TotalSalesClosed);
            totalSalesTransactions.AddRange(employee3.TotalSalesClosed);

            //USER WORKFLOW BEGINS HERE:

            var userSelection = 0;
            string input = null;
            bool validUserSelection = false;


            Console.WriteLine("Welcome to DufflinMunder Cardboard Company Sales Portal.\n");
            do
            {
                Console.WriteLine("\t1.Enter Sales.");
                Console.WriteLine("\t2.Generate Report for Accountant.");
                Console.WriteLine("\t3.Add New Sales Employee.");
                Console.WriteLine("\t4.Find a Sale.");
                Console.WriteLine("\t5.Exit.");

                input = Console.ReadLine();

                validUserSelection = Int32.TryParse(input, out userSelection);

                if (validUserSelection && userSelection < 6)
                {
                    switch (userSelection)
                    {
                        case 1:
                            int salesAgentId = 0;
                            Console.WriteLine("Which sales employee are you? Please enter your employee id:");
                            foreach (var person in salesTeam)
                            {
                                Console.WriteLine(Indent(4)+$"{person.EmployeeId} - {person.FirstName} {person.LastName} ");
                            }
                            var salesAgent = Console.ReadLine();
                            bool validsalesAgent = Int32.TryParse(salesAgent, out salesAgentId);
                            SalesStaff selectedSalesAgent = salesTeam.First(person => person.EmployeeId == salesAgentId);
                            Console.WriteLine($"\tHi {selectedSalesAgent.FirstName}!");

                            Console.WriteLine("\nPlease enter the client business name.");
                            var clientName = Console.ReadLine();

                            Console.WriteLine("Please enter the client Id.");
                            var clientId = int.Parse(Console.ReadLine());

                            Console.WriteLine("Please enter the sale amount.");
                            var saleAmount = decimal.Parse(Console.ReadLine());

                            Console.WriteLine("Please choose a billing cycle:");
                            var values = Enum.GetValues(typeof(Cycle));
                            foreach (var option in values)
                            {
                                Console.WriteLine(option);
                            }
                             var cyclePayment = Console.ReadLine();
                            Cycle cycle = (Cycle)Enum.Parse(typeof(Cycle), cyclePayment);
                             
                            
       
                            Console.WriteLine("Please enter the contract length in months.");
                            var contractLength = int.Parse(Console.ReadLine());

                            selectedSalesAgent.TotalSalesClosed.Add(new Sale(salesAgent,clientName,clientId,saleAmount, cycle, contractLength));
                            Console.WriteLine($"\tCongratulations {selectedSalesAgent.FirstName}, your new sale has been added!\n");

                            totalSalesTransactions.Add(new Sale(salesAgent, clientName, clientId, saleAmount, cycle, contractLength));

                            break;

                        case 2:
                            string accountantSelection;
                            int accountantEmployeeId;
                            bool validAccountantSelection;
                            int salesTeamOrder = 0;
                            int saleTransactionOrder = 0;
                            decimal totalSalesTeamMemberValue = 0.00m;
                            var accountantsIds = new List<int>();
                            bool validAccountantId;

                            Console.WriteLine("Generate Report for Accountant\n");
                            Console.WriteLine("Which accountant would you like to see a report for? (Please type in their employee ID.)\n");

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
                            else Console.WriteLine("Please enter a valid accounting team member's employee ID!\n");

                            break;

                        case 3:
                            Console.WriteLine("Add New Sales Employee.");
                            Console.WriteLine("Please enter new sales employee first name");
                            var firstName = Console.ReadLine();
                            Console.WriteLine("Please enter new sales employee last name");
                            var lastName = Console.ReadLine();
                            Console.WriteLine("Please add a quote");
                            var quote = Console.ReadLine();
                            Console.WriteLine($"First Name: {firstName}");
                            Console.WriteLine($"Last Name: {lastName}");
                            var random = new Random().Next(1000, 9999);
                            Console.WriteLine($"Employee Id: {random}");

                            salesTeam.Add(new SalesStaff(firstName, lastName, random, quote));

                            break;

                        case 4:
                            int salesClientId = 0;
                            bool validId;
                            bool validClientId;
                            Sale selectedSale;

                            Console.WriteLine("Find a Sale\n");
                            Console.WriteLine("Please enter the client ID you would like to review.\n");
                            var userInput = Console.ReadLine();
                            validId = Int32.TryParse(userInput, out salesClientId);
                            validClientId = totalSalesTransactions.Any(item => item.ClientId == salesClientId);

                            if (validId && validClientId)
                            {
                                selectedSale = totalSalesTransactions.First(item => item.ClientId == salesClientId);
                                Console.WriteLine(Indent(4)+$@"Here are the details of the client ID you requested:
CLient ID: {selectedSale.ClientId}
Client Name: {selectedSale.ClientName}
Sales Agent: {selectedSale.SalesAgent}
Sale Amount: {selectedSale.SaleAmount}
Recurring: {selectedSale.Recurring}
Time Frame: {selectedSale.ContractLength} Months
");
                            }
                            else Console.WriteLine("Please enter a valid client ID!\n");

                            break;

                    }
                    Console.WriteLine("Would you like to choose another option?\n");
                }
                else Console.WriteLine("Please enter a valid option!\n");
                continue;
                
            }
            while (userSelection != 5);

        }
    }
}

