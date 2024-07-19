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
    public class DishController : BaseApiController
    {
        private readonly IDishServices _DishServices;

        public DishController(IDishServices DishServices)
        {
            _DishServices = DishServices;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get([FromQuery] DishQueryFilters filters)
        {
            var dishes = _DishServices.GetAllWithInclude(filters);

            if (dishes == null || dishes.Count() == 0)
                return NoContent();

            return Ok(dishes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(int id)
        {
            var dish = await _DishServices.GetByIdWithIncludeAsync(id);

            if (dish is null)
                return NoContent();

            return Ok(dish);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DishDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(DishDto dishDto)
        {
            var result = await _DishServices.CreateAsync(dishDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, DishDto dishDto)
        {
            await _DishServices.UpdateAsync(id, dishDto);
            return Ok(dishDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _DishServices.DeleteAsync(id);
            return NoContent();
        }
    }
}
