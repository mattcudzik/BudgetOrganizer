using AutoMapper;
using BudgetOrganizer.Models.CategoryModel;

namespace BudgetOrganizer.Models.OperationModel
{
    public class OperationMappingProfile : Profile
    {
        public OperationMappingProfile() 
        {
            CreateMap<Category, GetCategoryDTO>().ReverseMap();
            CreateMap<Operation, GetOperationDTO>().ReverseMap();
            CreateMap<AddOperationDTO, Operation>().ReverseMap();
        }
    }
}
