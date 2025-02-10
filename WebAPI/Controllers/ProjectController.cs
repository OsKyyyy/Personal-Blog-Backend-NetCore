using Business.Abstract;
using Entities.Dtos.Project;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(ProjectAddDto projectAddDto)
        {                        
            var result = _projectService.Add(projectAddDto);

            if (!result.Status)
            {
                return BadRequest(result);
            }              

            return Ok(result);            
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(ProjectUpdateDto projectUpdateDto)
        {
            var listById = _projectService.CheckExistById(projectUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _projectService.Update(projectUpdateDto);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Route("Delete")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var listById = _projectService.CheckExistById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _projectService.Delete(id);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
