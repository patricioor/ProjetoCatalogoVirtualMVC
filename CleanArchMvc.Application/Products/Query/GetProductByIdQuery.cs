using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Query
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int? Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
