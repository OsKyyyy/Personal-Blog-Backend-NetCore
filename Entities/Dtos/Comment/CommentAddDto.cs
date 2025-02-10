using Core.Entities;

namespace Entities.Dtos.Comment
{
    public class CommentAddDto : IDto
    {
        public int BlogId { get; set; }
        public int? ParentId { get; set; }
        public string CommentText { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
