using System;
using System.Collections.Generic;
using System.Text;

namespace DufflinMunder.Transactions
{
    class Sale
    {
        public string SalesAgent { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }
        public decimal SaleAmount { get; set; }
        public Cycle Recurring { get; set; }
        public int ContractLength { get; set; }



    }
   
}
