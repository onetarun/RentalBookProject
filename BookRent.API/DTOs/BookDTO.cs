using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRent.API.DTOs
{
    public class BookDTO
    {
        public int BookID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string BookImagePath { get; set; }
        public bool Availability { get; set; }
        public double Price { get; set; }
        public int GenreID { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; }
        public int TotalPages { get; set; }
        public string BookDimensions { get; set; }
        //public List<GenreDTO> Genres { get; set; } = new();
    }
}
