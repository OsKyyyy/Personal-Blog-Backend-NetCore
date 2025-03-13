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
        List<ResumeViewDto> List();
        ResumeViewDto ListById(int id);
        bool CheckExistById(int id);

    }
}
