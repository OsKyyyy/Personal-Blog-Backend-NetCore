using Business.Abstract;
using Entities.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(UserUpdateDto userUpdateDto)
        {
            var userExists = _userService.CheckExistsForUpdate(userUpdateDto.Email, userUpdateDto.Id);
            if (!userExists.Status)
            {
                return BadRequest(userExists);
            }

            var result = _userService.Update(userUpdateDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Delete")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var listById = _userService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _userService.Delete(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
