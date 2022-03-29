using Microsoft.AspNetCore.Mvc;

namespace SportNugget.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        public EchoController()
        {

        }

        /// <summary>
        /// Echo GET Method
        /// </summary>
        [HttpGet("")]
        public async Task<string> Get()
        {
            return "Echo!";
        }
        /// <summary>
        /// Echo POST Method
        /// </summary>
        [HttpPost("{input}")]
        public async Task<string> Post(string input)
        {
            return input;
        }
    }
}
