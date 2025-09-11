using AutoMapper;

namespace csms_backend.Models
{
    public class RepresentativeMapper : Profile
    {
        public RepresentativeMapper()
        {
            CreateMap<RepresentativeRequest, Representative>();

            CreateMap<Representative, RepresentativeResponse>();
        }
    }
}
