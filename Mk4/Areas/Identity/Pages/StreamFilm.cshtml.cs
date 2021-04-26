using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mk4.Data;
using Mk4.Models;

using Microsoft.AspNetCore.Identity;

namespace Mk4.Areas.Identity.Pages
{
    public class StreamFilmModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        
        
        public void OnGet()
        {
        }

        
    }
}
