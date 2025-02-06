using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Domain.Entities;

namespace BookRent.API.DTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string BookUrl { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
        public int GenreId { get; set; }
        public GenreDTO Genre { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; }
        public int TotalPages { get; set; }
        public string BookDimensions { get; set; } 
    }
}
