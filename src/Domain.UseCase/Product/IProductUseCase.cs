using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dto;
using Data.Util;

namespace Domain.UseCase
{
    public interface IProductUseCase
    {
        Task<Response<IEnumerable<ProductDto>>> GetAllProducts();
        Task<Response<ProductDto>> GetProductById(int id);
        Task<Response<ProductDto>> CreateProduct(ProductDto dto);
        Task<Response<ProductDto>> UpdateProduct(int id, ProductDto dto);
        Task<Response<bool>> DeleteProduct(int id);
    }
}
