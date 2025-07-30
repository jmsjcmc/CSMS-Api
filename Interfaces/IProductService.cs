using CSMapi.Models;

namespace CSMapi.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductOnlyResponse>> productslist(int id);
        Task<Pagination<ProductResponse>> allproducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<Pagination<ProductCompanyInventoryAsOfResponse>> customerbasedproducts_asof(
            int pageNumber = 1,
            int pageSize = 10,
            int? companyId = null,
            DateTime? asOf = null);
        Task<Pagination<ProductWithReceivingAndDispatchingResponse>> customerbasedproducts_fromto(
            int pageNumber = 1,
            int pageSize = 10,
            string? company = null,
            DateTime? from = null,
            DateTime? to = null);
        Task<Pagination<ProductBasedReceiving>> productbasedreceivings(
           int pageNumber = 1,
           int pageSize = 10,
           int? productId = null,
           DateTime? from = null,
           DateTime? to = null);
        Task<Pagination<ProductBasedDispatching>> productbaseddispatchings(
            int pageNumber = 1,
            int pageSize = 10,
            int? productId = null,
            DateTime? from = null,
            DateTime? to = null);
        Task<List<BasicProductResponse>> customerbasedproductsbasic(int id);
        //Task<Pagination<ProductSummary>> customerbasedproducts_summary(
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    string? company = null,
        //    DateTime? from = null,
        //    DateTime? to = null);
        Task<ProductResponse> getproduct(int id);
        Task<ProductResponse> getproductbycode(string productCode);
        Task<List<ProductCodeResponse>> getproductcodefordispatch();
        Task<ProductWithReceivingResponse> getproductwithreceivingdetail(string productCode);
        Task<ProductResponse> addproduct(ProductRequest request);
        Task<ProductResponse> updateproduct(ProductRequest request, int id);
        Task<ProductActiveResponse> toggleactive(int id);
        Task<ProductResponse> hideproduct(int id);
        Task<ProductResponse> deleteproduct(int id);
    }
}
