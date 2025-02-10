using Core.Utilities.Results.Abstract;
using Entities.Dtos.Resume;

namespace Business.Abstract
{
    public interface IResumeService
    {
        IResult Add(ResumeAddDto resumeAddDto);
        IResult Update(ResumeUpdateDto resumeUpdateDto);
        IResult CheckExistById(int id);
        IResult Delete(int id);
        IDataResult<List<ResumeViewDto>> List();
    }
}
