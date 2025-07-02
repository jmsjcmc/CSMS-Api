using CSMapi.Models;

namespace CSMapi.Validators
{
    public class ProductValidator
    {
        public async Task ValidateProductRequest(ProductRequest request)
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
