using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/home")]
    public class HomeController : Controller
    {
       [HttpGet]
       public IActionResult Get()
        {
            return Ok("Hello, from the home controller!");
        }
    }
}