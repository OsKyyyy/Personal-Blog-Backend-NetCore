using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos.User;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        int Add(User user);
        User Delete(int id);
        User Update(User user);
        bool CheckExistForUpdate(string email, int Id);
        bool CheckExistById(int id);
        void AddUserOperationClaim(UserOperationClaim userOperationClaim);
    }
}
