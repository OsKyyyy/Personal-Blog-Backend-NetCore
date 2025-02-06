namespace Core.Entities.Concrete
{
    public class Resume : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Organization { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool CurrentPosition { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public bool Deleted { get; set; }
    }
}
