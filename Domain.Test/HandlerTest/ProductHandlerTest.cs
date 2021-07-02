using Domain.Commands;
using Domain.Commands.Product;
using Domain.Entities;
using Domain.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.HandlerTest
{
    [TestClass]
    public class ProductHandlerTest
    {
        private byte[] MockImg()
        {
            var bytes = "imXXmock";
            byte[] _Img = Convert.FromBase64String(bytes);
            return _Img;
        }

        [TestMethod]
        public void Should_be_register_a_product()
        {
            var command = new CreateProductCommand()
            {
                Name = "Product Name",
                Description = "Product Description",
                Price = 199,
                Img = MockImg(),
                Tags = new List<CreateProductTagCommand>()
                {
                    new CreateProductTagCommand() { TagId = 1},
                    new CreateProductTagCommand() { TagId = 2}
                }
            };
            var repository = new RepositoriesFake.ProductRepositoryFake();
            var handler =  new ProductHandler(repository);
            var handlerResult = (CommandResult)handler.Execute(command);

            Assert.IsTrue(handlerResult.IsSuccess && 
                ((Product)handlerResult.Body).Id > 0 &&
                ((Product)handlerResult.Body).ProductTags.Count == 2 &&
                ((Product)handlerResult.Body).Images.Count == 1);
        }

        [TestMethod]
        public void Should_be_update_a_product()
        {
            var command = new UpdateProductCommand()
            {
                ProductId = 1,
                Name = "Product Name",
                Description = "Product Description",
                Price = 199,
                Img = MockImg()
               
            };
            var repository = new RepositoriesFake.ProductRepositoryFake();
            var handler = new ProductHandler(repository);
            var handlerResult = (CommandResult)handler.Execute(command);

            Assert.IsTrue(handlerResult.IsSuccess &&
                ((Product)handlerResult.Body).Id == command.ProductId &&
                ((Product)handlerResult.Body).Name == command.Name &&
                ((Product)handlerResult.Body).Description == command.Description &&
                ((Product)handlerResult.Body).Price == command.Price &&
                ((Product)handlerResult.Body).Images.Count == 1);
        }

        [TestMethod]
        public void Should_be_register_single__product_tag()
        {
            var command = new RegisterProductTagSingleCommand()
            {
                ProductId = 1,
                TagId = 1
            };
            var repository = new RepositoriesFake.ProductRepositoryFake();
            var handler = new ProductHandler(repository);
            var handlerResult = (CommandResult)handler.Execute(command);

            Assert.IsTrue(handlerResult.IsSuccess &&
                ((Product)handlerResult.Body).ProductTags.Count == 3);
        }

        [TestMethod]
        public void Should_be_delete_product_tag()
        {
            var command = new DeleteProductTagCommand()
            {
                ProductId = 1,
                TagId = 1
            };
            var repository = new RepositoriesFake.ProductRepositoryFake();
            var handler = new ProductHandler(repository);
            var handlerResult = (CommandResult)handler.Execute(command);

            Assert.IsTrue(handlerResult.IsSuccess &&
                ((Product)handlerResult.Body).ProductTags.Count == 1);
        }


        [TestMethod]
        public void Should_be_delete_product()
        {
            var command = new DeleteProductCommand()
            {
                ProductId = 1
            };
            var repository = new RepositoriesFake.ProductRepositoryFake();
            var handler = new ProductHandler(repository);
            var handlerResult = (CommandResult)handler.Execute(command);

            Assert.IsTrue(handlerResult.IsSuccess);
        }
    }
}
