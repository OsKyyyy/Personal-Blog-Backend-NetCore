using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Aspects.Autofac.Validation;
using Entities.Dtos.Blog;
using Business.ValidationRules.FluentValidation.Blog;

namespace Business.Concrete
{
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }
         
        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(AddValidator))]
        public IDataResult<BlogViewDto> Add(BlogAddDto blogAddDto)
        {
            var blog = new Blog
            {
                Title = blogAddDto.Title,
                Content = blogAddDto.Content,
                Slug = SlugGenerator.GenerateSlug(blogAddDto.Title),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = blogAddDto.CreateUserId,
                UpdateUserId = blogAddDto.CreateUserId,
                Deleted = false
            };

            int result = _blogDal.Add(blog);

            var blogViewDto = new BlogViewDto()
            {
                Id = result
            };

            if (result == 0)
            {
                return new ErrorDataResult<BlogViewDto>(blogViewDto,Messages.BlogAddError);
            }

            return new SuccessDataResult<BlogViewDto>(blogViewDto,Messages.BlogAdded);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(BlogUpdateDto blogUpdateDto)
        {
            var blog = new Blog
            {
                Id = blogUpdateDto.Id,
                Title = blogUpdateDto.Title,
                Content = blogUpdateDto.Content,
                Slug = SlugGenerator.GenerateSlug(blogUpdateDto.Title),
                UpdateDate = DateTime.Now,
                UpdateUserId = blogUpdateDto.UpdateUserId
            };
            _blogDal.Update(blog);

            return new SuccessResult(Messages.BlogUpdated);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _blogDal.Delete(id);
            return new SuccessResult(Messages.BlogDeleted);
        }

        [SecuredOperation("Admin")]
        public IResult CheckExistById(int id)
        {
            var result = _blogDal.CheckExistById(id);
            if (!result)
            {
                return new ErrorResult(Messages.BlogNotFound);
            }

            return new SuccessResult(Messages.BlogInfoListed);
        }
    }
}
