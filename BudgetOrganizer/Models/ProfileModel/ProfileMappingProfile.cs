namespace BudgetOrganizer.Models.ProfileModel
{
    public class ProfileMappingProfile : AutoMapper.Profile
    {
        public ProfileMappingProfile() 
        {
            CreateMap<Profile,AddProfileDTO>().ReverseMap();
            CreateMap<Profile,GetProfileDTO>().ReverseMap();
            CreateMap<Profile,UpdateProfileDTO>().ReverseMap();
        }
    }
}
