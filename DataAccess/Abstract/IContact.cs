using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos.Contact;

namespace DataAccess.Abstract
{
    public interface IContactDal : IEntityRepository<Contact>
    {
        void Add(Contact contact);
        void Update(Contact contact);
        void UpdateStatus(int id);
        void Delete(int id);
        List<ContactViewDto> List();
        ContactViewDto ListById(int id);
        bool CheckExistById(int id);
    }
}
