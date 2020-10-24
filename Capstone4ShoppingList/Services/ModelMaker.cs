using Capstone4ShoppingList.Context;
using Capstone4ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone4ShoppingList.Services
{
    public class ModelMaker : IModelMaker
    {
        public CheckoutModel MakeModel(CapstoneShoppingListDBContext context)
        {
            var cart = context.ShoppingListDetails;
            var table2 = context.ProductList;

            var checkout = new CheckoutModel();

            var productQuery = (from s in cart
                                join p in table2
                                on s.ProductId equals p.Id
                                select s.Product).ToList();

            checkout.Items = productQuery;
            checkout.Total = (double)productQuery.Sum(_ => _.Price);
            checkout.Tax = Math.Round((checkout.Total * .06), 2, MidpointRounding.AwayFromZero);
            checkout.GrandTotal = checkout.Total + checkout.Tax;
            return checkout;
        }
    }
}
