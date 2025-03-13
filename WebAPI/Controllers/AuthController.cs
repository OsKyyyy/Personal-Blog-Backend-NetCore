using Business.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Dtos;
using Entities.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(UserLoginDto userForLoginDto)
        {           
            var userToLogin = _authService.Login(userForLoginDto);
            
            if (!userToLogin.Status)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Status)
            {
                SuccessDataResult<UserViewDto> dataResult = new SuccessDataResult<UserViewDto>(
                    new UserViewDto
                    {
                        Id = userToLogin.Data.Id,
                        FirstName = userToLogin.Data.FirstName,
                        LastName = userToLogin.Data.LastName,
                        Email = userToLogin.Data.Email,
                        Phone = userToLogin.Data.Phone,
                        Token = result.Data.Token,
                        Expiration = result.Data.Expiration
                    },
                    result.Message
                );                
                return Ok(dataResult);
            }

            return BadRequest(result);
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(UserRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Status)
            {
                return BadRequest(userExists);
            }

            var result = _authService.Register(userForRegisterDto, userForRegisterDto.Password);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            var userRoleAddDto = new UserRoleAddDto
            {
                Id = result.Data.Id,
                OperationClaimId = userForRegisterDto.Role
            };

            var resultRole = _authService.AddUserOperationClaim(userRoleAddDto);

            if (resultRole.Status)
            {
                return Ok(result);
            }

            return Ok(result);
        }
    }
}
