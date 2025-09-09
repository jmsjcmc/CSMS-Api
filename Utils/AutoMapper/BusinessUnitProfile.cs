using AutoMapper;
using csms_backend.Models;

namespace csms_backend.Utils.AutoMapper
{
    public class BusinessUnitProfile : Profile
    {
        public BusinessUnitProfile()
        {
            CreateMap<BusinessUnitRequest, BusinessUnit>();

            CreateMap<BusinessUnit, BusinessUnitResponse>();
        }
    }
}
