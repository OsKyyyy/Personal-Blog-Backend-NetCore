using Core.Utilities.Results.Abstract;
using Entities.Dtos.Blog;

namespace Business.Abstract
{
    public interface IBlogService
    {
        IDataResult<BlogViewDto> Add(BlogAddDto blogAddDto);
        IResult Update(BlogUpdateDto blogUpdateDto);
        IDataResult<BlogViewDto> ListById(int id);
        IResult Delete(int id);
    }
}
