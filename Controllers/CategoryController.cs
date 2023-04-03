using LiquorStoreApi.DTOs;
using LiquorStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() 
        {
            var result = await _categoryService.GetAll();

            return StatusCode(200, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _categoryService.GetById(id);

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDto category)
        {
            var result = await _categoryService.Create(category);

            return StatusCode(201, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CategoryDto category)
        {
            var result = await _categoryService.Update(id, category);

            return StatusCode(200, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var result = await _categoryService.DeleteById(id);

            return StatusCode(200, result);
        }
    }
}
