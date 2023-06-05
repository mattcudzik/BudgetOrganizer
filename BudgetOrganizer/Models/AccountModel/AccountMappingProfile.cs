using BudgetOrganizer.Models.RoleModel;

namespace BudgetOrganizer.Models.AccountModel
{
    //Describes mapping Entity -> DTO and reverse
    public class AccountMappingProfile : AutoMapper.Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Account, UpdateAccountDTO>().ReverseMap();
            CreateMap<Account, AddAccountDTO>().ReverseMap();
            CreateMap<Account, GetAccountDTO>().ReverseMap();

            
        }
    }
}
