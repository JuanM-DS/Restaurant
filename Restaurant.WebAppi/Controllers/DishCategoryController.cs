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
    public class DishCategoryController : BaseApiController
    {
        private readonly IDishCategoryServices _DishCategoryServices;

        public DishCategoryController(IDishCategoryServices DishCategoryServices)
        {
            _DishCategoryServices = DishCategoryServices;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishCategoryDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get([FromQuery] DishCategoryQueryFilters filters)
        {
            var dishCategories = _DishCategoryServices.GetAll(filters);

            if (dishCategories == null || dishCategories.Count() == 0)
                return NoContent();

            return Ok(dishCategories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishCategoryDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(int id)
        {
            var dishCategory = await _DishCategoryServices.GetByIdAsync(id);

            if (dishCategory is null)
                return NoContent();

            return Ok(dishCategory);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DishCategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(DishCategoryDto dishDto)
        {
            var result = await _DishCategoryServices.CreateAsync(dishDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishCategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, DishCategoryDto dishDto)
        {
            await _DishCategoryServices.UpdateAsync(id, dishDto);
            return Ok(dishDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _DishCategoryServices.DeleteAsync(id);
            return NoContent();
        }
    }
}
