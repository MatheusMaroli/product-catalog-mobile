using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    
    public class TagController : Controller
    {
        [HttpGet, Route("api/tag")]
        public IActionResult GetAll([FromServices] ITagRepository repository)
        {
            try
            {
                var tags = repository.GetAll();
                return Ok(tags);
            }
            catch(Exception e)
            {
                return BadRequest("Fatal fail on get tags");
            }
        }

        [HttpGet, Route("api/tag/{tagId}/catalog")]
        public IActionResult GetCatalog([FromServices] ITagRepository repository, [FromRoute] int tagId)
        {
            try
            {
                var tags = repository.GetCatalog(tagId);
                return Ok(tags.Select(currentItem => new { Id= currentItem.ProductId, Name= currentItem.Product.Name , Price = currentItem.Product.Price}));
            }
            catch (Exception)
            {
                return BadRequest("Fatal fail on get tags catalog");
            }
        }
    }
}
