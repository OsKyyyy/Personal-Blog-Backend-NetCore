using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.User;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DataBaseContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new DataBaseContext())
            {
                var result = 
                    from operationClaim in context.OperationClaims
                    join userOperationClaim in context.UserOperationClaims
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == user.Id
                    select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }

        public int Add(User user)
        {
            using (var context = new DataBaseContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
                return user.Id;
            }
        }

        public User Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from user in context.Users
                              where user.Id == id
                              select user).FirstOrDefault();                

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();

                return result;
            }
        }

        public User Update(User user)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from u in context.Users
                              where u.Id == user.Id
                              select u).FirstOrDefault();

                if (result != null)
                {
                    result.FirstName = user.FirstName;
                    result.LastName = user.LastName;
                    result.Phone = user.Phone;
                    result.Email = user.Email;
                    result.Status = user.Status;

                    context.SaveChanges();
                }

                return user;
            }
        }

        public UserViewDto ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var userDetailsWithRoles = (from uoc in context.UserOperationClaims
                                            join oc in context.OperationClaims
                                            on uoc.OperationClaimId equals oc.Id
                                            join user in context.Users
                                            on uoc.UserId equals user.Id
                                            where uoc.UserId == id
                                            select new UserViewDto
                                            {
                                                Id = user.Id,
                                                FirstName = user.FirstName,
                                                LastName = user.LastName,
                                                Email = user.Email,
                                                Phone = user.Phone,
                                                Status = user.Status,
                                                RoleName = oc.Name
                                            }).FirstOrDefault();

                return userDetailsWithRoles;
            }
        }

        public bool CheckExistForUpdate(string email, int Id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from user in context.Users
                              where user.Email == email && user.Id != Id
                              select user).Any();
                return result;
            }
        }

        public void AddUserOperationClaim(UserOperationClaim userOperationClaim)
        {
            using (var context = new DataBaseContext())
            {
                context.UserOperationClaims.Add(userOperationClaim);
                context.SaveChanges();              
            }
        }
    }
}
