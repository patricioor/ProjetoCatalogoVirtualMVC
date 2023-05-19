using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();

        Task<ProductDTO> GetByIdAsync(int? id);

        //Task<ProductDTO> GetProductCategoryAsync(int? id);

        Task Add(ProductDTO productDTO);

        Task Update(ProductDTO productDTO);

        Task Remove(int? id);
    }
}
