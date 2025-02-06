using Core.Entities;

namespace Entities.Dtos
{
    public class UserRoleAddDto : IDto
    {
        public int Id { get; set; }
        public int OperationClaimId { get; set; }
    }
}
