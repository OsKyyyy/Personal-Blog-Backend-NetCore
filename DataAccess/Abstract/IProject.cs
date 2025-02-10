using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos.Project;

namespace DataAccess.Abstract
{
    public interface IProjectDal : IEntityRepository<Project>
    {
        int Add(Project project);
        void Update(Project project);
        void Delete(int id);
        bool CheckExistById(int id);
    }
}
