﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRent.Domain.Entities
{
    public class Book
    {


        //Create Primary Key , Update Primary Key Update
        [Key]

        //Cancel ok
        public int BookId { get; set; }
        //not isbn
         //isbn
         //ok done
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string? BookUrl { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }       
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;  
        public int TotalPages { get; set; }
        public string BookDimensions { get; set; }  

    }
}
