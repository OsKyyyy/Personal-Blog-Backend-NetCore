using Core.Entities;

namespace Entities.Dtos.Contact
{
    public class ContactAddDto : IDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
