using AutoMapper;
using ProductService.Domain.Entities;
using Sahred.Common.DTOs;

namespace ProductService.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
