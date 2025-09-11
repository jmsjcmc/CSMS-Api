using AutoMapper;
using csms_backend.Models;

namespace csms_backend.Models.Entities
{
    public class BusinessUnitMapper : Profile
    {
        public BusinessUnitMapper()
        {
            CreateMap<BusinessUnitRequest, BusinessUnit>();

            CreateMap<BusinessUnit, BusinessUnitResponse>();
        }
    }
}
