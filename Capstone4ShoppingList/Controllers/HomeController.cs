using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Capstone4ShoppingList.Models;
using Capstone4ShoppingList.Services;
using Capstone4ShoppingList.Context;

namespace Capstone4ShoppingList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       // private readonly IDBSetup _DBSetup;

        public HomeController(ILogger<HomeController> logger, IDBSetup setup)
        {
            _logger = logger;
          //  _DBSetup = setup;
          //  setup.createNew(new CapstoneShoppingListDBContext());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
