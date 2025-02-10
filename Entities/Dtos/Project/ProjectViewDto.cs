using Core.Entities;

namespace Entities.Dtos.Project
{
    public class ProjectViewDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
