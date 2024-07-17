using Restaurant.Core.Application.DTOs.Entities.ApplicationUser;
using Restaurant.Core.Application.QueryFilters;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> Update(string id, ApplicationUserDto userDto);

        public Task<bool> Delete(ApplicationUserDto userDto);

        public Task<ApplicationUserDto?> GetById(string id);

        public IEnumerable<ApplicationUserDto> GetAll();

        public IEnumerable<ApplicationUserDto> GetWithInclude(List<string> properties);

        public Task<ApplicationUserDto?> GetWithInclude(string id, List<string> properties);

        public IEnumerable<ApplicationUserDto> GetWithInclude(UserQueryFilters filter, List<string> properties);
    }
}
