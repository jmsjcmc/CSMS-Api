using AutoMapper;
using csms_backend.Models;

namespace csms_backend.Models.Entities
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<RoleRequest, Role>();

            CreateMap<Role, RoleResponse>();
        }
    }
}
