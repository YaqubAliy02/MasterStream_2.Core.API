//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Microsoft.AspNetCore.Mvc;

namespace MasterStream_2.Core.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Welcome to MasterStream Project");
        }
    }
}
