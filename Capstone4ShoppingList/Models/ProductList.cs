using System;
using System.Collections.Generic;

namespace Capstone4ShoppingList.Models
{
    public partial class ProductList
    {
        public ProductList()
        {
            ShoppingListDetails = new HashSet<ShoppingListDetails>();
        }

        public int Id { get; set; }
        public string Product { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<ShoppingListDetails> ShoppingListDetails { get; set; }
    }
}
