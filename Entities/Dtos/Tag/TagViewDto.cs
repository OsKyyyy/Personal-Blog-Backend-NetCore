using Core.Entities;

namespace Entities.Dtos.Tag
{
    public class TagViewDto : IDto
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
    }
}
