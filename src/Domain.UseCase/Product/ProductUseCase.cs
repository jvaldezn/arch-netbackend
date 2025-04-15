using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Configuration.Context;
using Data.Dto;
using Data.Messaging.Contract;
using Data.Messaging.Publisher;
using Data.Util;
using Domain.Model;
using Domain.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace Domain.UseCase
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductUseCase> _logger;
        private readonly IEventPublisher _eventPublisher;

        public ProductUseCase(IUnitOfWork<AppDbContext> unitOfWork, IMapper mapper, IProductRepository productRepository, ILogger<ProductUseCase> logger, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productRepository = productRepository;
            _logger = logger;
            _eventPublisher = eventPublisher;
        }

        public async Task<Response<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            if (!productDtos.Any())
                return Response<IEnumerable<ProductDto>>.Success(productDtos, Messages.NoProductsFound);

            return Response<IEnumerable<ProductDto>>.Success(productDtos);
        }

        public async Task<Response<ProductDto>> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null
                ? Response<ProductDto>.Failure(Messages.ProductNotFound)
                : Response<ProductDto>.Success(_mapper.Map<ProductDto>(product));
        }

        public async Task<Response<ProductDto>> CreateProduct(ProductDto dto)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var product = _mapper.Map<Product>(dto);
                await _productRepository.AddAsync(product);
                await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                await _eventPublisher.Publish(new LogCreatedEvent
                {
                    MachineName = "TEST",
                    Logged = DateTime.UtcNow,
                    Level = "INFORMATION",
                    Message = Messages.ProductCreated,
                    Logger = "jvaldez",
                    Properties = $"Product Created with id {product.Id}",
                    Callsite = nameof(ProductUseCase),
                    Exception = null,
                    ApplicationId = 1
                });

                return Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), Messages.ProductCreated);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(string.Format(Messages.UnexpectedError, ex.Message), Messages.ProductCreatedError);
                return Response<ProductDto>.Failure(string.Format(Messages.UnexpectedError, ex.Message));
            }
        }

        public async Task<Response<ProductDto>> UpdateProduct(int id, ProductDto dto)
        {
            if (id != dto.Id)
                return Response<ProductDto>.Failure(Messages.ProductNoMatch);

            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var product = await _productRepository.GetByIdAsync(dto.Id);
                if (product == null)
                    return Response<ProductDto>.Failure(Messages.ProductNotFound);

                _mapper.Map(dto, product);

                _productRepository.Update(product);
                await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                return Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), Messages.ProductUpdated);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(string.Format(Messages.UnexpectedError, ex.Message), string.Format(Messages.ProductUpdatedError, id));
                return Response<ProductDto>.Failure(string.Format(Messages.UnexpectedError, ex.Message));
            }
        }

        public async Task<Response<bool>> DeleteProduct(int id)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    return Response<bool>.Failure(Messages.ProductNotFound);

                _productRepository.Delete(product);
                await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                return Response<bool>.Success(true, Messages.ProductDeleted);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(string.Format(Messages.UnexpectedError, ex.Message), string.Format(Messages.ProductDeletedError, id));
                return Response<bool>.Failure(string.Format(Messages.UnexpectedError, ex.Message));
            }
        }
    }
}
