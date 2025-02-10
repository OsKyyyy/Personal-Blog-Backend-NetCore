using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation.Comment;
using Entities.Dtos.Comment;

namespace Business.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }
         
        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(CommentAddDto commentAddDto)
        {
            var comment = new Comment
            {
                BlogId = commentAddDto.BlogId,
                ParentId = commentAddDto.ParentId,
                CommentText = commentAddDto.CommentText,
                Name = commentAddDto.Name,
                Email = commentAddDto.Email,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Status = false,
                Deleted = false
            };

            _commentDal.Add(comment);

            return new SuccessResult(Messages.CommentAdded);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(CommentUpdateDto commentUpdateDto)
        {
            var comment = new Comment
            {
                Id = commentUpdateDto.Id,
                BlogId = commentUpdateDto.BlogId,
                ParentId = commentUpdateDto.ParentId,
                CommentText = commentUpdateDto.CommentText,
                Name = commentUpdateDto.Name,
                Email = commentUpdateDto.Email,
                UpdateDate = DateTime.Now,
                Status = commentUpdateDto.Status,
            };
            _commentDal.Update(comment);

            return new SuccessResult(Messages.CommentUpdated);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _commentDal.Delete(id);
            return new SuccessResult(Messages.CommentDeleted);
        }

        [SecuredOperation("Admin")]
        public IResult CheckExistById(int id)
        {
            var result = _commentDal.CheckExistById(id);
            if (!result)
            {
                return new ErrorResult(Messages.CommentNotFound);
            }

            return new SuccessResult(Messages.CommentInfoListed);
        }
    }
}
