﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
        private readonly Logging.Interfaces.ILogger _logger;
        private readonly IAPIUtility _apiUtility;

        public TestController(ITestService testService, Logging.Interfaces.ILogger logger, IAPIUtility apiUtility)
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
                //_logger.LogError(e, "Error retrieving TestModel from API.");
                return _apiUtility.StatusCodeResponse(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        /// <summary>
        /// Test GET Method
        /// </summary>
        /// 
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWrapper<IEnumerable<TestModel>>))]
        [HttpGet("")]
        public async Task<ActionResult<ResponseWrapper<IEnumerable<TestModel>>>> GetAll()
        {
            try
            {
                _logger.LogInfo("Test SportNugget.Logger!");
                var serviceResult = await _testService.GetAllTests();
                if (serviceResult != null)
                {
                    return _apiUtility.OkResponse(serviceResult);
                }
                return _apiUtility.StatusCodeResponse(StatusCodes.Status500InternalServerError, "Error retrieving TestModels from API. Service result returned null. Shouldn't be possible.");
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "Error retrieving TestModels from API.");
                return _apiUtility.StatusCodeResponse(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
    }
}
