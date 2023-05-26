using AutoMapper;

namespace BudgetOrganizer.Models.AccountModel
{
    //Describes mapping Entity -> DTO and reverse
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<Account,AddAccountDTO>().ReverseMap();
            CreateMap<Account,UpdateAccountDTO>().ReverseMap();
            CreateMap<Account,GetAccountDTO>().ReverseMap();
        }
    }
}
