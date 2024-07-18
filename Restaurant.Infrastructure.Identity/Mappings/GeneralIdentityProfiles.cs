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
