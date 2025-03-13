using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos.Blog
{
    public class BlogAddDto : IDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public string Tags { get; set; }
        public int CreateUserId { get; set; }
    }
}
