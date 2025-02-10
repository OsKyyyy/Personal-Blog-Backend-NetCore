using Core.Utilities.Results.Abstract;
using Entities.Dtos.Project;

namespace Business.Abstract
{
    public interface IProjectService
    {
        IResult Add(ProjectAddDto projectAddDto);
        IResult Update(ProjectUpdateDto projectUpdateDto);
        IResult Delete(int id);
        IResult CheckExistById(int id);
    }
}
