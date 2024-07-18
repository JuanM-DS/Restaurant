using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> UpdateAsync(string id, ApplicationUserDto userDto);

        public Task<bool> DeleteAsync(ApplicationUserDto userDto);

        public Task<ApplicationUserDto?> GetByIdAsync(string id);

        public IEnumerable<ApplicationUserDto> GetAll();

        public IEnumerable<ApplicationUserDto> GetWithInclude(List<string> properties);

        public Task<ApplicationUserDto?> GetWithIncludeAsync(string id, List<string> properties);

        public IEnumerable<ApplicationUserDto> GetWithInclude(UserQueryFilters filter, List<string> properties);
    }
}
