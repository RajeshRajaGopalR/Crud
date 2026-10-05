using WebApplication_CRUD.Model;
using WebApplication_CRUD.Repository;

namespace WebApplication_CRUD.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(
            IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }

        public async Task<ProductDto> CreateAsync(
            CreateProductDto dto)
        {
            // Business validation
            if (dto.Price < 0)
            {
                throw new ArgumentException(
                    "Price cannot be negative.");
            }

            if (dto.Quantity < 0)
            {
                throw new ArgumentException(
                    "Quantity cannot be negative.");
            }

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Quantity = dto.Quantity
            };

            var createdProduct =
                await _repository.AddAsync(product);

            return new ProductDto
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Price = createdProduct.Price,
                Quantity = createdProduct.Quantity
            };
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateProductDto dto)
        {
            if (dto.Price < 0)
            {
                throw new ArgumentException(
                    "Price cannot be negative.");
            }

            if (dto.Quantity < 0)
            {
                throw new ArgumentException(
                    "Quantity cannot be negative.");
            }

            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return false;
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;

            await _repository.UpdateAsync(product);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return false;
            }

            await _repository.DeleteAsync(product);

            return true;
        }
    }
}
