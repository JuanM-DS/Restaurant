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
                .ForMember(des => des.ConfirmPassword, opt=>opt.Ignore())
                .ForMember(des => des.Password, opt => opt.Ignore())
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
