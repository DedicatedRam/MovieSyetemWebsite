using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Mk4.Models
{
    public class Movie
    {
        [Key]

        public int FilmID { get; set; }
        [Required]
        
        [Column(TypeName = "varchar(100)")]


        public string FilmTitle { get; set; }
        [Required]
        [Column(TypeName = "varchar(10)")]


        public string FilmCertificate { get; set; }

        
        


        [Column(TypeName = "text")]
        public string FilmDescription { get; set; }




        [Column(TypeName = "varchar(200)")]
        public string FilmImage { get; set; }



        public float Cost { get; set; }

        [Column(TypeName = "float")]
        public int Rating { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        
        public int RunTimeMins { get; set; }

        public string OMDB_URL { get; set; }

        public string Genres { get; set; }

        public string Director { get; set; }

        public string Actors { get; set; }

        public string Awards { get; set; }

        public int Metascore { get; set; }

        public float IMDB_Rating { get; set; }

        public int IMDB_Votes { get; set; }



    }
}
