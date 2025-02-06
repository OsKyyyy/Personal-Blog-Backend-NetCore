using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos.Resume;

namespace DataAccess.Abstract
{
    public interface IResumeDal : IEntityRepository<Resume>
    {
        void Add(Resume resume);
        void Update(Resume resume);
        void Delete(int id);
        ResumeViewDto ListById(int id);

    }
}
