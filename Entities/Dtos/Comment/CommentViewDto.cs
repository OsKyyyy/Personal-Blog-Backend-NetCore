using Core.Entities;

namespace Entities.Dtos.Comment
{
    public class CommentViewDto : IDto
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int? ParentId { get; set; }
        public string CommentText { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
