using AutoMapper;
using TTS.Dto.Product;
using TTS.Models.Products;
namespace TTS.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<Product,ProductResponsive>().ReverseMap();

            CreateMap<CreateProduct, Product>();


            CreateMap<UpdateProduct, Product>();
        }
        

    }
}
