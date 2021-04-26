using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mk4.Models
{
    public class BookingRecord 
    {
        public Booking book { get; set; }
        public Movie film { get; set; }
    }
}
