using AutoMapper;
using csms_backend.Models;

namespace csms_backend.Utils.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleRequest, Role>();

            CreateMap<Role, RoleResponse>();
        }
    }
}
