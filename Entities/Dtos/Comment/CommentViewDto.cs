using Core.Entities;

namespace Entities.Dtos.Comment
{
    public class CommentViewDto : IDto
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogSlug { get; set; }
        public int? ParentId { get; set; }
        public string CommentText { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreateDate { get; set; }
        public bool Status { get; set; }
        public List<CommentViewDto> Children { get; set; } = new List<CommentViewDto>();
    }
}
