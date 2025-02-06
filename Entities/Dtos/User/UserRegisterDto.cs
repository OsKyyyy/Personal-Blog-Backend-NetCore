using Core.Entities;

namespace Entities.Dtos.User
{
    public class UserRegisterDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public bool Status { get; set; }
    }
}
