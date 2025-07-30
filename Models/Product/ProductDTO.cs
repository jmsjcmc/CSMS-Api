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


    public class ProductValidator
    {
        public void ValidateProductRequest(ProductRequest request)
        {
            if (request.Categoryid <= 0)
            {
                throw new ArgumentException("Category Id required.");
            }

            if (request.Customerid < 0)
            {
                throw new ArgumentException("Customer Id required and must be valid identifier.");
            }

            if (string.IsNullOrWhiteSpace(request.Productcode))
            {
                throw new ArgumentException("Product Code required.");
            }

            if (string.IsNullOrWhiteSpace(request.Productname))
            {
                throw new ArgumentException("Product Name required.");
            }

            if (string.IsNullOrWhiteSpace(request.Variant))
            {
                throw new ArgumentException("Variant required.");
            }

            if (string.IsNullOrWhiteSpace(request.Sku))
            {
                throw new ArgumentException("Sku required.");
            }

            if (string.IsNullOrWhiteSpace(request.Productpackaging))
            {
                throw new ArgumentException("Product Packaging required.");
            }

            if (string.IsNullOrWhiteSpace(request.Deliveryunit))
            {
                throw new ArgumentException("Delivery Unit required.");
            }

            if (request.Quantity < 0)
            {
                throw new ArgumentException("Quantity required and must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(request.Uom))
            {
                throw new ArgumentException("Unit of measurement required.");
            }

            if (request.Weight < 0)
            {
                throw new ArgumentException("Weight required.");
            }

            if (string.IsNullOrWhiteSpace(request.Unit))
            {
                throw new ArgumentException("Unit required.");
            }
        }
    }
}
