using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone4ShoppingList.Services
{
    public class DBSetup : IDBSetup
    {
        public void createNew(Context.CapstoneShoppingListDBContext context)
        {
            var remove = from removable in context.ShoppingListDetails
                         where removable.Id > 0
                         select removable;
            foreach(var entity in remove)
            {
                context.ShoppingListDetails.Remove(entity);
            }
        }
    }
}
