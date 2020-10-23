using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone4ShoppingList.Models
{
    public class CheckoutModel
    {
        public List<ProductList> Items { get; set; }
        public double Total { get; set; }
        public double Tax { get; set; }

        public double GrandTotal { get; set; }
    }
}
