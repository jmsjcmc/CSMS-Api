using AutoMapper;

namespace csms_backend.Models
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper()
        {
            CreateMap<CompanyRequest, Company>()
                .ForMember(d => d.Representative, o => o.MapFrom(s => s.Representative));

            CreateMap<Company, CompanyResponse>()
                .ForMember(d => d.Representative, o => o.MapFrom(s => s.Representative));
        }
    }
    public class RepresentativeMapper : Profile
    {
        public RepresentativeMapper()
        {
            CreateMap<RepresentativeRequest, Representative>();

            CreateMap<Representative, RepresentativeResponse>();
        }
    }
}
