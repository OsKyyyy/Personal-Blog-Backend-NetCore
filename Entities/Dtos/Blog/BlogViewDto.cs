using Core.Entities;
using Entities.Dtos.Comment;
using Entities.Dtos.Tag;

namespace Entities.Dtos.Blog
{
    public class BlogViewDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }

        public List<TagViewDto> Tags { get; set; }
        public List<CommentViewDto> Comments { get; set; }
    }
}
