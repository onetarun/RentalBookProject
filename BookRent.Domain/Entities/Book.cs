using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRent.Domain.Entities
{
    public class Book
    {

        //Create Primary Key
        [Key]
        public int BookID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }= DateTime.Now;
        public string BookImagePath { get; set; }
        public bool Availability { get; set; }
        public double Price { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }       
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;  
        public int TotalPages { get; set; }
        public string BookDimensions { get; set; }
        public int isdeleted { get; set; }
        public DateTime DeletedOn { get; set; } = DateTime.Now; 

    }
}
