using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> UpdateAsync(string id, ApplicationUserDto userDto);

        public Task<bool> DeleteAsync(ApplicationUserDto userDto);

        public Task<ApplicationUserDto?> GetByIdAsync(string id);

        public Task<ApplicationUserDto?> GetByNameAsync(string name);

        public Task<ApplicationUserDto?> GetByEmailAsync(string email);

        public IEnumerable<ApplicationUserDto> GetAll();

        public IEnumerable<ApplicationUserDto> GetAllWithFilters(UserQueryFilters filters);

        public IEnumerable<ApplicationUserDto> GetWithInclude(List<string> properties);

        public Task<ApplicationUserDto?> GetByIdWithIncludeAsync(string id, List<string> properties);

        public IEnumerable<ApplicationUserDto> GetWithInclude(UserQueryFilters filters, List<string> properties);
    }
}
