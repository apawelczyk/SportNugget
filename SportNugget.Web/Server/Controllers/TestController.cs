using Microsoft.AspNetCore.Mvc;
using SportNugget.Business.Services.Interfaces;
using SportNugget.BusinessModels.Test;
using SportNugget.Common.API;
using SportNugget.Web.Server.Utility.Interfaces;

namespace SportNugget.Web.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly ILogger _logger;
        private readonly IAPIUtility _apiUtility;

        public TestController(ITestService testService, ILogger logger, IAPIUtility apiUtility)
        {
            _testService = testService;
            _logger = logger;
            _apiUtility = apiUtility;
        }

        /// <summary>
        /// Test GET Method
        /// </summary>
        /// 
        //[Authorize(Policy = "PublicSecure")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<TestModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseWrapper<TestModel>))]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWrapper<TestModel>>> Get(int id)
        {
            try
            {
                var serviceResult = await _testService.GetTest(id);
                if (serviceResult != null)
                {
                    return _apiUtility.OkResponse(serviceResult);
                }
                return _apiUtility.StatusCodeResponse(StatusCodes.Status500InternalServerError, "Error retrieving TestModel from API. Service result returned null. Shouldn't be possible.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving TestModel from API.");
                return _apiUtility.StatusCodeResponse(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        /// <summary>
        /// Test GET Method
        /// </summary>
        /// 
        [HttpGet("Blah")]
        public async Task<int> GetBlah(int id)
        {
            return 5;
        }
    }
}
