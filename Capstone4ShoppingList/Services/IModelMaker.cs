using Capstone4ShoppingList.Context;
using Capstone4ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone4ShoppingList.Services
{
    public interface IModelMaker
    {
        public Models.CheckoutModel MakeModel(CapstoneShoppingListDBContext c);
    }
}
