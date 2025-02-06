using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.Resume;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfResumeDal : EfEntityRepositoryBase<Resume, DataBaseContext>, IResumeDal
    {       
        public void Add(Resume resume)
        {
            using (var context = new DataBaseContext())
            {
                context.Resume.Add(resume);
                context.SaveChanges();
            }
        }

        public void Update(Resume resume)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from u in context.Resume
                              where u.Id == resume.Id
                              select u).FirstOrDefault();

                if (result != null)
                {
                    result.Description = resume.Description;
                    result.Title = resume.Title;
                    result.Organization = resume.Organization;
                    result.StartDate = resume.StartDate;
                    result.EndDate = resume.EndDate;
                    result.CurrentPosition = resume.CurrentPosition;
                    result.UpdateDate = resume.UpdateDate;
                    result.UpdateUserId = resume.UpdateUserId;

                    context.SaveChanges();
                }
            }
        }

        public ResumeViewDto ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from a in context.Resume
                              where a.Id == id
                              select new ResumeViewDto
                              {
                                  Id = a.Id,
                                  Description = a.Description,
                                  Title = a.Title,
                                  Organization = a.Organization,
                                  StartDate = a.StartDate,
                                  EndDate = a.EndDate,
                                  CurrentPosition = a.CurrentPosition,
                              }).FirstOrDefault();                             

                return result;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {

                var result = (from a in context.Resume
                              where a.Id == id
                              select a).FirstOrDefault();

                result.Deleted = true;

                context.SaveChanges();
            }
        }
    }
}
