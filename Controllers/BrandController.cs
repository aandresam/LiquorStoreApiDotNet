using LiquorStoreApi.DTOs;
using LiquorStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreApi.Controllers
{
    [Route("api/brands")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _brandService.GetAll();

            return StatusCode(200, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _brandService.GetById(id);

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BrandDto brandDto)
        {
            var result = await _brandService.Create(brandDto);

            return StatusCode(201, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] BrandDto brandDto)
        {
            var result = await _brandService.Update(id, brandDto);

            return StatusCode(200, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _brandService.DeleteById(id);

            return StatusCode(204, result);
        }
    }
}
