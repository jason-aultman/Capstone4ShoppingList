using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Capstone4ShoppingList.Context;
using Capstone4ShoppingList.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography.Xml;
using Capstone4ShoppingList.Services;

namespace Capstone4ShoppingList.Controllers
{
    public class ProductListsController : Controller
    {
        private readonly CapstoneShoppingListDBContext _context;
        private readonly IDBSetup _dbSetup;
        
        public ProductListsController(CapstoneShoppingListDBContext context, IDBSetup setup)
        {
            _context = context;
            _dbSetup = setup;
          //  _dbSetup.createNew(context);

        }

        // GET: ProductLists
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.ProductList.ToListAsync());
        }
       

        
        public IActionResult AddToCart(int Id)
        {
            var product = _context.ProductList.FirstOrDefault(_ => _.Id == Id);
            var shoppingListDetails = new ShoppingListDetails();
          
            shoppingListDetails.ProductId = product.Id;
            shoppingListDetails.Quantity=1;
            shoppingListDetails.Price = product.Price;
            shoppingListDetails.Product = product;

            _context.ShoppingListDetails.Add(shoppingListDetails);
            _context.SaveChanges();
            
            
            return View(product);
        }

        // GET: ProductLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productList = await _context.ProductList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productList == null)
            {
                return NotFound();
            }

            return View(productList);
        }

        // GET: ProductLists/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Checkout()
        {
           var cart = _context.ShoppingListDetails;
           var table2 = _context.ProductList;
            var checkout = new CheckoutModel();
           
            var productQuery = (from s in cart
                         join p in table2
                         on s.ProductId equals p.Id
                         select s.Product).ToList();
            
            checkout.Items = productQuery;
            checkout.Total = (double)productQuery.Sum(_ => _.Price);
            checkout.Tax = Math.Round((checkout.Total * .06), 2, MidpointRounding.AwayFromZero);
            checkout.GrandTotal = checkout.Total + checkout.Tax;
            return View(checkout);
        }

        // POST: ProductLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product,Price")] ProductList productList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productList);
        }

        // GET: ProductLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productList = await _context.ProductList.FindAsync(id);
            if (productList == null)
            {
                return NotFound();
            }
            return View(productList);
        }

        // POST: ProductLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Product,Price")] ProductList productList)
        {
            if (id != productList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductListExists(productList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productList);
        }

        // GET: ProductLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productList = await _context.ProductList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productList == null)
            {
                return NotFound();
            }

            return View(productList);
        }

        // POST: ProductLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productList = await _context.ProductList.FindAsync(id);
            _context.ProductList.Remove(productList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductListExists(int id)
        {
            return _context.ProductList.Any(e => e.Id == id);
        }
    }
}
