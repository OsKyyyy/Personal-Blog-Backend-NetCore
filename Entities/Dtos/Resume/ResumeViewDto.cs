using Core.Entities;

namespace Entities.Dtos.Resume
{
    public class ResumeViewDto : IDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Organization { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool CurrentPosition { get; set; }
    }
}
