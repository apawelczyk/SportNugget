using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportNugget.BusinessModels.Login;
using SportNugget.Common.API;
using SportNugget.Web.Server.Utility.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SportNugget.Web.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Logging.Interfaces.ILogger _logger;
        private readonly IAPIUtility _apiUtility;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(Logging.Interfaces.ILogger logger, IAPIUtility apiUtility, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _apiUtility = apiUtility;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Login POST Method
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<LoginResponseModel>>> Login([FromBody] LoginRequestModel loginModel)
        {
            try
            {
                //var signInResult = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);

                //if (!signInResult.Succeeded)
                //{
                //    return _apiUtility.StatusCodeResponse(StatusCodes.Status401Unauthorized, "Username and password are invalid.");
                //}

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, loginModel.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ServerSecretKey12345"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(Convert.ToInt32(1));

                var token = new JwtSecurityToken(
                    "https://localhost:44363",
                    "SportNugget.Web.API",
                    claims,
                    expires: expiry,
                    signingCredentials: creds
                );

                return _apiUtility.OkResponse(new LoginResponseModel { IsSuccessful = true, BearerToken = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error.");
                return _apiUtility.StatusCodeResponse(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
    }
}
