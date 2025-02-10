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
                var result = (from r in context.Resume
                              where r.Id == resume.Id
                              select r).FirstOrDefault();

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

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Resume
                              where r.Id == id
                              select r).FirstOrDefault();

                result.Deleted = true;

                context.SaveChanges();
            }
        }

        public List<ResumeViewDto> List()
        {
            using (var context = new DataBaseContext())
            {
                var result = (from r in context.Resume
                              where r.Deleted == false
                              orderby r.StartDate descending
                              select new
                              {
                                  r.Id,
                                  r.Description,
                                  r.Title,
                                  r.Organization,
                                  r.StartDate,
                                  r.EndDate,
                                  r.CurrentPosition                                  
                              }).ToList();

                List<ResumeViewDto> resumeList = new List<ResumeViewDto>();

                foreach (var r in result)
                {
                    resumeList.Add(new ResumeViewDto
                    {
                        Id = r.Id,
                        Description = r.Description,
                        Title = r.Title,
                        Organization = r.Organization,
                        StartDate = r.StartDate.ToString("MMMM yyyy"),
                        EndDate = r.EndDate.HasValue ? r.EndDate.Value.ToString("MMMM yyyy") : null,
                        CurrentPosition = r.CurrentPosition
                    });
                }

                return resumeList;
            }
        }

        public bool CheckExistById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var exist = context.Resume.Any(r => r.Id == id);
                return exist;
            }
        }
    }
}
