using DufflinMunder.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DufflinMunder.Employees
{
    class SalesStaff : Employee
    {
        public List<Sale> TotalSalesClosed { get; set; }

        public SalesStaff(string firstName, string lastName, int employeeId, string quote) : base(firstName, lastName, employeeId, EmployeeDepartment.Sales, quote)
        { 
            
        }
    }
}
