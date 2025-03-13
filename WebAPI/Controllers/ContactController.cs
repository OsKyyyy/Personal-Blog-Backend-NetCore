using Business.Abstract;
using Entities.Dtos.Contact;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(ContactAddDto contactAddDto)
        {            
            var result = _contactService.Add(contactAddDto);
            
            if (!result.Status)
            {
                return BadRequest(result);
            }            

            return Ok(result);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(ContactUpdateDto contactUpdateDto)
        {
            var listById = _contactService.CheckExistById(contactUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _contactService.Update(contactUpdateDto);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Route("UpdateStatus")]
        [HttpPut]
        public ActionResult UpdateStatus(int id)
        {
            var listById = _contactService.CheckExistById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _contactService.UpdateStatus(id);

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
            var listById = _contactService.CheckExistById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _contactService.Delete(id);

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
            var result = _contactService.List();
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var result = _contactService.ListById(id);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
