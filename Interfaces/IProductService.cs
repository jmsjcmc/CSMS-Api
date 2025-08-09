using CSMapi.Models;

namespace CSMapi.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductOnlyResponse>> ProductsList(int id);
        Task<Pagination<ProductResponse>> AllProducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<Pagination<ProductCompanyInventoryAsOfResponse>> customerbasedproducts_asof(
            int pageNumber = 1,
            int pageSize = 10,
            int? companyId = null,
            DateTime? asOf = null);
        Task<ProductWithReceivingResponse> GetProductWithReceivingDetail(string productCode);
        Task<Pagination<ProductWithReceivingAndDispatchingResponse>> CustomerBasedProducts_FromTo(
            int pageNumber = 1,
            int pageSize = 10,
            string? company = null,
            DateTime? from = null,
            DateTime? to = null);
        Task<Pagination<ProductBasedReceiving>> ProductBasedReceivings(
           int pageNumber = 1,
           int pageSize = 10,
           int? productId = null,
           DateTime? from = null,
           DateTime? to = null);
        Task<Pagination<ProductBasedDispatching>> ProductBasedDispatchings(
            int pageNumber = 1,
            int pageSize = 10,
            int? productId = null,
            DateTime? from = null,
            DateTime? to = null);
        Task<List<BasicProductResponse>> CustomerBasedProductsBasic(int id);
        //Task<Pagination<ProductSummary>> customerbasedproducts_summary(
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    string? company = null,
        //    DateTime? from = null,
        //    DateTime? to = null);
        Task<ProductResponse> GetProduct(int id);
        Task<ProductResponse> GetProductByCode(string productCode);
        Task<List<ProductCodeResponse>> GetProductCodeForDispatch();
        //Task<ProductWithReceivingResponse> getproductwithreceivingdetail(string productCode);
        Task<ProductResponse> AddProduct(ProductRequest request);
        Task<ProductResponse> UpdateProduct(ProductRequest request, int id);
        Task<ProductActiveResponse> ToggleActive(int id);
        Task<ProductResponse> HideProduct(int id);
        Task<ProductResponse> DeleteProduct(int id);
    }
}
