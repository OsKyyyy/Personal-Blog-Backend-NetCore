using Core.Utilities.Results.Abstract;
using Entities.Dtos.About;

namespace Business.Abstract
{
    public interface IAboutService
    {
        IResult Add(AboutAddDto aboutAddDto);
        IResult Update(AboutUpdateDto aboutUpdateDto);
        IResult Delete(int id);
        IDataResult<AboutViewDto> List();
        IResult CheckExistById(int id);
    }
}
