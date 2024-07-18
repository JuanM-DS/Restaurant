using AutoMapper;
using Microsoft.Extensions.Options;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Domain.Common;
using Restaurant.Core.Domain.Settings;
using System.Net;

namespace Restaurant.Core.Application.Services
{
    public class GenericServices<TEntityDto, TEntity> : IGeneralServices<TEntityDto, TEntity>
        where TEntity : BaseEntity
        where TEntityDto : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        private readonly IMapper _mapper;
        protected readonly PaginationSettings _paginationSettings;

        public GenericServices(IGenericRepository<TEntity> genericRepository, IMapper mapper, IOptions<PaginationSettings> paginationSettings)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _paginationSettings = paginationSettings.Value;
        }

        public virtual async Task<TEntityDto> CreateAsync(TEntityDto entityDto)
        {
            var tEntity = _mapper.Map<TEntity>(entityDto);

            var result = await _genericRepository.CreateAsync(tEntity);
            if (!result)
                throw new RestaurantException($"There is a error while creating the {typeof(TEntity).Name}", HttpStatusCode.BadRequest);

            var newEntityDto = _mapper.Map<TEntityDto>(tEntity);
            return newEntityDto;
        }

        public virtual async Task DeleteAsync(int entityId)
        {
            var tEntityById = await _genericRepository.GetByIdAsync(entityId);
            if (tEntityById is null)
                throw new RestaurantException($"There is not any {typeof(TEntity).Name} with this Id: {entityId}", HttpStatusCode.NoContent);

            var result = await  _genericRepository.DeleteAsync(tEntityById);
            if(result)
                throw new RestaurantException($"There is a error while deleting the {typeof(TEntity).Name}", HttpStatusCode.BadRequest);
        }

        public virtual List<TEntityDto> GetAll()
        {
            var tEntities = _genericRepository.GetAll();

            return _mapper.Map<List<TEntityDto>>(tEntities);
        }

        public virtual async Task<TEntityDto?> GetByIdAsync(int entityId)
        {
            var tEntity = await _genericRepository.GetByIdAsync(entityId);

            return _mapper.Map<TEntityDto>(tEntity);
        }

        public virtual async Task UpdateAsync(int entityId, TEntityDto entityDto)
        {
            var tEntityById = await _genericRepository.GetByIdAsync(entityId);
            if (tEntityById is null)
                throw new RestaurantException($"There is not any {typeof(TEntity).Name} with this Id: {entityId}", HttpStatusCode.NoContent);

            _mapper.Map(entityDto, tEntityById);
            var result = await _genericRepository.UpdateAsync(tEntityById);
            if (result)
                throw new RestaurantException($"There is a error while updating the {typeof(TEntity).Name}", HttpStatusCode.BadRequest);
        }
    }
}
