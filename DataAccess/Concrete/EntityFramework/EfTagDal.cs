using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTagDal : EfEntityRepositoryBase<Tag, DataBaseContext>, ITagDal
    {       
        public void Add(List<Tag> tag)
        {
            using (var context = new DataBaseContext())
            {
                context.Tags.AddRange(tag);
                context.SaveChanges();
            }
        }              

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {

                var result = (from a in context.Tags
                              where a.Id == id
                              select a).FirstOrDefault();

                result.Deleted = true;

                context.SaveChanges();
            }
        }
    }
}
