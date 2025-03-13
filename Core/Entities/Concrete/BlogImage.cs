namespace Core.Entities.Concrete
{
    public class BlogImage : IEntity
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Url { get; set; }             
    }
}
