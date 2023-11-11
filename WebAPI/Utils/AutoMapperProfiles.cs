using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CustomerDTO, Customer>();
            CreateMap<CategoryDTO, Category>();

        }
    }
}
