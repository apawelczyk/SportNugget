using Microsoft.AspNetCore.Mvc;

namespace SportNugget.Web.Server.Utility.Interfaces
{
    public interface IAPIUtility
    {
        OkObjectResult OkResponse(object responseObject);
        ObjectResult StatusCodeResponse(int statusCode, object responseObject);
    }
}
