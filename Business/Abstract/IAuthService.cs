using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Dtos;
using Entities.Dtos.User;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserLoginDto userForLoginDto);
        IResult UserExists(string email);
        IResult AddUserOperationClaim(UserRoleAddDto userRoleAddDto);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
