using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repository.Interface;

namespace Domain.Repository.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // Métodos adicionales específicos para productos (si son necesarios)
    }
}
