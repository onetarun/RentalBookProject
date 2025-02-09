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

            CreateMap<VMBook, Book>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(MapToGenre));
                //.ForMember(dest => dest.Genre, opt => opt.Ignore());

            CreateMap<Book, VMBook>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(MapToVMGenre))
                .ForMember(dest => dest.BookImage, opt => opt.Ignore());


            CreateMap<Book,VMBookList>()
                .ForMember(dest => dest.GenreName, opt=>opt.MapFrom(opt=> opt.Genre.Title));

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

        public Genre MapToGenre(VMBook vm, Book book)
        {
            Genre genrelist = new Genre();
            if (vm.Genre != null)
            {
                genrelist.GenreID = vm.Genre[0].GenreId;
                genrelist.Title = vm.Genre[0].Title;
            }
            return genrelist;
        }
        //public List<VMGenre> MapToVMGenre(Book book, VMBook vm)
        //{
        //    List<VMGenre> vmlist = new List<VMGenre>
        //    {
        //        new VMGenre { GenreId = vm.Genres[0].GenreId, Title = vm.Genres[0].Title }
        //    }; 
        //    return vmlist;
        //}
        public List<VMGenre> MapToVMGenre(Book book, VMBook vm)
        {
            List<VMGenre> vmlist = new List<VMGenre>();
            
            vmlist.Add( new VMGenre { GenreId=book.GenreId, Title=book.Title });
             
            return vmlist;
        }
    }
}
