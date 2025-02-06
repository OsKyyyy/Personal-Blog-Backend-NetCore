using Core.Utilities.Results.Abstract;
using Entities.Dtos.Resume;

namespace Business.Abstract
{
    public interface IResumeService
    {
        IResult Add(ResumeAddDto resumeAddDto);
        IResult Update(ResumeUpdateDto resumeUpdateDto);
        IDataResult<ResumeViewDto> ListById(int id);
        IResult Delete(int id);
    }
}
