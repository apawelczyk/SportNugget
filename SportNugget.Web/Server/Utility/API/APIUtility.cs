using Microsoft.AspNetCore.Mvc;
using SportNugget.Web.Server.Utility.Interfaces;

namespace SportNugget.Web.Server.Utility.API
{
    public class APIUtility : ControllerBase, IAPIUtility
    {
        public OkObjectResult OkResponse(object responseObject)
        {
            return Ok(responseObject);
        }

        public ObjectResult StatusCodeResponse(int statusCode, object responseObject)
        {
            return StatusCode(statusCode, responseObject);
        }
    }
}
