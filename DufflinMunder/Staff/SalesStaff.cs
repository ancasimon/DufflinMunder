using DufflinMunder.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DufflinMunder.Employees
{
    class SalesStaff : Employee
    {
        public List<Sale> TotalSale { get; set; }

        public SalesStaff(string firstName, string lastName, int employeeId) : base(firstName, lastName, employeeId, EmployeeDepartment.Sales)
        { 
            
        }
    }
}
