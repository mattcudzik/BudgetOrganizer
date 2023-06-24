using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.RoleModel
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
