using Core.Entities;

namespace Entities.Dtos.Project
{
    public class ProjectUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UpdateUserId { get; set; }
    }
}
