using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Repository.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
