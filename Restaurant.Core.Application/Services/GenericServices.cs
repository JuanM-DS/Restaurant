using AutoMapper;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Application.Services
{
    public class GenericServices<TEntityDto, TEntity> : IGeneralServices<TEntityDto, TEntity>
        where TEntity : BaseEntity
        where TEntityDto : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        private readonly IMapper _mapper;

        public GenericServices(IGenericRepository<TEntity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<TEntityDto> CreateAsync(TEntityDto entityDto)
        {
            var tEntity = _mapper.Map<TEntity>(entityDto);

            var result = await _genericRepository.Create(tEntity);
            if (!result)
                throw new Exception($"There is a error while creating the {typeof(TEntity).Name}");

            var newEntityDto = _mapper.Map<TEntityDto>(tEntity);
            return newEntityDto;
        }

        public async Task DeleteAsync(int entityDtoId)
        {
            var tEntityById = await _genericRepository.GetByIdAsync(entityDtoId);
            if (tEntityById is null)
                throw new Exception($"There is not any {typeof(TEntity).Name} with this Id: {entityDtoId}");

            var result = await  _genericRepository.Delete(tEntityById);
            if(result)
                throw new Exception($"There is a error while deleting the {typeof(TEntity).Name}");
        }

        public List<TEntityDto> GetAll()
        {
            var tEntities = _genericRepository.GetAll();

            return _mapper.Map<List<TEntityDto>>(tEntities);
        }

        public async Task<TEntityDto?> GetByIdAsync(int entityDtoId)
        {
            var tEntity = await _genericRepository.GetByIdAsync(entityDtoId);

            return _mapper.Map<TEntityDto>(tEntity);
        }

        public async Task UpdateAsync(int entityDtoId, TEntityDto entityDto)
        {
            var tEntityById = await _genericRepository.GetByIdAsync(entityDtoId);
            if (tEntityById is null)
                throw new Exception($"There is not any {typeof(TEntity).Name} with this Id: {entityDtoId}");

            _mapper.Map(entityDto, tEntityById);
            var result = await _genericRepository.Update(tEntityById);
            if (result)
                throw new Exception($"There is a error while deleting the {typeof(TEntity).Name}");
        }
    }
}
