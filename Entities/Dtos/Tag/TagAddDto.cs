using Core.Entities;

namespace Entities.Dtos.Tag
{
    public class TagAddDto : IDto
    {
        public int BlogId { get; set; }
        public string Name { get; set; }        
        public int CreateUserId { get; set; }
    }
}
