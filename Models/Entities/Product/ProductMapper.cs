using AutoMapper;

namespace csms_backend.Models.Entities
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductRequest, Product>();

            CreateMap<Product, ProductResponse>()
                .ForMember(d => d.Company, o => o.MapFrom(s => s.Company));
        }
    }
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryRequest, Category>();

            CreateMap<Category, CategoryResponse>();
        }
    }
}
