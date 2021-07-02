using Domain.Commands.Contracts;
using Domain.Commands.Product;
using Domain.Entities;
using Domain.Handlers.Contracts;
using Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Domain.Handlers
{
    public class ProductHandler : BaseHandler, 
        IHandler<CreateProductCommand>, 
        IHandler<UpdateProductCommand>,
        IHandler<DeleteProductTagCommand>, 
        IHandler<RegisterProductTagSingleCommand>,
        IHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;

        public ProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateProductCommand command)
        {
            try
            {
                var product = new Product()
                {
                    Name = command.Name,
                    Description = command.Description,
                    Price = command.Price,
                    Images = new List<ProductImage>()
                    {
                        new ProductImage() { Img = command.Img }
                    },
                };

                foreach(var currentTag in command.Tags)
                {
                    product.AddTag(currentTag.TagId);
                }

                _repository.Save(product);
                return new Commands.CommandResult(product);
            }
            catch(Exception e)
            {
                return ExceptionResult("ProductHandler.CreateProductCommand", e);
            }
        }

        public ICommandResult Handle(DeleteProductTagCommand command)
        {
            try
            {
                var product = _repository.GetById(command.ProductId);

                if (product == null)
                    return new Commands.CommandResult(Enums.CommandResultStatus.InvalidData, "Nenhum produto foi localizado");

                product.RemoveTag(command.TagId);
                _repository.Update(product);
                return new Commands.CommandResult(product);
            }
            catch (Exception e)
            {
                return ExceptionResult("ProductHandler.DeleteProductTagCommand", e);
            }
        }

        public ICommandResult Handle(RegisterProductTagSingleCommand command)
        {
            try
            {
                var product = _repository.GetById(command.ProductId);

                if (product == null)
                    return new Commands.CommandResult(Enums.CommandResultStatus.InvalidData, "Nenhum produto foi localizado");

                product.AddTag(command.TagId);
                _repository.Update(product);
                return new Commands.CommandResult(product);
            }
            catch (Exception e)
            {
                return ExceptionResult("ProductHandler.RegisterProductTagSingleCommand", e);
            }
        }

        public ICommandResult Handle(UpdateProductCommand command)
        {
            try
            {
                var product = _repository.GetById(command.ProductId);
                if (product == null)
                    return new Commands.CommandResult(Enums.CommandResultStatus.InvalidData, "Nenhum produto foi localizado");

                product.Name = command.Name;
                product.Description = command.Description;
                product.Price = command.Price;
                product.Images.Clear();
                product.Images = new List<ProductImage>()
                {
                    new ProductImage() { Img = command.Img }
                };

                _repository.Update(product);
                return new Commands.CommandResult(product);
            }
            catch (Exception e)
            {
                return ExceptionResult("ProductHandler.UpdateProductCommand", e);
            }
        }

        public ICommandResult Handle(DeleteProductCommand command)
        {
            try
            {
                var product = _repository.GetById(command.ProductId);
                if (product == null)
                    return new Commands.CommandResult(Enums.CommandResultStatus.InvalidData, "Nenhum produto foi localizado");
                

                _repository.Delete(product);
                return new Commands.CommandResult(null);
            }
            catch (Exception e)
            {
                return ExceptionResult("ProductHandler.DeleteProductCommand", e);
            }
        }
    }
}
