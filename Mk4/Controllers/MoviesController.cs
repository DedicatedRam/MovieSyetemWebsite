using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mk4.Data;
using Mk4.Models;

namespace Mk4.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        const string SessionCart = "_Cart";

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Movies/Stream

        [Authorize]
        public async Task<IActionResult> Stream(int? id)
        {
            
            

            

            return View();
        }

        // POST: Movies/Stream

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Stream([Bind("BookingID, FilmID,UserID,startRent,endRent,daysDuration,Cost")] Booking stream)
        {
            if (ModelState.IsValid)
            {
                stream.startRent = System.DateTime.Now;
                stream.endRent = System.DateTime.Now.AddDays(stream.daysDuration);
                stream.UserID = HttpContext.Session.GetString("UserEmail");
                _context.Add(stream);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stream);
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        //GET: Search form
        public async Task<IActionResult> SearchForm()
        {
            return View();
        }

        //Post: Show search result
        public async Task<IActionResult> ShowSearchResult(String SearchTitle, String SearchRating)
        {
            if (SearchRating != "No Filter")
            {
                return View("Index", await _context.Movies.Where(j => j.FilmTitle.Contains(SearchTitle) && j.FilmCertificate.Contains(SearchRating)).ToListAsync());
            }
            else
            {
                return View("Index", await _context.Movies.Where(j => j.FilmTitle.Contains(SearchTitle)).ToListAsync());
            }
        }




        // GET: Movies/Details/5
        public async Task<IActionResult> FilmDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.FilmID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        public IActionResult FilmDetails(IFormCollection form)

        {

            int FilmID = int.Parse(form["FilmID"]);
            string FilmTitle = form["FilmTitle"].ToString();
            decimal FilmPrice = Decimal.Parse(form["Cost"]);
            int OrderQuantity = int.Parse(form["OrderQuantity"]);
            CartItem newOrder = new CartItem();
            newOrder.FilmID = FilmID;
            newOrder.FilmTitle = FilmTitle;
            newOrder.FilmPrice = FilmPrice;
            newOrder.OrderQuantity = OrderQuantity;
            newOrder.OrderDate = DateTime.Now;

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
            return RedirectToAction("FilmDetails");

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

        // GET: Movies/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmID,FilmTitle,FilmCertificate,FilmDescription,FilmImage,Cost,Rating,ReleaseDate,RunTimeMins,OMDB_URL,Genres,Director,Actors,Awards,Metascore,IMDB_Rating,IMDB_Votes")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Manager")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmID,FilmTitle,FilmCertificate,FilmDescription,FilmImage,Cost,Rating,ReleaseDate,RunTimeMins,OMDB_URL,Genres,Director,Actors,Awards,Metascore,IMDB_Rating,IMDB_Votes")] Movie movie)
        {
            if (id != movie.FilmID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.FilmID))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.FilmID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.FilmID == id);
        }
    }
}
