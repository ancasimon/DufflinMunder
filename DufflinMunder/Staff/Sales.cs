using DufflinMunder.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DufflinMunder.Employees
{
    class Sales : Employee
    {
        public List<Sale> TotalSale { get; set; }
    }
}
