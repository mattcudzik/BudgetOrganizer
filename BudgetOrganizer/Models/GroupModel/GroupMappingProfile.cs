using AutoMapper;
using BudgetOrganizer.Models.AccountModel;
using BudgetOrganizer.Models.RoleModel;

namespace BudgetOrganizer.Models.GroupModel
{
    public class GroupMappingProfile : Profile
    {
        public GroupMappingProfile()
        {
            CreateMap<Account, GetGroupAccountDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<Group, GroupDTO>().ReverseMap();

        }
    }
}
