using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
#pragma warning disable IDE0044 // Adicionar modificador somente leitura
        ApplicationDbContext _productContext;
#pragma warning restore IDE0044 // Adicionar modificador somente leitura

        public ProductRepository(ApplicationDbContext context)
        {
            _productContext = context;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            //return await _productContext.Products.FindAsync(id);
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return await _productContext.Products.Include(c => c.Category)
                                     .SingleOrDefaultAsync(p => p.Id == id);
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }

        //public async Task<Product> GetProductCategoryAsync(int? id)
        //{
        //    return await _productContext.Products.Include(c => c.Category)
        //                                         .SingleOrDefaultAsync(p => p.Id == id);
        //}

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _productContext.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> RemoveAsync(Product product)
        {
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }


    }
}
