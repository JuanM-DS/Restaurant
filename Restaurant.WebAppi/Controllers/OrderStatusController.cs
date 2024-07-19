using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.QueryFilters;

namespace Restaurant.WebAppi.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Roles = nameof(RoleTypes.Admin))]
    public class OrderStatusController : BaseApiController
    {
        private readonly IOrderStatusServices _OrderStatusServices;

        public OrderStatusController(IOrderStatusServices OrderStatusServices)
        {
            _OrderStatusServices = OrderStatusServices;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderStatusDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get([FromQuery] OrderStatusQueryFilters filters)
        {
            var OrderStatus = _OrderStatusServices.GetAll(filters);

            if (OrderStatus == null || OrderStatus.Count() == 0)
                return NoContent();

            return Ok(OrderStatus);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderStatusDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(int id)
        {
            var OrderStatus = await _OrderStatusServices.GetByIdAsync(id);

            if (OrderStatus is null)
                return NoContent();

            return Ok(OrderStatus);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderStatusDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(OrderStatusDto OrderStatusDto)
        {
            var result = await _OrderStatusServices.CreateAsync(OrderStatusDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderStatusDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, OrderStatusDto OrderStatusDto)
        {
            await _OrderStatusServices.UpdateAsync(id, OrderStatusDto);
            return Ok(OrderStatusDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _OrderStatusServices.DeleteAsync(id);
            return NoContent();
        }
    }
}
