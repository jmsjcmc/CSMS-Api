using CSMapi.Models;

namespace CSMapi.Validators
{
    public class CustomerValidator
    {
        public void ValidateCustomerRequest(CustomerRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Firstname))
            {
                throw new ArgumentException("First Name required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lastname))
            {
                throw new ArgumentException("Last Name required.");
            }

            if (string.IsNullOrWhiteSpace(request.Position))
            {
                throw new ArgumentException("Position required.");
            }

            if (string.IsNullOrWhiteSpace(request.Companyname))
            {
                throw new ArgumentException("Company Name required.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email required.");
            }

            if (string.IsNullOrWhiteSpace(request.Phone))
            {
                throw new ArgumentException("Phone required.");
            }

            if (string.IsNullOrWhiteSpace(request.Companyaddress))
            {
                throw new ArgumentException("Company Address required.");
            }

            if (string.IsNullOrWhiteSpace(request.Companyemail))
            {
                throw new ArgumentException("Company Email required.");
            }

            if (string.IsNullOrWhiteSpace(request.Companynumber))
            {
                throw new ArgumentException("Company Number required.");
            }
        }
    }
}
