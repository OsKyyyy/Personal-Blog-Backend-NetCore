using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos.About;

namespace DataAccess.Abstract
{
    public interface IAboutDal : IEntityRepository<About>
    {
        void Add(About about);
        void Update(About about);
        void Delete(int id);
        void DeleteAll();
        AboutViewDto List();
        bool CheckExistById(int id);
    }
}
