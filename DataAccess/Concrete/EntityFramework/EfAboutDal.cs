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

        public AboutViewDto List()
        {
            using (var context = new DataBaseContext())
            {
                var result = (from a in context.About
                              where a.Deleted == false
                              select new
                              {
                                  a.Id,
                                  a.Title,
                                  a.Content,
                                  a.Name,
                                  a.DateOfBirth,
                                  a.Address,
                                  a.Email,
                                  a.Phone,
                              }).FirstOrDefault();
                return new AboutViewDto
                {
                    Id = result.Id,
                    Title = result.Title,
                    Content = result.Content,
                    Name = result.Name,
                    DateOfBirth = result.DateOfBirth.ToString("dd/MM/yyyy"),
                    Address = result.Address,
                    Email = result.Email,
                    Phone = result.Phone,
                };
            }
        }

        public bool CheckExistById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var exists = context.About.Any(a => a.Id == id);
                return exists;
            }
        }
    }
}
