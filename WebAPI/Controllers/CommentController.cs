using Business.Abstract;
using Entities.Dtos.Comment;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(CommentAddDto commentAddDto)
        {            
            var result = _commentService.Add(commentAddDto);
            
            if (!result.Status)
            {
                return BadRequest(result);
            }            

            return Ok(result);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(CommentUpdateDto commentUpdateDto)
        {
            var listById = _commentService.CheckExistById(commentUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _commentService.Update(commentUpdateDto);

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
            var result = _commentService.List();
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
            var result = _commentService.ListById(id);
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
            var listById = _commentService.CheckExistById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _commentService.Delete(id);

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
            var listById = _commentService.CheckExistById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _commentService.UpdateStatus(id);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
