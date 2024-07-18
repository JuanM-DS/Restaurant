using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IGeneralServices<TEntityDto, TEntity>
        where TEntity : BaseEntity
        where TEntityDto : class
    {
        public Task<TEntityDto> CreateAsync(TEntityDto entityDto);

        public Task UpdateAsync(int entityId, TEntityDto entityDto);

        public Task DeleteAsync(int entityId);

        public List<TEntityDto> GetAll();

        public Task<TEntityDto?> GetByIdAsync(int entityId);
    }
}

