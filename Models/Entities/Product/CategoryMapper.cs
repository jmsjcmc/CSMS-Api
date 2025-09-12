using AutoMapper;

namespace csms_backend.Models.Entities
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryRequest, Category>();

            CreateMap<Category, CategoryResponse>();
        }
    }
}
