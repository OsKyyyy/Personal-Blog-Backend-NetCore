using Core.Utilities.Results.Abstract;
using Entities.Dtos.Contact;

namespace Business.Abstract
{
    public interface IContactService
    {
        IResult Add(ContactAddDto contactAddDto);
        IResult Update(ContactUpdateDto contactUpdateDto);
        IResult UpdateStatus(int id);
        IResult CheckExistById(int id);
        IResult Delete(int id);
        IDataResult<List<ContactViewDto>> List();
        IDataResult<ContactViewDto> ListById(int id);
    }
}
