using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.QueryFilters;

namespace Restaurant.WebAppi.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Roles = nameof(RoleTypes.Waiter))]
    public class OrderController : BaseApiController
    {
        private readonly IOrderServices _OrderServices;

        public OrderController(IOrderServices OrderServices)
        {
            _OrderServices = OrderServices;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get([FromQuery] OrderQueryFilters filters)
        {
            var Orders = _OrderServices.GetAllWithInclude(filters);

            if (Orders == null || Orders.Count() == 0)
                return NoContent();

            return Ok(Orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(int id)
        {
            var Order = await _OrderServices.GetByIdWithIncludeAsync(id);

            if (Order is null)
                return NoContent();

            return Ok(Order);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(OrderDto OrderDto)
        {
            var result = await _OrderServices.CreateAsync(OrderDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, OrderDto OrderDto)
        {
            await _OrderServices.UpdateAsync(id, OrderDto);
            return Ok(OrderDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _OrderServices.DeleteAsync(id);
            return NoContent();
        }
    }
}
