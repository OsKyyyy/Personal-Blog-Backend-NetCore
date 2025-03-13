using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation.Contact;
using Entities.Dtos.Contact;

namespace Business.Concrete
{
    public class ContactManager : IContactService
    {
        IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }
         
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(ContactAddDto contactAddDto)
        {
            var contact = new Contact
            {
                Name = contactAddDto.Name,
                Email = contactAddDto.Email,
                Subject = contactAddDto.Subject,
                Message = contactAddDto.Message,
                Status = true,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Deleted = false
            };

            _contactDal.Add(contact);

            return new SuccessResult(Messages.ContactAdded);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(ContactUpdateDto contactUpdateDto)
        {
            var contact = new Contact
            {
                Id = contactUpdateDto.Id,
                Name = contactUpdateDto.Name,
                Email = contactUpdateDto.Email,
                Subject = contactUpdateDto.Subject,
                Message = contactUpdateDto.Message,
                Status = contactUpdateDto.Status,
                UpdateDate = DateTime.Now,
                UpdateUserId = contactUpdateDto.UpdateUserId
            };
            _contactDal.Update(contact);

            return new SuccessResult(Messages.ContactUpdated);
        }

        [SecuredOperation("Admin")]
        public IResult UpdateStatus(int id)
        {
            _contactDal.UpdateStatus(id);
            return new SuccessResult(Messages.ContactUpdated);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _contactDal.Delete(id);
            return new SuccessResult(Messages.ContactDeleted);
        }

        [SecuredOperation("Admin")]
        public IDataResult<List<ContactViewDto>> List()
        {
            var result = _contactDal.List();
            return new SuccessDataResult<List<ContactViewDto>>(result, Messages.ContactInfoListed);
        }

        [SecuredOperation("Admin")]
        public IDataResult<ContactViewDto> ListById(int id)
        {
            var result = _contactDal.ListById(id);
            return new SuccessDataResult<ContactViewDto>(result, Messages.ContactInfoListed);
        }

        [SecuredOperation("Admin")]
        public IResult CheckExistById(int id)
        {
            var result = _contactDal.CheckExistById(id);
            if (!result)
            {
                return new ErrorResult(Messages.ContactNotFound);
            }

            return new SuccessResult(Messages.ContactInfoListed);
        }
    }
}
