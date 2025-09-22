using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book mappings
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.BookCategories.Select(bc => bc.Category)));
            
            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.BookCategories, opt => opt.Ignore());
            
            CreateMap<UpdateBookDto, Book>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.BookCategories, opt => opt.Ignore());

            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.BookCategories.Select(bc => bc.Book)));
            
            CreateMap<CreateCategoryDto, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.BookCategories, opt => opt.Ignore());
            
            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.BookCategories, opt => opt.Ignore());

            // Book <-> BookSimpleDto
            CreateMap<Book, BookSimpleDto>();

            // Category <-> CategorySimpleDto
            CreateMap<Category, CategorySimpleDto>();
        }
    }
}
