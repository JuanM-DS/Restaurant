using Restaurant.Core.Application.DTOs.Identity.EntityDTOs;
using Restaurant.Core.Application.QueryFilters;

namespace Restaurant.Core.Application.Interfaces.Identity.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> Update(string id, ApplicationUserDTO userDto);

        public Task<bool> Delete(ApplicationUserDTO userDto);

        public Task<ApplicationUserDTO?> GetById(string id);

        public IEnumerable<ApplicationUserDTO> GetAll();

        public IEnumerable<ApplicationUserDTO> GetWithInclude(List<string> properties);

        public Task<ApplicationUserDTO?> GetWithInclude(string id, List<string> properties);

        public IEnumerable<ApplicationUserDTO> GetWithInclude(UserQueryFilters filter, List<string> properties);
    }
}
