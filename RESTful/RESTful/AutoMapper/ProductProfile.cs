using AutoMapper;
using RESTful.Dto.Product;
using RESTful.Models;
using RESTful.Repositories;
namespace RESTful.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductResponse>().ReverseMap();
            CreateMap<CreateProduct, Product>();
            CreateMap<UpdateProduct, Product>();
        }
    }
    
}
