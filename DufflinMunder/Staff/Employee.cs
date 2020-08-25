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

       /* public Employee(string firstName, string lastName, int employeeId, EmployeeDepartment employeeDepartment)
        {
            FirstName = firstName;
            LastName = lastName;
            EmployeeId = employeeId;
            EmployeeDepartment = employeeDepartment;
        } */
    }

    enum EmployeeDepartment
    {
        Accountant,
        Sales,
    }
}
