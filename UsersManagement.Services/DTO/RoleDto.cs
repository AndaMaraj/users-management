using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository.Entities;
using UsersManagement.Services.Mappings;

namespace UsersManagement.Services.DTO
{
    public class RoleDto : BaseEntityDto, IMapFrom
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
