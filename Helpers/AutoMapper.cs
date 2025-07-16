using AutoMapper;
using CSMapi.Models;

namespace CSMapi.Helpers
{
    public class AutoMapper : Profile
    {
       
        public AutoMapper()
        {
            // Dispatchings Mapping 
            CreateMap<DispatchingRequest, Dispatching>()
                .ForMember(d => d.Requestorid, o => o.Ignore())
                .ForMember(d => d.Approverid, o => o.Ignore())
                .ForMember(d => d.Createdon, o => o.Ignore())
                .ForMember(d => d.Approvedon, o => o.Ignore())
                .ForMember(d => d.Declinedon, o => o.Ignore())
                .ForMember(d => d.Updatedon, o => o.Ignore())
                .ForMember(d => d.Dispatched, o => o.Ignore())
                .ForMember(d => d.Pending, o => o.Ignore());

            CreateMap<Dispatching, DispatchingResponse>()
                .ForMember(d => d.Document, o => o.MapFrom(s => s.Document))
                .ForMember(d => d.Product, o => o.MapFrom(s => s.Product))
                .ForMember(d => d.Requestor, o => o.MapFrom(s => s.Requestor))
                .ForMember(d => d.Approver, o => o.MapFrom(s => s.Approver))
                .ForMember(d => d.DispatchingDetail, o => o.MapFrom(s => s.Dispatchingdetails));

            CreateMap<Dispatching, DispatchingTimeStartEndResponse>();

            CreateMap<Dispatching, ProductDispatchingResponse>()
                .ForMember(d => d.Document, o => o.MapFrom(s => s.Document))
                .ForMember(d => d.DispatchignDetail, o => o.MapFrom(s => s.Dispatchingdetails));
            // Dispatching Details Mapping
            CreateMap<DispatchingDetailRequest, DispatchingDetail>()
                .ForMember(d => d.Partialdispatched, o => o.Ignore())
                .ForMember(d => d.Fulldispatched, o => o.Ignore());

            CreateMap<DispatchingDetail, DispatchingDetailResponse>()
                .ForMember(d => d.Position, o => o.MapFrom(s => s.PalletPosition))
                .ForMember(d => d.Pallet, o => o.MapFrom(s => s.Pallet));
            // Documents Mapping
            CreateMap<DocumentRequest, Document>();

            CreateMap<Document, DocumentResponse>();
            // Cold Storages Mapping
            CreateMap<ColdStorageRequest, ColdStorage>()
                .ForMember(d => d.Active, o => o.Ignore());

            CreateMap<ColdStorage, ColdStorageResponse>();
            // Contracts Mapping
            CreateMap<ContractRequest, Contract>()
                .ForMember(d => d.Leasedpremises, o => o.MapFrom(s => s.LeasedPremise))
                .ForMember(d => d.Createdon, o => o.Ignore())
                .ForMember(d => d.Creatorid, o => o.Ignore())
                .ForMember(d => d.Updatedon, o => o.Ignore())
                .ForMember(d => d.Active, o => o.Ignore())
                .ForMember(d => d.Removed, o => o.Ignore());

            CreateMap<Contract, ContractResponse>()
                .ForMember(d => d.User, o => o.MapFrom(s => s.Creator))
                .ForMember(d => d.LeasedPremise, o => o.MapFrom(s => s.Leasedpremises));
            // Customers Mapping
            CreateMap<CustomerRequest, Customer>()
                .ForMember(d => d.Active, o => o.Ignore())
                .ForMember(d => d.Removed, o => o.Ignore());

            CreateMap<Customer, CustomerResponse>();
            // Leased Premises Mapping
            CreateMap<LeasedPremisesRequest, LeasedPresmises>();

            CreateMap<LeasedPresmises, LeasedPremisesResponse>();
            // Pallets Mapping
            CreateMap<PalletRequest, Pallet>()
                .ForMember(d => d.Createdon, o => o.Ignore())
                .ForMember(d => d.Creatorid, o => o.Ignore())
                .ForMember(d => d.Updatedon, o => o.Ignore())
                .ForMember(d => d.Occupied, o => o.Ignore())
                .ForMember(d => d.Active, o => o.Ignore())
                .ForMember(d => d.Removed, o => o.Ignore());

            CreateMap<Pallet, PalletResponse>()
                .ForMember(d => d.User, o => o.MapFrom(s => s.Creator));

            CreateMap<Pallet, PalletOnlyResponse>();

            CreateMap<Pallet, ActivePalletResponse>();

            CreateMap<Pallet, OccupiedPalletResponse>()
                .ForMember(d => d.Product, o => o.MapFrom(s => s.ReceivingDetail
                .Where(r => r.Receiving != null && r.Receiving.Product != null && !r.Receiving.Declined)
                .Select(r => r.Receiving.Product)
                .FirstOrDefault()))
                .ForMember(d => d.ReceivingDetail, o => o.MapFrom(s => s.ReceivingDetail.Where(r => !r.Fulldispatched && r.Received)));

            CreateMap<Pallet, ProductBasedOccupiedPalletResponse>();

            CreateMap<Pallet, PalletTypeBasedResponse>();
            // Pallet Positions Mapping
            CreateMap<PalletPositionRequest, PalletPosition>()
                .ForMember(d => d.Hidden, o => o.Ignore())
                .ForMember(d => d.Removed, o => o.Ignore());

            CreateMap<PalletPosition, PalletPositionResponse>()
                .ForMember(d => d.ColdStorage, o => o.MapFrom(s => s.Coldstorage));
            // Products Mapping
            CreateMap<ProductRequest, Product>()
                .ForMember(d => d.Active, o => o.Ignore())
                .ForMember(d => d.Removed, o => o.Ignore());

            CreateMap<Product, ProductCodeResponse>();

            CreateMap<Product, ProductWithReceivingAndDispatchingResponse>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Customer, o => o.MapFrom(s => s.Customer))
                .ForMember(d => d.Receiving, o => o.MapFrom(s => s.Receiving))
                .ForMember(d => d.Dispatching, o => o.MapFrom(s => s.Dispatching));

