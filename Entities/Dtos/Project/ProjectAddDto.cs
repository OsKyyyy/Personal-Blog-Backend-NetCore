using Core.Entities;

namespace Entities.Dtos.Project
{
    public class ProjectAddDto : IDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CreateUserId { get; set; }
    }
}
