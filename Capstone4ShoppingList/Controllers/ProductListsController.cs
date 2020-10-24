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
        private readonly IModelMaker _modelMaker;
        private readonly IAddsToCart _addsToCart;
        public ProductListsController(CapstoneShoppingListDBContext context, IDBSetup setup, IModelMaker modelMaker, IAddsToCart addsToCart)
        {
            _context = context;
            _dbSetup = setup;
            _modelMaker = modelMaker;
            _addsToCart = addsToCart;
        }

        // GET: ProductLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductList.ToListAsync());
        }
        public IActionResult AddToCart(int Id)
        {
            var product = _context.ProductList.FirstOrDefault(_ => _.Id == Id);
            _addsToCart.AddToCart(_context, Id);
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
            var checkout =_modelMaker.MakeModel(_context);
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
