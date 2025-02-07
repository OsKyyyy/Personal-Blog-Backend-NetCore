using Core.Entities;

namespace Entities.Dtos.Blog
{
    public class BlogViewDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
    }
}
