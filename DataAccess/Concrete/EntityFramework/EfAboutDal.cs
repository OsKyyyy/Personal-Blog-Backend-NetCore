using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.About;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAboutDal : EfEntityRepositoryBase<About, DataBaseContext>, IAboutDal
    {       
        public void Add(About about)
        {
            using (var context = new DataBaseContext())
            {
                context.About.Add(about);
                context.SaveChanges();
            }
        }

        public void Update(About about)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from u in context.About
                              where u.Id == about.Id
                              select u).FirstOrDefault();

                if (result != null)
                {
                    result.Title = about.Title;
                    result.Content = about.Content;
                    result.Name = about.Name;
                    result.Title = about.Title;
                    result.DateOfBirth = about.DateOfBirth;
                    result.Address = about.Address;
                    result.Email = about.Email;
                    result.Phone = about.Phone;
                    result.UpdateDate = about.UpdateDate;
                    result.UpdateUserId = about.UpdateUserId;

                    context.SaveChanges();
                }
            }
        }

        public AboutViewDto ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from a in context.About
                              where a.Id == id
                              select new AboutViewDto
                              {
                                  Id = a.Id,
                                  Title = a.Title,
                                  Content = a.Content,
                                  Name = a.Name,
                                  DateOfBirth = a.DateOfBirth,
                                  Address = a.Address,
                                  Email = a.Email,
                                  Phone = a.Phone,
                              }).FirstOrDefault();                             

                return result;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {

                var result = (from a in context.About
                              where a.Id == id
                              select a).FirstOrDefault();

                result.Deleted = true;

                context.SaveChanges();
            }
        }
    }
}
