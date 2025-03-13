using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos.BlogImage;

namespace DataAccess.Abstract
{
    public interface IBlogImageDal : IEntityRepository<BlogImage>
    {
        void Add(List<BlogImage> blogImage);
        void Delete(string url);
        List<BlogImageViewDto> ListById(int blogId);
    }
}
