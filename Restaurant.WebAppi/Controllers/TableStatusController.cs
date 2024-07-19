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
    public class TableStatusController : BaseApiController
    {
        private readonly ITableStatusServices _TableStatusServices;

        public TableStatusController(ITableStatusServices TableStatusServices)
        {
            _TableStatusServices = TableStatusServices;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableStatusDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get([FromQuery] TableStatusQueryFilters filters)
        {
            var tableStatus = _TableStatusServices.GetAll(filters);

            if (tableStatus == null || tableStatus.Count() == 0)
                return NoContent();

            return Ok(tableStatus);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableStatusDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(int id)
        {
            var tableStatus = await _TableStatusServices.GetByIdAsync(id);

            if (tableStatus is null)
                return NoContent();

            return Ok(tableStatus);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TableStatusDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(TableStatusDto TableStatusDto)
        {
            var result = await _TableStatusServices.CreateAsync(TableStatusDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, TableStatusDto TableStatusDto)
        {
            await _TableStatusServices.UpdateAsync(id, TableStatusDto);
            return Ok(TableStatusDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _TableStatusServices.DeleteAsync(id);
            return NoContent();
        }
    }
}
