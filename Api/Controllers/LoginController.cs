using Domain.Commands;
using Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost, Route("api/login")]
        public IActionResult GetAll([FromServices] LoginHandler handler , [FromBody] LoginCommand command)
        {
            var handlerResult = (CommandResult)handler.Execute(command);
            if (handlerResult.IsSuccess)
            {
                return Ok(new { Token = "generate token" });
            }
            else
                return BadRequest(handlerResult);
        }
    }
}
