using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mk4.Models
{
    public class Booking
    {

        [Key]
        [Required]
        public int BookingID { get; set; }
        [Column(TypeName = "varchar(100)")]

        [Required]
        [ForeignKey("FilmID")]
        public int FilmID { get; set; }

        [Required]
        [ForeignKey("UserID")]
        public string UserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        public DateTime startRent { get; set; }
        public DateTime endRent { get; set; }
        
        public int daysDuration { get; set; }

        public int Cost { get; set; }

    
    }
}
