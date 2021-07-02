using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;

namespace Domain.Test.RepositoriesFake
{
    public class ProductRepositoryFake : IProductRepository
    {
        public void Delete(Product product)
        {
            //
        }

        public IEnumerable<Product> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Product GetById(int productId)
        {
            return new Product() { Id = productId ,      
                Name = "Product Name",
                Description = "Product Description",
                Price = 199,
                Images = new List<ProductImage>(),
                ProductTags = new List<ProductTags>()
                {
                    new ProductTags() { TagId = 1},
                    new ProductTags() { TagId = 2}
                }
            };
        }

        public void Save(Product product)
        {
            product.Id = 1;
        }

        public void Update(Product product)
        {
            //
        }
    }
}
