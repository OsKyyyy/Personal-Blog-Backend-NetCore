using Core.Entities;

namespace Entities.Dtos.Contact
{
    public class ContactViewDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }        
        public bool Status { get; set; }
        public string CreateDate { get; set; }
        public string UpdateUser { get; set; }
    }
}
