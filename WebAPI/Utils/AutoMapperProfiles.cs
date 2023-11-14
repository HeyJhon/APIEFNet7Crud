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
            CreateMap<ProductDTO, Product>();
            CreateMap<SellerDTO, Seller>();
            CreateMap<SaleDTO, Sale>();
            CreateMap<SaleDetailDTO, SaleDetail>();

        }
    }
}
