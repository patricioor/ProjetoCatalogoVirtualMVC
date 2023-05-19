using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Query;
using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        //IProductRepository _productRepository;
        readonly IMapper _mapper;
        readonly IMediator _mediator;

        public ProductService(/*IProductRepository productRepository*/IMediator mediator, IMapper mapper)
        {
            //_productRepository = productRepository ??
            //    throw new ArgumentNullException(nameof(productRepository));
            _mediator = mediator;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productsQuery = new GetProductsQuery();

            if (productsQuery == null)
                throw new Exception($"Entity could not be loaded.");
            
            var result = await _mediator.Send(productsQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);

        }
        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new ApplicationException("Entity could not be loaded");

            var result = await _mediator.Send(productByIdQuery);
            return _mapper.Map<ProductDTO>(result);
        }

        //public async Task<ProductDTO> GetProductCategoryAsync(int? id)
        //{
        //    var productQuery = new GetProductByIdQuery(id.Value);

        //    if (productQuery == null)
        //        throw new ApplicationException("Entity could not be loaded");

        //    var result = await _mediator.Send(productQuery);
        //    return _mapper.Map<ProductDTO>(result);
        //}

        public async Task Add(ProductDTO productDTO)
        {

            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            var result = await _mediator.Send(productCreateCommand);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            var result = await _mediator.Send(productUpdateCommand);

        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);

            if (productRemoveCommand == null)
                throw new ApplicationException("Entity could not be loaded");

            var result = await _mediator.Send(productRemoveCommand);
        }
    }
}