using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IGeneralServices<TEntityDto, TEntity>
        where TEntity : AuditableBaseEntity
        where TEntityDto : class
    {
        public Task<TEntityDto> CreateAsync(TEntityDto entityDto);

        public Task UpdateAsync(TEntityDto entityDto);

        public Task DeleteAsync(TEntityDto entityDto);

        public PagedList<IEnumerable<TEntityDto>> GetAll();

        public Task<TEntityDto> GetByIdAsync(int entityId);
    }
}
