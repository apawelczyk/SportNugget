using Microsoft.AspNetCore.Mvc;
using SportNugget.BusinessModels.Session;
using SportNugget.Caching.Interfaces;
using SportNugget.Common.API;
using SportNugget.Web.Server.Utility.Interfaces;

namespace SportNugget.Web.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionStateController : ControllerBase
    {
        private readonly Logging.Interfaces.ILogger _logger;
        private readonly IAPIUtility _apiUtility;
        private readonly ICacheManager _cacheManager;

        public SessionStateController(Logging.Interfaces.ILogger logger, IAPIUtility apiUtility, ICacheManager cacheManager)
        {
            _logger = logger;
            _apiUtility = apiUtility;
            _cacheManager = cacheManager;
        }


        /// <summary>
        /// GET Session cached data
        /// </summary>
        [HttpGet("{key}")]
        public async Task<ActionResult<string>> Get(string key)
        {
            try
            {
                var stringData = await _cacheManager.GetItem(key);
                return _apiUtility.OkResponse(stringData);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving Session State data from API.");
                return _apiUtility.StatusCodeResponse(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        /// <summary>
        /// POST Session cached data
        /// </summary>
        [HttpPost("{key}")]
        public async Task<ActionResult<ResponseWrapper<bool>>> Post(string key, [FromBody] object model)
        {
            try
            {
                if (model == null)
                {
                    return _apiUtility.StatusCodeResponse(StatusCodes.Status500InternalServerError, "Data input is null.");
                }

                var result = await _cacheManager.SetItem(key, model);
                return _apiUtility.OkResponse(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error setting Session State data from API.");
                return _apiUtility.StatusCodeResponse(StatusCodes.Status500InternalServerError, "Error setting data.");
            }
        }
    }
}
