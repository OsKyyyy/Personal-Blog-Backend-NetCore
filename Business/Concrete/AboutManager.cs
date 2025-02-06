using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Aspects.Autofac.Validation;
using Entities.Dtos.About;
using Business.ValidationRules.FluentValidation.About;

namespace Business.Concrete
{
    public class AboutManager : IAboutService
    {
        IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }
         
        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(AboutAddDto aboutAddDto)
        {
            var about = new About
            {
                Title = aboutAddDto.Title,
                Content = aboutAddDto.Content,
                Name = aboutAddDto.Name,
                DateOfBirth = aboutAddDto.DateOfBirth,
                Address = aboutAddDto.Address,
                Email = aboutAddDto.Email,
                Phone = aboutAddDto.Phone,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = aboutAddDto.CreateUserId,
                UpdateUserId = aboutAddDto.CreateUserId,
                Deleted = false
            };

            _aboutDal.Add(about);

            return new SuccessResult(Messages.AboutAdded);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(AboutUpdateDto aboutUpdateDto)
        {
            var about = new About
            {
                Id = aboutUpdateDto.Id,
                Title = aboutUpdateDto.Title,
                Content = aboutUpdateDto.Content,
                Name = aboutUpdateDto.Name,
                DateOfBirth = aboutUpdateDto.DateOfBirth,
                Address = aboutUpdateDto.Address,
                Email = aboutUpdateDto.Email,
                Phone = aboutUpdateDto.Phone,
                UpdateDate = DateTime.Now,
                UpdateUserId = aboutUpdateDto.UpdateUserId
            };
            _aboutDal.Update(about);

            return new SuccessResult(Messages.AboutUpdated);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _aboutDal.Delete(id);
            return new SuccessResult(Messages.AboutDeleted);
        }

        [SecuredOperation("Admin")]
        public IDataResult<AboutViewDto> ListById(int id)
        {
            var result = _aboutDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<AboutViewDto>(Messages.AboutNotFound);
            }

            return new SuccessDataResult<AboutViewDto>(result, Messages.AboutInfoListed);
        }
    }
}
