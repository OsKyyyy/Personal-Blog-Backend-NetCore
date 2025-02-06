using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Business.ValidationRules.FluentValidation.User;
using Core.Aspects.Autofac.Validation;
using Entities.Dtos.User;
using Entities.Dtos;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public User GetByMail(string email, bool status = false)
        {
            if (status)
            {
                return _userDal.Get(u => u.Email == email && u.Status == true);
            }
            return _userDal.Get(u => u.Email == email);
        }
                
        public int Add(User user)
        {
            var result = _userDal.Add(user);

            return result;
        }

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _userDal.Delete(id);
            return new SuccessResult(Messages.UserDeleted);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IDataResult<User> Update(UserUpdateDto userUpdateDto)
        {
            var user = new User
            {
                Id = userUpdateDto.Id,
                Email = userUpdateDto.Email,
                FirstName = userUpdateDto.FirstName,
                LastName = userUpdateDto.LastName,
                Phone = userUpdateDto.Phone,
                Status = userUpdateDto.Status
            };
            _userDal.Update(user);

            return new SuccessDataResult<User>(user, Messages.UserUpdated);
        }        

        public IResult CheckExistsForUpdate(string email, int Id)
        {
            var result = _userDal.CheckExistForUpdate(email, Id);

            if (result)
            {
                return new ErrorResult(Messages.UserNotFound);

            }

            return new SuccessResult(Messages.UserInfoListed);
        }

        public IDataResult<UserViewDto> ListById(int id)
        {
            var result = _userDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<UserViewDto>(Messages.UserNotFound);
            }

            return new SuccessDataResult<UserViewDto>(result, Messages.UserInfoListed);
        }

        public IResult AddUserOperationClaim(UserRoleAddDto userRoleAddDto)
        {
            var userOperationClaim = new UserOperationClaim
            {
                UserId = userRoleAddDto.Id,
                OperationClaimId = userRoleAddDto.OperationClaimId,
            };
            _userDal.AddUserOperationClaim(userOperationClaim);

            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

    }
}