            CreateMap<Product, ProductActiveResponse>();

            CreateMap<Product, ProductResponse>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category))
                .ForMember(d => d.Customer, o => o.MapFrom(s => s.Customer));

            CreateMap<Product, ProductOnlyResponse>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category));

            CreateMap<Product, ProductWithReceivingResponse>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Overallweight, o => o.Ignore())
                .ForMember(d => d.Customer, o => o.MapFrom(s => s.Customer))
                .ForMember(d => d.ReceivingDetail, o => o.MapFrom((s, d, dm, c) =>
                    s.Receiving
                        .Where(r => r.Productid == s.Id)
                        .SelectMany(r => r.Receivingdetails
                        .Where(r => r.Receiving.Received && !r.Fulldispatched)
                        .Select(detail => c.Mapper.Map<ProductReceivingDetailResponse>(detail))
                        .ToList()
                )));

            CreateMap<Product, ProductSummary>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Companyname, o => o.MapFrom(s => s.Customer.Companyname));

            CreateMap<Product, ProductBasedReceivingDispatchingResponse>()
                .ForMember(d => d.Receiving, o => o.MapFrom(s => s.Receiving))
                .ForMember(d => d.Dispatching, o => o.MapFrom(s => s.Dispatching));

            CreateMap<Product, BasicProductResponse>();
            // Repalletizations Mapping
            CreateMap<RepalletizationRequest, Repalletization>()
                .ForMember(d => d.Createdon, o => o.Ignore())
                .ForMember(d => d.Creatorid, o => o.Ignore())
                .ForMember(d => d.RepalletizationDetail, o => o.MapFrom(s => s.RepalletizationDetail));

            CreateMap<RepalletizationDetailRequest, RepalletizationDetail>();
            // Receivings Mapping
            CreateMap<ReceivingRequest, Receiving>()
                .ForMember(d => d.Receivingdetails, o => o.MapFrom(s => s.ReceivingDetail))
                .ForMember(d => d.Documentid, o => o.Ignore())
                .ForMember(d => d.Approverid, o => o.Ignore())
                .ForMember(d => d.Updatedon, o => o.Ignore())
                .ForMember(d => d.Createdon, o => o.Ignore())
                .ForMember(d => d.Requestorid, o => o.Ignore())
                .ForMember(d => d.Datereceived, o => o.Ignore())
                .ForMember(d => d.Datedeclined, o => o.Ignore())
                .ForMember(d => d.Pending, o => o.Ignore())
                .ForMember(d => d.Received, o => o.Ignore())
                .ForMember(d => d.Declined, o => o.Ignore())
                .ForMember(d => d.Removed, o => o.Ignore());

            CreateMap<Receiving, ReceivingResponse>()
                .ForMember(d => d.Document, o => o.MapFrom(s => s.Document))
                .ForMember(d => d.Product, o => o.MapFrom(s => s.Product))
                .ForMember(d => d.Requestor, o => o.MapFrom(s => s.Requestor))
                .ForMember(d => d.Approver, o => o.MapFrom(s => s.Approver))
                .ForMember(d => d.ReceivingDetail, o => o.MapFrom(s => s.Receivingdetails));

            CreateMap<Receiving, ProductReceivingResponse>()
                .ForMember(d => d.Document, o => o.MapFrom(s => s.Document))
                .ForMember(d => d.ReceivingDetail, o => o.MapFrom(s => s.Receivingdetails));
            // Receiving Details Mapping
            CreateMap<ReceivingDetailRequest, ReceivingDetail>()
                .ForMember(d => d.Partialdispatched, o => o.Ignore())
                .ForMember(d => d.Fulldispatched, o => o.Ignore());

            CreateMap<ReceivingDetail, ReceivingDetailResponse>()
                .ForMember(d => d.Pallet, o => o.MapFrom(s => s.Pallet))
                .ForMember(d => d.Position, o => o.MapFrom(s => s.PalletPosition));

            CreateMap<ReceivingDetail, ProductReceivingDetailResponse>()
                .ForMember(d => d.Quantityinapallet, o => o.Ignore())
                .ForMember(d => d.Totalweight, o => o.Ignore())
                .ForMember(d => d.Pallet, o => o.MapFrom(s => s.Pallet))
                .ForMember(d => d.Position, o => o.MapFrom(s => s.PalletPosition));

            CreateMap<ReceivingDetail, RepalletizeDetailResponse>();
            // Roles Mapping
            CreateMap<RoleRequest, Role>()
                .ForMember(d => d.Removed, o => o.Ignore());

            CreateMap<Role, RoleResponse>();
            // Users Mapping
            CreateMap<UserRequest, User>()
                .ForMember(d => d.Active, o => o.Ignore())
                .ForMember(d => d.Createdon, o => o.Ignore())
                .ForMember(d => d.Updatedon, o => o.Ignore())
                .ForMember(d => d.Removed, o => o.Ignore());

            CreateMap<User, UserResponse>();

            CreateMap<User, UserEsignResponse>();
            // Repalletization Mapping
            CreateMap<RepalletizationRequest, Repalletization>()
                .ForMember(d => d.RepalletizationDetail, o => o.MapFrom(s => s.RepalletizationDetail));

            CreateMap<Repalletization, RepalletizationResponse>()
                .ForMember(d => d.RepalletizationDetail, o => o.MapFrom(s => s.RepalletizationDetail));
            // Repalletization Detail Mapping
            CreateMap<RepalletizationDetailRequest, RepalletizationDetail>();

            CreateMap<RepalletizationDetail, RepalletizationDetailResponse>();
            // Category Mapping
            CreateMap<CategoryRequest, Category>();

            CreateMap<Category, CategoryResponse>();
        }
    }
}
