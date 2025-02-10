using Business.Abstract;
using Entities.Dtos.Resume;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : Controller
    {
        private IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(ResumeAddDto resumeAddDto)
        {            
            var result = _resumeService.Add(resumeAddDto);
            
            if (!result.Status)
            {
                return BadRequest(result);
            }            

            return Ok(result);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(ResumeUpdateDto resumeUpdateDto)
        {
            var listById = _resumeService.CheckExistById(resumeUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _resumeService.Update(resumeUpdateDto);

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
            var listById = _resumeService.CheckExistById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _resumeService.Delete(id);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Route("List")]
        [HttpGet]
        public ActionResult List()
        {
            var result = _resumeService.List();
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
