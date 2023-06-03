using AutoMapper;
using BudgetOrganizer.Models.AccountModel;

namespace BudgetOrganizer.Models.GroupModel
{
    public class GroupMappingProfile : Profile
    {
        public GroupMappingProfile()
        {
            CreateMap<Account, GetAccountDTO>().ReverseMap();

            CreateMap<Group, GroupDTO>().ReverseMap();
        }
    }
}
