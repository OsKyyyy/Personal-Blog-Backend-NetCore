using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.Project;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProjectDal : EfEntityRepositoryBase<Project, DataBaseContext>, IProjectDal
    {       
        public int Add(Project project)
        {
            using (var context = new DataBaseContext())
            {
                context.Projects.Add(project);
                int rowsAffected = context.SaveChanges(); 

                if (rowsAffected > 0)
                {
                    return project.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void Update(Project project)
        {
            using (var context = new DataBaseContext())
            {
                var result = (from p in context.Projects
                              where p.Id == project.Id
                              select p).FirstOrDefault();

                if (result != null)
                {
                    result.Title = project.Title;
                    result.Content = project.Content;
                    result.Slug = project.Slug;
                    result.UpdateDate = project.UpdateDate;
                    result.UpdateUserId = project.UpdateUserId;

                    context.SaveChanges();
                }
            }
        }

        public bool CheckExistById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var exist = context.Projects.Any(p => p.Id == id);
                return exist;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {

                var result = (from p in context.Projects
                              where p.Id == id
                              select p).FirstOrDefault();

                result.Deleted = true;

                context.SaveChanges();
            }
        }
    }
}
