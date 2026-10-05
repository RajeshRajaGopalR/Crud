using Microsoft.AspNetCore.Mvc;
using WebApplication_CRUD.Model;
using WebApplication_CRUD.Service;

namespace WebApplication_CRUD.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class ProductsController : ControllerBase
        {
            private readonly IProductService _service;

            public ProductsController(
                IProductService service)
            {
                _service = service;
            }

            // GET: api/products
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var products = await _service.GetAllAsync();

                return Ok(products);
            }

            // GET: api/products/1
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var product = await _service.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound(new
                    {
                        message = "Product not found."
                    });
                }

                return Ok(product);
            }

            // POST: api/products
            [HttpPost]
            public async Task<IActionResult> Create(
                CreateProductDto dto)
            {
                var product = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = product.Id },
                    product);
            }

            // PUT: api/products/1
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(
                int id,
                UpdateProductDto dto)
            {
                var result =
                    await _service.UpdateAsync(id, dto);

                if (!result)
                {
                    return NotFound(new
                    {
                        message = "Product not found."
                    });
                }

                return NoContent();
            }

            // DELETE: api/products/1
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var result =
                    await _service.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(new
                    {
                        message = "Product not found."
                    });
                }

                return NoContent();
            }
        }
    }

