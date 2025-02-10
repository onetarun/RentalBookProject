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

            CreateMap<BookDTO, Book>()
                 .ForMember(dest => dest.Genre, opt => opt.Ignore());
            //.ForMember(dest => dest.Genre, opt => opt.MapFrom(MapToGenre));


            CreateMap<Book, BookDTO>()
                .ForMember(dest=>dest.Genre,opt=>opt.MapFrom(MapToGenreDTO));

            CreateMap<Book, BookListDTO>()
                            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(opt => opt.Genre.Title));

            CreateMap<BookListDTO, Book>()
            .ForMember(dest => dest.BookDimensions, opt => opt.Ignore())
            .ForMember(dest => dest.Author, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.BookUrl, opt => opt.Ignore())
            .ForMember(dest => dest.IsAvailable, opt => opt.Ignore())
            .ForMember(dest => dest.Genre, opt => opt.Ignore())
            .ForMember(dest => dest.PublisherName, opt => opt.Ignore())
            .ForMember(dest => dest.PublicationDate, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPages, opt => opt.Ignore());

            //.ForMember(dest => dest.Genre, opt => opt.MapFrom(MapToGenreDTO));
        }
        public List<GenreDTO> MapToGenreDTO(Book book, BookDTO dto)
        {
            List<GenreDTO> vmlist = new List<GenreDTO>();

            vmlist.Add(new GenreDTO { GenreID = book.GenreId, Title = book.Title });

            return vmlist;
        }
        //public GenreDTO MapToGenreDTO(Book book, BookDTO dto)
        //{

        //    GenreDTO dtolist = new GenreDTO();
        //    dtolist.GenreID = book.Genre.GenreID;
        //    dtolist.Title = book.Genre.Title;

        //    return dtolist;

        //}

        public Genre MapToGenre(BookDTO dto, Book book)
        {
            Genre genrelist = new Genre();
            if (dto.Genre != null)
            {
                genrelist.GenreID = dto.Genre[0].GenreID;
                genrelist.Title = dto.Genre[0].Title;
            }
            return genrelist;
        }
    }
}
