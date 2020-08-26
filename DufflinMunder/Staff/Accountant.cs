using System;
using System.Collections.Generic;
using System.Text;

namespace DufflinMunder.Employees
{
    class Accountant : Employee 
    {
        public Accountant(string firstName, string lastName, int employeeId, string quote) : base(firstName, lastName, employeeId, EmployeeDepartment.Accountant, quote)
        {

        }
    }
}
