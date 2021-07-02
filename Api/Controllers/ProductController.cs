using Domain.Commands;
using Domain.Commands.Product;
using Domain.Entities;
using Domain.Handlers;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Api.Controllers
{
    public class ProductController : Controller
    {


        [HttpGet, Route("api/product")]
        public IActionResult GetAll([FromServices] IProductRepository repository)
        {
            try
            {
                var products = repository.GetAll();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest("Fatal fail on get product");
            }
        }

        [HttpGet, Route("api/product/{productId}/detail")]
        public IActionResult GetDetail([FromServices] IProductRepository repository, [FromRoute] int productId)
        {
            try
            {
                var product = repository.GetById(productId);
                return Ok(new { 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Img = product.Images != null ? product.Images.FirstOrDefault().Img : null,
                    Tags = product.ProductTags?.Select(currentTag => new {Id = currentTag.TagId, Name = currentTag.Tag.Name})                
                });
            }
            catch (Exception e)
            {
                return BadRequest("Fatal fail on get product");
            }
        }

        [HttpPost, Route("api/product/")]
        public IActionResult Create([FromServices] ProductHandler handler, [FromBody] CreateProductCommand command)
        {
            var handlerResult = (CommandResult)handler.Execute(command);
            if (handlerResult.IsSuccess)
            {
                return Ok(((Product)handlerResult.Body).Id);
            }
            else
                return BadRequest(handlerResult);
        }

        [HttpPost, Route("api/product/create1000")]
        public IActionResult Create1000([FromServices] ProductHandler handler, [FromBody] CreateProductCommand command)
        {            
            for (var i=0; i< 1000; i++)
            {
                var handlerResult = (CommandResult)handler.Execute(command);
                if (! handlerResult.IsSuccess)
                    return BadRequest(handlerResult);
            }
            return Ok();
        }


        [HttpPut, Route("api/product/{productId}")]
        public IActionResult Update([FromServices] ProductHandler handler, [FromRoute] int productId, [FromBody] UpdateProductCommand command)
        {
            command.ProductId = productId;
            var handlerResult = (CommandResult)handler.Execute(command);
            if (handlerResult.IsSuccess)
            {
                return Ok(null);
            }
            else
                return BadRequest(handlerResult);
        }


        [HttpDelete, Route("api/product/{productId}")]
        public IActionResult Delete([FromServices] ProductHandler handler, [FromRoute] int productId)
        {
            var command = new DeleteProductCommand();
            command.ProductId = productId;
            var handlerResult = (CommandResult)handler.Execute(command);
            if (handlerResult.IsSuccess)
            {
                return Ok(null);
            }
            else
                return BadRequest(handlerResult);
        }

        [HttpPost, Route("api/product/{productId}/tag")]
        public IActionResult CreateTag([FromServices] ProductHandler handler, [FromRoute] int productId, [FromBody] RegisterProductTagSingleCommand command)
        {
            command.ProductId = productId;
            var handlerResult = (CommandResult)handler.Execute(command);
            if (handlerResult.IsSuccess)
            {
                return Ok(null);
            }
            else
                return BadRequest(handlerResult);
        }


        [HttpDelete, Route("api/product/{productId}/tag/{tagId}")]
        public IActionResult DeleteTag([FromServices] ProductHandler handler, [FromRoute] int productId, [FromRoute]int tagId)
        {
            var command = new DeleteProductTagCommand();
            command.ProductId = productId;
            command.TagId = tagId;
            var handlerResult = (CommandResult)handler.Execute(command);
            if (handlerResult.IsSuccess)
            {
                return Ok(null);
            }
            else
                return BadRequest(handlerResult);
        }


    }
}
