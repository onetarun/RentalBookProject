﻿using BookRent.Domain.Entities;

namespace BookRent.App.ViewModels
{
    public class VMBookList
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }  
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
        public string GenreName { get; set; } 
    }
    public class VMBook
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string? BookUrl { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
        public int GenreId { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; }
        public int TotalPages { get; set; }
        public string BookDimensions { get; set; } 
        public List<VMGenre> Genres { get; set; } = new();
    }
}
