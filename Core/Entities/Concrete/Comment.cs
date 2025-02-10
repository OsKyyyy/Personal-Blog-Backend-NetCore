namespace Core.Entities.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int? ParentId { get; set; }
        public string CommentText { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
    }
}
