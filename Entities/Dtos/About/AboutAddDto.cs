using Core.Entities;

namespace Entities.Dtos.About
{
    public class AboutAddDto : IDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CreateUserId { get; set; }
    }
}
