using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Query
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
