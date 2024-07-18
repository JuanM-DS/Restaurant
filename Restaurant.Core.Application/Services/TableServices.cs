using AutoMapper;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Domain.Entities;
using System.Net;

namespace Restaurant.Core.Application.Services
{
    public class TableServices : GenericServices<TableDto, Table>, ITableServices
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public TableServices(ITableRepository tableRepository, IMapper mapper)
            : base(tableRepository, mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public override async Task DeleteAsync(int entityDtoId)
        {
            var tableStatus = await _tableRepository.GetStatusOfTableById(entityDtoId);
            if (tableStatus != "Available")
                throw new Exceptions.RestaurantException("To delete a table it must be available", HttpStatusCode.BadRequest);

            await base.DeleteAsync(entityDtoId);
        }
    }
}
