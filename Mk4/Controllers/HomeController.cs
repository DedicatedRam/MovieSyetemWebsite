using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mk4.Models;

namespace Mk4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        const string SessionCart = "_Cart";
        
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult RemoveCartItem(IFormCollection form)
        {
            int FilmID = int.Parse(form["FilmID"]);
            var CartList = new List<CartItem>();
            if (HttpContext.Session.GetString(SessionCart) != null)
            {
                string serialJSON = HttpContext.Session.GetString(SessionCart);
                CartList = JsonSerializer.Deserialize<List<CartItem>>(serialJSON);
                var item = CartList.FirstOrDefault(o => o.FilmID == FilmID);
                if (item != null)
                {
                    CartList.Remove(item);
                }

            }

            HttpContext.Session.SetString(SessionCart, JsonSerializer.Serialize(CartList));
            TempData["msg"] = "Item Removed";
            return RedirectToAction("ManageCart");
        }

        [HttpGet]
        public IActionResult ManageCart()

        {
            List<CartItem> cart = new List<CartItem>();
            if (HttpContext.Session.GetString(SessionCart) != null)
            {
                string serialJSON = HttpContext.Session.GetString(SessionCart);
                cart = JsonSerializer.Deserialize<List<CartItem>>(serialJSON);
            }
            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"].ToString();
            }
            return View(cart);

        }

        [HttpPost]
        public IActionResult FilmDetails(IFormCollection form)

        {

            int FilmID = int.Parse(form["FilmID"]);
            string FilmTitle = form["FilmTitle"].ToString();
            decimal FilmPrice = Decimal.Parse(form["FilmPrice"]);
            int OrderQuantity = int.Parse(form["OrderQuantity"]);
            int OrderDuration = int.Parse(form["OrderDuration"]);
            CartItem newOrder = new CartItem();
            newOrder.FilmID = FilmID;
            newOrder.FilmTitle = FilmTitle;
            newOrder.FilmPrice = FilmPrice;
            newOrder.OrderQuantity = OrderQuantity;
            newOrder.OrderDate = DateTime.Now;
            newOrder.DaysDuration = OrderDuration;

            var CartList = new List<CartItem>();
            if (HttpContext.Session.GetString(SessionCart) != null)
            {
                string serialJSON = HttpContext.Session.GetString(SessionCart);
                CartList = JsonSerializer.Deserialize<List<CartItem>>(serialJSON);
                //
                var item = CartList.FirstOrDefault(o => o.FilmID == FilmID);
                if (item != null)
                {
                    item.OrderQuantity += OrderQuantity;
                }
                else
                {
                    CartList.Add(newOrder);
                }

            }
            else
            {
                CartList.Add(newOrder);
            }
            HttpContext.Session.SetString(SessionCart, JsonSerializer.Serialize(CartList));
            return RedirectToAction("Details");

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
