using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Aspects.Autofac.Validation;
using Entities.Dtos.Project;
using Business.ValidationRules.FluentValidation.Project;

namespace Business.Concrete
{
    public class ProjectManager : IProjectService
    {
        IProjectDal _projectDal;

        public ProjectManager(IProjectDal projectDal)
        {
            _projectDal = projectDal;
        }
         
        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(ProjectAddDto projectAddDto)
        {
            var project = new Project
            {
                Title = projectAddDto.Title,
                Content = projectAddDto.Content,
                Slug = SlugGenerator.GenerateSlug(projectAddDto.Title),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = projectAddDto.CreateUserId,
                UpdateUserId = projectAddDto.CreateUserId,
                Deleted = false
            };

            _projectDal.Add(project);

            return new SuccessResult(Messages.ProjectAdded);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(ProjectUpdateDto projectUpdateDto)
        {
            var project = new Project
            {
                Id = projectUpdateDto.Id,
                Title = projectUpdateDto.Title,
                Content = projectUpdateDto.Content,
                Slug = SlugGenerator.GenerateSlug(projectUpdateDto.Title),
                UpdateDate = DateTime.Now,
                UpdateUserId = projectUpdateDto.UpdateUserId
            };
            _projectDal.Update(project);

            return new SuccessResult(Messages.ProjectUpdated);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _projectDal.Delete(id);
            return new SuccessResult(Messages.ProjectDeleted);
        }

        [SecuredOperation("Admin")]
        public IResult CheckExistById(int id)
        {
            var result = _projectDal.CheckExistById(id);
            if (!result)
            {
                return new ErrorResult(Messages.ProjectNotFound);
            }

            return new SuccessResult(Messages.ProjectInfoListed);
        }
    }
}
