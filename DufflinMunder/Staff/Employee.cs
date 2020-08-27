using DufflinMunder.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DufflinMunder.Employees
{
    class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDepartment EmployeeDepartment { get; set; }
        public string Quote { get; set; }

       public Employee(string firstName, string lastName, int employeeId, EmployeeDepartment employeeDepartment, string quote)
        {
            FirstName = firstName;
            LastName = lastName;
            EmployeeId = employeeId;
            EmployeeDepartment = employeeDepartment;
            Quote = quote;
        }

    }

    enum EmployeeDepartment
    {
        Accountant,
        Sales,
    }
}
