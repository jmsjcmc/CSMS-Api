using CSMapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSMapi.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductOnlyResponse>> productslist(string category);
        Task<Pagination<ProductResponse>> allproducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<Pagination<ProductWithReceivingAndDispatchingResponse>> customerbasedproducts_asof(
            int pageNumber = 1,
            int pageSize = 10,
            string? company = null);
        Task<Pagination<ProductWithReceivingAndDispatchingResponse>> customerbasedproducts_fromto(
            int pageNumber = 1,
            int pageSize = 10,
            string? company = null,
            DateTime? from = null,
            DateTime? to = null);
        Task<Pagination<ProductSummary>> customerbasedproducts_summary(
            int pageNumber = 1,
            int pageSize = 10,
            string? company = null,
            DateTime? from = null,
            DateTime? to = null);
        Task<ProductResponse> getproduct(int id);
        Task<ProductResponse> getproductbycode(string productCode);
        Task<List<ProductCodeResponse>> getproductcodefordispatch();
        Task<ProductWithReceivingResponse> getproductwithreceivingdetail(string productCode);
        Task<ProductResponse> addproduct(ProductRequest request);
        Task<ProductResponse> updateproduct(ProductRequest request, int id);
        Task<ProductActiveResponse> toggleactive(int id);
        Task hideproduct(int id);
        Task deleteproduct(int id);
    }
}
