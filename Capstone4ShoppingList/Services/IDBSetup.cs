using Capstone4ShoppingList.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone4ShoppingList.Services
{
    public interface IDBSetup
    {
        public void createNew(CapstoneShoppingListDBContext context);
    }
}
