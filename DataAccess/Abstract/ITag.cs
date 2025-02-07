using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ITagDal : IEntityRepository<Tag>
    {
        void Add(List<Tag> tag);
        void Delete(int id);
    }
}
