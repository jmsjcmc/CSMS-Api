using AutoMapper;

namespace csms_backend.Models.Entities
{
    public class PalletMapper : Profile
    {
        public PalletMapper()
        {
            CreateMap<PalletRequest, Pallet>();

            CreateMap<Pallet, PalletResponse>()
                .ForMember(d => d.CFirstName, o => o.MapFrom(s => s.Creator.FirstName))
                .ForMember(d => d.CLastName, o => o.MapFrom(s => s.Creator.LastName));
        }
    }
    public class PalletPositionMapper : Profile
    {
        public PalletPositionMapper()
        {
            CreateMap<PalletPositionRequest, PalletPosition>();

            CreateMap<PalletPosition, PalletPositionResponse>()
                .ForMember(d => d.CsNumber, o => o.MapFrom(s => s.ColdStorage.Number));
        }
    }
    public class ColdStorageMapper : Profile
    {
        public ColdStorageMapper()
        {
            CreateMap<ColdStorageRequest, ColdStorage>();

            CreateMap<ColdStorage, ColdStorageResponse>();
        }
    }
}
