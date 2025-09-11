using AutoMapper;
using csms_backend.Models;

namespace csms_backend.Models.Entities
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserRequest, User>()
                .ForMember(d => d.Password, o => o.Ignore())
                .ForMember(d => d.UserRole, o => o.MapFrom(s => s.RoleId.Select(roleId => new UserRoleRelation
                {
                    RoleId = roleId
                })));

            CreateMap<User, UserResponse>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.BusinessUnit.Name))
                .ForMember(d => d.Location, o => o.MapFrom(s => s.BusinessUnit.Location))
                .ForMember(d => d.Role, o => o.MapFrom(s => s.UserRole.Select(ur => ur.Role)));

            CreateMap<UserLoginRequest, User>();
        }
    }
}
