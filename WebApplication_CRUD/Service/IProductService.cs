using WebApplication_CRUD.Model;

namespace WebApplication_CRUD.Service
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();

        Task<ProductDto?> GetByIdAsync(int id);

        Task<ProductDto> CreateAsync(CreateProductDto dto);

        Task<bool> UpdateAsync(
            int id,
            UpdateProductDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
