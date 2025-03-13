using Business.Abstract;
using Entities.Dtos.Blog;
using Entities.Dtos.Tag;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Transactions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        private ITagService _tagService;
        private IBlogImageService _blogImageService;

        public BlogController(IBlogService blogService,ITagService tagService, IBlogImageService blogImageService)
        {
            _blogService = blogService;
            _tagService = tagService;
            _blogImageService = blogImageService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add([FromForm] BlogAddDto blogAddDto)
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

                var blogImages = _blogImageService.Add(result.Data.Id, blogAddDto.Content);

                if (!blogImages.Status)
                {
                    return BadRequest(resultTag);
                }                

                scope.Complete();
                return Ok(result);               
            }
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update([FromForm] BlogUpdateDto blogUpdateDto)
        {
            using (var scope = new TransactionScope())
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

                var listImages = _blogImageService.ListById(blogUpdateDto.Id);
                if (!listImages.Status)
                {
                    return BadRequest(listImages);
                }

                var updateImages = _blogImageService.UpdateImages(blogUpdateDto.Content, listImages.Data);
                if (!listImages.Status)
                {
                    return BadRequest(listImages);
                }

                scope.Complete();
                return Ok(result);
            }
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

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var result = _blogService.ListById(id);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Route("ListBySlug")]
        [HttpGet]
        public ActionResult ListBySlug(string slug)
        {
            var result = _blogService.ListBySlug(slug);
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
            var result = _blogService.List();
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
