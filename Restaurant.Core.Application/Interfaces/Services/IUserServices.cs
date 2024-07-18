using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IUserServices
    {
        public Task UpdateAsync(string entityId, ApplicationUserDto entityDto);

        public Task DeleteAsync(string entityId);

        public List<ApplicationUserDto> GetAll();

        public Task<ApplicationUserDto?> GetByIdAsync(string entityId);

        public PagedList<ApplicationUserDto> GetAll(UserQueryFilters filters);

        public PagedList<ApplicationUserDto> GetAllWithInclude(UserQueryFilters filters);

        public List<ApplicationUserDto> GetAllWithInclude();

        public Task<ApplicationUserDto> GetByIdWithIncludeAsync(string id);
    }
}

