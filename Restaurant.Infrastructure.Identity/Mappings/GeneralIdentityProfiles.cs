using AutoMapper;
using Restaurant.Core.Application.DTOs.Identity.EntityDTOs;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Mappings
{
    public class GeneralIdentityProfiles : Profile
    {
        public GeneralIdentityProfiles()
        {
            #region user
            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ReverseMap();
            #endregion

            #region role
            CreateMap<ApplicationRole, ApplicationRoleDTO>()
                .ReverseMap();
            #endregion

            #region
            #endregion
        }
    }
}
