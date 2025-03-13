using Business.Abstract;
using Entities.Dtos.About;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : Controller
    {
        private IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(AboutAddDto aboutAddDto)
        {
            using (var scope = new TransactionScope())
            {
                var resultDelete = _aboutService.DeleteAll();

                if (!resultDelete.Status)
                {
                    return BadRequest(resultDelete);
                }

                var result = _aboutService.Add(aboutAddDto);

                if (!result.Status)
                {
                    return BadRequest(result);
                }

                scope.Complete();
                return Ok(result);
            }
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(AboutUpdateDto aboutUpdateDto)
        {
            var listById = _aboutService.CheckExistById(aboutUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _aboutService.Update(aboutUpdateDto);

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
            var listById = _aboutService.CheckExistById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _aboutService.Delete(id);

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
            var result = _aboutService.List();
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
