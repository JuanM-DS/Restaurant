using AutoMapper;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Mappings
{
    public class GeneralIdentityProfiles : Profile
    {
        public GeneralIdentityProfiles()
        {
            #region user
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(des => des.Password, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(des => des.Roles, opt => opt.Ignore());

            CreateMap<SaveApplicationUserDto, ApplicationUserDto>()
               .ForMember(des => des.Roles, opt => opt.Ignore())
               .ReverseMap();
            #endregion

            #region role
            CreateMap<ApplicationRole, ApplicationRoleDto>()
                .ReverseMap();
            #endregion

            #region
            #endregion
        }
    }
}
