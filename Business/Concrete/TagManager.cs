using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Dtos.Tag;

namespace Business.Concrete
{
    public class TagManager : ITagService
    {
        ITagDal _tagDal;

        public TagManager(ITagDal tagDal)
        {
            _tagDal = tagDal;
        }
         
        [SecuredOperation("Admin")]
        public IResult Add(List<TagAddDto> tagAddDto)
        {
            List<Tag> tag = new List<Tag>();

            foreach (var item in tagAddDto)
            {
                tag.Add(new Tag
                {
                    BlogId = item.BlogId,
                    Name = item.Name,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateUserId = item.CreateUserId,
                    UpdateUserId = item.CreateUserId,
                    Deleted = false
                });                
            }

            _tagDal.Add(tag);

            return new SuccessResult(Messages.TagAdded);
        }
        

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _tagDal.Delete(id);
            return new SuccessResult(Messages.TagDeleted);
        }        
    }
}
