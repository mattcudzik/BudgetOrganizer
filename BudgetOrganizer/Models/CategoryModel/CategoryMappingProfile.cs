using AutoMapper;

namespace BudgetOrganizer.Models.CategoryModel
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() 
        {
            CreateMap<Category,AddCategoryDTO>().ReverseMap();
            CreateMap<Category,GetCategoryDTO>().ReverseMap();
        }
    }
}
