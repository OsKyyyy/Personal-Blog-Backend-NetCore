namespace Core.Entities.Concrete
{
    public class Tag : IEntity
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Name { get; set; }      
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public bool Deleted { get; set; }
    }
}
