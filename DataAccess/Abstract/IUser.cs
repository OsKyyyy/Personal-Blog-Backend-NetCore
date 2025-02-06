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
        UserViewDto ListById(int id);
        bool CheckExistForUpdate(string email, int Id);
        void AddUserOperationClaim(UserOperationClaim userOperationClaim);
    }
}
