using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IGeneralServices<TEntityDto, TEntity>
        where TEntity : BaseEntity
        where TEntityDto : class
    {
        public Task<TEntityDto> CreateAsync(TEntityDto entityDto);

        public Task UpdateAsync(int entityDtoId, TEntityDto entityDto);

        public Task DeleteAsync(int entityDtoId);

        public List<TEntityDto> GetAll();

        public Task<TEntityDto?> GetByIdAsync(int entityDtoId);
    }
}
