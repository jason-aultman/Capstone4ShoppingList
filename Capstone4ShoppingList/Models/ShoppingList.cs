using System;
using System.Collections.Generic;

namespace Capstone4ShoppingList.Models
{
    public partial class ShoppingList
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? ShoppingListDetailId { get; set; }
        public decimal? Total { get; set; }

        public virtual ShoppingListDetails ShoppingListDetail { get; set; }
    }
}
