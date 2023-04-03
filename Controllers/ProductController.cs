using LiquorStoreApi.DTOs;
using LiquorStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiquorStoreApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _productService.GetAll();

            return StatusCode(200, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _productService.GetById(id);

            return StatusCode(200, result);
        }

  
        [HttpPost]
        public async Task<ActionResult> Create(ProductDtoRequest productDto)
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email is null)
                return StatusCode(401, "No autorizado.");

            var result = await _productService.Create(email, productDto);

            return StatusCode(201, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductDtoRequest productDto)
        {
            var result = await _productService.Update(id, productDto);

            return StatusCode(201, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var result = await _productService.DeleteById(id);

            return StatusCode(200, result);
        }
    }
}
