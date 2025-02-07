using Core.Utilities.Results.Abstract;
using Entities.Dtos.Tag;

namespace Business.Abstract
{
    public interface ITagService
    {
        IResult Add(List<TagAddDto> tagAddDto);
        IResult Delete(int id);
    }
}
