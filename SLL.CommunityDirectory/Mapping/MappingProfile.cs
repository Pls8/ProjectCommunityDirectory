using DAL.CommunityDirectory.Models.Event;
using DAL.CommunityDirectory.Models.Resources;
using SLL.CommunityDirectory.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SLL.CommunityDirectory.Mapping
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            //// Map Domain Model -> ReadDTO
            //// We tell it to get the Category Name from the category object
            //CreateMap<ResourceClass, ResourceReadDTO>()
            //    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.category.Name));

            //// Map CreateDTO -> Domain Model
            //CreateMap<ResourceCreateDTO, ResourceClass>();

            //// Events mapping
            //CreateMap<EventClass, EventDTO>()
            //    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));



            // Categories
            CreateMap<CategoryClass, CategoryDTO>().ReverseMap();

            CreateMap<ResourceReadDTO, ResourceCreateDTO>();

            // Resources
            // Resources: Map Database to ReadDTO
            CreateMap<ResourceClass, ResourceReadDTO>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.category.Name))
                .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.CategoryId)); // ADD THIS LINE

            CreateMap<ResourceCreateDTO, ResourceClass>();

            CreateMap<ResourceReadDTO, ResourceCreateDTO>();

            // Events
            CreateMap<EventClass, EventDTO>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ReverseMap();
        }
    }
}
