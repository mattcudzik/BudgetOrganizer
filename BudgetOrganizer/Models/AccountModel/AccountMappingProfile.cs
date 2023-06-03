using BudgetOrganizer.Models.RoleModel;

namespace BudgetOrganizer.Models.AccountModel
{
    //Describes mapping Entity -> DTO and reverse
    public class AccountMappingProfile : AutoMapper.Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<Account, UpdateAccountDTO>().ReverseMap();
            CreateMap<Account, AddAccountDTO>().ReverseMap();
            CreateMap<Account, GetAccountDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
