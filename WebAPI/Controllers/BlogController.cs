using Business.Abstract;
using Entities.Dtos.Blog;
using Entities.Dtos.Tag;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        private ITagService _tagService;

        public BlogController(IBlogService blogService,ITagService tagService)
        {
            _blogService = blogService;
            _tagService = tagService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(BlogAddDto blogAddDto)
        {            
            using (var scope = new TransactionScope())
            {
                var result = _blogService.Add(blogAddDto);

                if (!result.Status)
                {
                    return BadRequest(result);
                }

                string[] tags = blogAddDto.Tags.Split(',');
                List<TagAddDto> tagList = new List<TagAddDto>();

                foreach (var item in tags)
                {
                    tagList.Add(new TagAddDto
                    {
                        BlogId = result.Data.Id,
                        Name = item.Trim(),
                        CreateUserId = blogAddDto.CreateUserId
                    });
                }

                var resultTag = _tagService.Add(tagList);

                if (!resultTag.Status)
                {
                    return BadRequest(resultTag);
                }

                scope.Complete();
                return Ok(result);               
            }
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(BlogUpdateDto blogUpdateDto)
        {
            var listById = _blogService.CheckExistById(blogUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _blogService.Update(blogUpdateDto);

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
            var listById = _blogService.CheckExistById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _blogService.Delete(id);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
