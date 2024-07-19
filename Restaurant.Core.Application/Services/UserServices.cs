using AutoMapper;
using Microsoft.Extensions.Options;
using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Settings;
using System.Net;

namespace Restaurant.Core.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly PaginationSettings _paginationSettings;

        public UserServices(IUserRepository userRepository, IMapper mapper, IOptions<PaginationSettings> PaginationSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _paginationSettings = PaginationSettings.Value;
        }

        public async Task DeleteAsync(string entityId)
        {
            var userById = await _userRepository.GetByIdAsync(entityId);
            if(userById is null)
                throw new RestaurantException($"There is not any user with this Id: {entityId}", HttpStatusCode.NoContent);

            var result = await _userRepository.DeleteAsync(userById);
            if (result)
                throw new RestaurantException($"There is a error while deleting the user", HttpStatusCode.BadRequest);
        }

        public List<ApplicationUserDto> GetAll()
        {
            var tEntities = _userRepository.GetAll();

            return _mapper.Map<List<ApplicationUserDto>>(tEntities);
        }

        public PagedList<ApplicationUserDto> GetAll(UserQueryFilters filters)
        {
            filters.Page = (filters.Page is null) ? _paginationSettings.DefaultPage : filters.Page;
            filters.PageSize = (filters.PageSize is null) ? _paginationSettings.DefaultPageSize : filters.Page;

            var tEntities = _userRepository.GetAllWithFilters(filters);

            var source =  _mapper.Map<List<ApplicationUserDto>>(tEntities);

            return PagedList<ApplicationUserDto>.Create(source, filters.Page.Value, filters.PageSize.Value);
        }

        public PagedList<ApplicationUserDto> GetAllWithInclude(UserQueryFilters filters)
        {
            filters.Page = (filters.Page is null) ? _paginationSettings.DefaultPage : filters.Page;
            filters.PageSize = (filters.PageSize is null) ? _paginationSettings.DefaultPageSize : filters.Page;

            var tEntities = _userRepository.GetWithInclude(filters, ["Roles"]);

            var source = _mapper.Map<List<ApplicationUserDto>>(tEntities);

            return PagedList<ApplicationUserDto>.Create(source, filters.Page.Value, filters.PageSize.Value);
        }

        public List<ApplicationUserDto> GetAllWithInclude()
        {
            var tEntities = _userRepository.GetWithInclude(["Roles"]);

            return _mapper.Map<List<ApplicationUserDto>>(tEntities);
        }

        public async Task<ApplicationUserDto?> GetByIdAsync(string entityId)
        {
            var tEntity = await _userRepository.GetByIdAsync(entityId);

            return _mapper.Map<ApplicationUserDto>(tEntity);
        }

        public async Task<ApplicationUserDto> GetByIdWithIncludeAsync(string id)
        {
            var dishes = await _userRepository.GetByIdWithIncludeAsync(id, ["Roles"]);
            return _mapper.Map<ApplicationUserDto>(dishes);
        }

        public async Task UpdateAsync(string entityId, ApplicationUserDto entityDto)
        {
            var tEntityById = await _userRepository.GetByIdAsync(entityId);
            if (tEntityById is null)
                throw new RestaurantException($"There is not any user with this Id: {entityId}", HttpStatusCode.NoContent);

            var tEntityByName = await _userRepository.GetByNameAsync(entityDto.UserName);
            if (tEntityByName is null)
                throw new RestaurantException($"There is not any user with this User Name: {entityDto.UserName}", HttpStatusCode.NoContent);

            var tEntityByEmail = await _userRepository.GetByEmailAsync(entityDto.Email);
            if (tEntityByEmail is null)
                throw new RestaurantException($"There is not any user with this Email: {entityDto.Email}", HttpStatusCode.NoContent);

            var result = await _userRepository.UpdateAsync(entityId, entityDto);
            if (result)
                throw new RestaurantException($"There is a error while updating the user", HttpStatusCode.BadRequest);
        }
    }
}
