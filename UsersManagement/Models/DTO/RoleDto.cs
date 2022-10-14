using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Models.DTO
{
    public class RoleDto : BaseEntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }
    }
}
