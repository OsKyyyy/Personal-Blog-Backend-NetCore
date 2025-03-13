using Core.Entities;

namespace Entities.Dtos.User
{
    public class UserViewDto : IDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public string? RoleName { get; set; }

        public bool Status { get; set; }


        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
