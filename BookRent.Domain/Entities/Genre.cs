using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRent.Domain.Entities
{
    public class Genre
    {

        public int GenreID { get; set; }
        public string GenreCategory { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        //commented by Rajendra Ram...test
    }
}
