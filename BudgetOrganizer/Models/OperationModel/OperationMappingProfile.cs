using AutoMapper;

namespace BudgetOrganizer.Models.OperationModel
{
    public class OperationMappingProfile : Profile
    {
        public OperationMappingProfile() 
        {
            CreateMap<Operation, GetOperationDTO>().ReverseMap();
            CreateMap<AddOperationDTO, Operation>().ReverseMap();
        }
    }
}
