using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos.User;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        User GetByMail(string email, bool status = false);
        int Add(User user);
        IResult Delete(int id);
        IDataResult<User> Update(UserUpdateDto user);
        IResult CheckExistsForUpdate(string email, int Id);
        IResult CheckExistById(int id);
        IResult AddUserOperationClaim(UserRoleAddDto userRoleAddDto);
    }
}
