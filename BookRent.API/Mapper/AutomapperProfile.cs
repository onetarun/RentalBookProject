using AutoMapper;
using BookRent.API.DTOs;
using BookRent.Domain.Entities;

namespace BookRent.API.Mapper
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<GenreDTO, Genre>();
            CreateMap<Genre, GenreDTO>();

            CreateMap<BookDTO, Book>();
            CreateMap<Book, BookDTO>();
                //.ForMember(dest => dest.Genre, opt => opt.MapFrom(MapToGenreDTO));
        }
        //public List<GenreDTO> MapToGenreDTO(Book book,BookDTO dto)
        //{
        //    List<GenreDTO> dtolist = new List<GenreDTO>();
        //    if(book.Genre != null)
        //    {
        //        foreach (var item in book.Genre.Title)
        //        {
        //            dtolist.Add(new GenreDTO { Title = item });
        //        }

        //    }
        //}
    }
}
