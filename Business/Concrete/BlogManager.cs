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
using DataAccess.Concrete.EntityFramework;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

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
            string imagePath = null;

            if (blogAddDto.Image != null && blogAddDto.Image.Length > 0)
            {
                if (blogAddDto.Image.Length > 2 * 1024 * 1024)
                {
                    var failures = new List<ValidationFailure>
                    {
                        new ValidationFailure("DosyaBoyutu", "Fotoğraf en fazla 2MB olmalıdır")
                    };
                    throw new ValidationException("Validation Error", failures);
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(blogAddDto.Image.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    var failures = new List<ValidationFailure>
                    {
                        new ValidationFailure("DosyaFormatı", "Sadece JPG, JPEG ve PNG formatları desteklenmektedir")
                    };
                    throw new ValidationException("Validation Error", failures);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/blogs");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    blogAddDto.Image.CopyToAsync(stream);
                }

                imagePath = $"/uploads/blogs/{fileName}";
            }

            var blog = new Blog
            {
                Title = blogAddDto.Title,
                Content = blogAddDto.Content,
                Image = imagePath,
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
            string imagePath = null;

            if (blogUpdateDto.Image != null && blogUpdateDto.Image.Length > 0)
            {
                if (blogUpdateDto.Image.Length > 2 * 1024 * 1024)
                {
                    var failures = new List<ValidationFailure>
                    {
                        new ValidationFailure("DosyaBoyutu", "Fotoğraf en fazla 2MB olmalıdır")
                    };
                    throw new ValidationException("Validation Error", failures);
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(blogUpdateDto.Image.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    var failures = new List<ValidationFailure>
                    {
                        new ValidationFailure("DosyaFormatı", "Sadece JPG, JPEG ve PNG formatları desteklenmektedir")
                    };
                    throw new ValidationException("Validation Error", failures);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/blogs");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    blogUpdateDto.Image.CopyToAsync(stream);
                }

                imagePath = $"/uploads/blogs/{fileName}";
            }

            var blog = new Blog
            {
                Id = blogUpdateDto.Id,
                Title = blogUpdateDto.Title,
                Content = blogUpdateDto.Content,
                Slug = SlugGenerator.GenerateSlug(blogUpdateDto.Title),
                UpdateDate = DateTime.Now,
                UpdateUserId = blogUpdateDto.UpdateUserId
            };
            if (!string.IsNullOrEmpty(imagePath))
            {
                blog.Image = imagePath;
            }
            _blogDal.Update(blog);

            return new SuccessResult(Messages.BlogUpdated);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _blogDal.Delete(id);
            return new SuccessResult(Messages.BlogDeleted);
        }

        public IDataResult<List<BlogViewDto>> List()
        {
            var result = _blogDal.List();
            return new SuccessDataResult<List<BlogViewDto>>(result, Messages.BlogInfoListed);
        }
        
        public IDataResult<BlogViewDto> ListById(int id)
        {
            var result = _blogDal.ListById(id);
            return new SuccessDataResult<BlogViewDto>(result, Messages.BlogInfoListed);
        }

        public IDataResult<BlogViewDto> ListBySlug(string slug)
        {
            var result = _blogDal.ListBySlug(slug);
            return new SuccessDataResult<BlogViewDto>(result, Messages.BlogInfoListed);
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
