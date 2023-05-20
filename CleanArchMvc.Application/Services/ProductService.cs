using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Query;
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

#pragma warning disable IDE0270 // Usar a expressão de união
            if (productsQuery == null)
                throw new Exception($"Entity could not be loaded.");
#pragma warning restore IDE0270 // Usar a expressão de união

            var result = await _mediator.Send(productsQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }
        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
#pragma warning disable CS8629 // O tipo de valor de nulidade pode ser nulo.
            var productByIdQuery = new GetProductByIdQuery(id.Value);
#pragma warning restore CS8629 // O tipo de valor de nulidade pode ser nulo.

#pragma warning disable IDE0270 // Usar a expressão de união
            if (productByIdQuery == null)
                throw new ApplicationException("Entity could not be loaded");
#pragma warning restore IDE0270 // Usar a expressão de união

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
            await _mediator.Send(productCreateCommand);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);

        }

        public async Task Remove(int? id)
        {
#pragma warning disable CS8629 // O tipo de valor de nulidade pode ser nulo.
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
#pragma warning restore CS8629 // O tipo de valor de nulidade pode ser nulo.

#pragma warning disable IDE0270 // Usar a expressão de união
            if (productRemoveCommand == null)
                throw new Exception("Entity could not be loaded");
#pragma warning restore IDE0270 // Usar a expressão de união

            await _mediator.Send(productRemoveCommand);
        }
    }
}