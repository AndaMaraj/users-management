using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Models.DTO
{
    public class UserDto : BaseEntityDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual RoleDto Role { get; set; }
    }
}
