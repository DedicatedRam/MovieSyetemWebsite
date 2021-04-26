using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mk4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mk4.ViewComponents
{
    
        public class ShoppingCartViewComponent : ViewComponent
        {
            const string SessionCart = "_Cart";
            private readonly IHttpContextAccessor _contextAccesor;
        public ShoppingCartViewComponent(IHttpContextAccessor newContextAccessor)
        { 
            _contextAccesor = newContextAccessor; 
        }
        

            public async Task<IViewComponentResult> InvokeAsync()
            {
                List<CartItem> cart = new List<CartItem>();
                if (HttpContext.Session.GetString(SessionCart) != null)
                {
                    string serialJSON = HttpContext.Session.GetString(SessionCart);
                    cart = JsonSerializer.Deserialize<List<CartItem>>(serialJSON);
                }
                return View(cart);
            }
        }
    
}
