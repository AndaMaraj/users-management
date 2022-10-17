using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace UsersManagement.Services.Mappings
{
    public interface IMapFrom
    {
        void Mapping(Profile profile);
    }
}
