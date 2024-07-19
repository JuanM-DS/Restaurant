using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.QueryFilters;

namespace Restaurant.WebAppi.Controllers
{
    [ApiVersion("1.0")]
    public class TableController : BaseApiController
    {
        private readonly ITableServices _tableServices;

        public TableController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TableDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = $"{nameof(RoleTypes.Admin)},{nameof(RoleTypes.Waiter)}")]
        public IActionResult GetAll([FromQuery] TableQueryFilters filters)
        {
            var tables = _tableServices.GetAll(filters);

            if (tables == null || !tables.Any())
                return NoContent();

            return Ok(tables);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = $"{nameof(RoleTypes.Admin)},{nameof(RoleTypes.Waiter)}")]
        public async Task<IActionResult> GetById(int id)
        {
            var table = await _tableServices.GetByIdAsync(id);

            if (table == null)
                return NoContent();

            return Ok(table);
        }

        [HttpGet("{id:int}/orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = nameof(RoleTypes.Waiter))]
        public IActionResult Get(int id)
        {
            var orders = _tableServices.GetTableOrderInProcess(id);

            if (orders == null)
                return NoContent();

            return Ok(orders);
        }

        [HttpPatch("{id:int}/status{tableStatus:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = nameof(RoleTypes.Waiter))]
        public async Task<IActionResult> ChangeStatus(int id, int tableStatus)
        {
            await _tableServices.ChangeStatusAsync(id, tableStatus);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TableDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        public async Task<IActionResult> Create([FromBody] TableDto tableDto)
        {
            if (tableDto == null)
                return BadRequest();

            var result = await _tableServices.CreateAsync(tableDto);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        public async Task<IActionResult> Update(int id, [FromBody] TableDto tableDto)
        {
            await _tableServices.UpdateAsync(id, tableDto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        public async Task<IActionResult> Delete(int id)
        {
            await _tableServices.DeleteAsync(id);

            return NoContent();
        }
    }
}
