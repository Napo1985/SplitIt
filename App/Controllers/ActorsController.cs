using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Splitit.App.Controllers
{
    [ApiController]
    [Route("actor")]
    public class ActorsController : Controller
    {
        [HttpGet(Name = "index")]
        public string index()
        {
            return "yaniv";
        }
    }
}

