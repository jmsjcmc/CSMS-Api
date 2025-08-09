namespace CSMapi.Models
{
    public class ProductRequest
    {
        public int Categoryid { get; set; }
        public int Customerid { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
        public string Productpackaging { get; set; }
        public string Deliveryunit { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
    }

    public class ProductResponse
    {
        public int Id { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
        public string Productpackaging { get; set; }
        public string Deliveryunit { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
        public bool Active { get; set; }
        public bool Removed { get; set; }
        public CategoryResponse Category { get; set; }
        public CustomerResponse Customer { get; set; }
    }
    public class ProductActiveResponse
    {
        public int Id { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public bool Active { get; set; }
    }
    public class ProductOnlyResponse
    {
        public int Id { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
        public string Productpackaging { get; set; }
        public string Deliveryunit { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
        public bool Active { get; set; }
        public bool Removed { get; set; }
        public CategoryResponse Category { get; set; }
    }

    public class ProductCodeResponse
    {
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
    }

    public class ProductWithReceivingResponse
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
        public string Productpackaging { get; set; }
        public string Deliveryunit { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
        public double Overallweight { get; set; }
        public CustomerResponse Customer { get; set; }
        public List<ProductReceivingDetailResponse> ReceivingDetail { get; set; }
    }
    public class ProductCompanyInventoryAsOfResponse
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public List<ProductCompanyInventoryReceivingResponse> Receiving { get; set; }
    }
    public class ProductWithReceivingAndDispatchingResponse
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
        public string Productpackaging { get; set; }
        public string Deliveryunit { get; set; }
        public int? Quantity { get; set; }
        public string Uom { get; set; }
        public double? Weight { get; set; }
        public string Unit { get; set; }
        public bool Active { get; set; }
        public bool Removed { get; set; }
        public CustomerResponse Customer { get; set; }
        public List<ProductReceivingResponse> Receiving { get; set; }
        public List<ProductDispatchingResponse> Dispatching { get; set; }
    }

    public class ProductBasedReceiving
    {
        public int? Id { get; set; }
        public string? Productname { get; set; }
        public string? Productpackaging { get; set; }
        public string? Deliveryunit { get; set; }
        public DateTime? Datereceived { get; set; }
        public int? Quantityinapallet { get; set; }
    }

    public class ProductBasedDispatching
    {
        public int? Id { get; set; }
        public string? Productname { get; set; }
        public string? Productpackaging { get; set; }
        public string? Deliveryunit { get; set; }
        public DateTime? Dispatchdate { get; set; } = null;
        public int? Quantity { get; set; }
    }

    public class BasicProductResponse
    {
        public int Id { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
    }

    public class ProductSummary
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Companyname { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
        public string Productpackaging { get; set; }
        public string Deliveryunit { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
        public bool Active { get; set; }
        public bool Removed { get; set; }
    }
}
