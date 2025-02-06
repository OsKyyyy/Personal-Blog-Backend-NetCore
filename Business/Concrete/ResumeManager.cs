using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation.Resume;
using Entities.Dtos.Resume;

namespace Business.Concrete
{
    public class ResumeManager : IResumeService
    {
        IResumeDal _resumeDal;

        public ResumeManager(IResumeDal resumeDal)
        {
            _resumeDal = resumeDal;
        }
         
        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(ResumeAddDto resumeAddDto)
        {
            var resume = new Resume
            {
                Description = resumeAddDto.Description,
                Title = resumeAddDto.Title,
                Organization = resumeAddDto.Organization,
                StartDate = resumeAddDto.StartDate,
                EndDate = resumeAddDto.EndDate,
                CurrentPosition = resumeAddDto.CurrentPosition,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = resumeAddDto.CreateUserId,
                UpdateUserId = resumeAddDto.CreateUserId,
                Deleted = false
            };

            _resumeDal.Add(resume);

            return new SuccessResult(Messages.ResumeAdded);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(ResumeUpdateDto resumeUpdateDto)
        {
            var resume = new Resume
            {
                Id = resumeUpdateDto.Id,
                Description = resumeUpdateDto.Description,
                Title = resumeUpdateDto.Title,
                Organization = resumeUpdateDto.Organization,
                StartDate = resumeUpdateDto.StartDate,
                EndDate = resumeUpdateDto.EndDate,
                CurrentPosition = resumeUpdateDto.CurrentPosition,
                UpdateDate = DateTime.Now,
                UpdateUserId = resumeUpdateDto.UpdateUserId
            };
            _resumeDal.Update(resume);

            return new SuccessResult(Messages.ResumeUpdated);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _resumeDal.Delete(id);
            return new SuccessResult(Messages.ResumeDeleted);
        }

        [SecuredOperation("Admin")]
        public IDataResult<ResumeViewDto> ListById(int id)
        {
            var result = _resumeDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ResumeViewDto>(Messages.ResumeNotFound);
            }

            return new SuccessDataResult<ResumeViewDto>(result, Messages.ResumeInfoListed);
        }
    }
}
