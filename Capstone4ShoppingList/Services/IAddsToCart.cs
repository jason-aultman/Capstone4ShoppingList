using Capstone4ShoppingList.Context;
using Capstone4ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone4ShoppingList.Services
{
    public interface IAddsToCart
    {
        public ProductList AddToCart(CapstoneShoppingListDBContext context, int id);
    }
}
