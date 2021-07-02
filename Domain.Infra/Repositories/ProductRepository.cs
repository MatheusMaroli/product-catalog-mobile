using Domain.Entities;
using Domain.Infra.Contexts;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Infra.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductCatalogContext context) : base(context)
        {
        }   

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int productId)
        {
            return _context.Products.
                Include(product => product.Images).
                Include(product => product.ProductTags).ThenInclude(productTag => productTag.Tag).
                FirstOrDefault(f => f.Id == productId);
        }

        public void Save(Product product)
        {
            base.GenericSave(product);
        }

        public void Update(Product product)
        {
            base.GenericUpdate(product);
        }

        public void Delete(Product product)
        {
            base.GenericDelete(product);
        }
    }
}
