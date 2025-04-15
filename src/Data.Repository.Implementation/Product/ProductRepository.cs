using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Configuration.Context;
using Data.Repository.Implementation.GenericRepository;
using Domain.Model;
using Domain.Repository.Interface;

namespace Data.Repository.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
    }
}
