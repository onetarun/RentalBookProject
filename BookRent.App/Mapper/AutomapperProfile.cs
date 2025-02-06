using AutoMapper;
using BookRent.App.ViewModels;
using BookRent.Domain.Entities;

namespace BookRent.API.Mapper
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<VMGenre, Genre>();
            CreateMap<Genre, VMGenre>();

            CreateMap<VMBook, Book>();
            CreateMap<Book, VMBook>();

            CreateMap<Book,VMBookList>();

            CreateMap<VMBookList,Book>()
            .ForMember(dest => dest.BookDimensions, opt=>opt.Ignore())
            .ForMember(dest => dest.Author, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.BookUrl, opt => opt.Ignore())
            .ForMember(dest => dest.IsAvailable, opt => opt.Ignore())
            .ForMember(dest => dest.Genre, opt => opt.Ignore())
            .ForMember(dest => dest.PublisherName, opt => opt.Ignore())
            .ForMember(dest => dest.PublicationDate, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPages, opt => opt.Ignore());

        }
    }
}
