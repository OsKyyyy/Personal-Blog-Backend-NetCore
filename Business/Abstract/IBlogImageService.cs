using Core.Utilities.Results.Abstract;
using Entities.Dtos.BlogImage;

namespace Business.Abstract
{
    public interface IBlogImageService
    {
        IResult Add(int blogId, string content);
        IResult UpdateImages(string content, List<BlogImageViewDto> images);
        IDataResult<List<BlogImageViewDto>> ListById(int blogId);
    }
}
