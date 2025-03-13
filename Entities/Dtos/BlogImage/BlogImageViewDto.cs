using Core.Entities;

namespace Entities.Dtos.BlogImage
{
    public class BlogImageViewDto : IDto
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Url { get; set; }       
    }
}
