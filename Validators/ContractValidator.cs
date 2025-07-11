using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Validators
{
    public class ContractValidator
    {
        private readonly AppDbContext _context;
        public ContractValidator(AppDbContext context)
        {
            _context = context;
        }
        public async Task ValidateContractRequest(ContractRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Agreementdate))
            {
                throw new ArgumentException("Agreement Date required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lessorcompany))
            {
                throw new ArgumentException("Lessor Company required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lessorrepresentative))
            {
                throw new ArgumentException("Lessor Representative required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lessorrepresentativeposition))
            {
                throw new ArgumentException("Lessor Represetative Position required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lessorcompanylocation))
            {
                throw new ArgumentException("Lessor Company Location required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lesseecompany))
            {
                throw new ArgumentException("Lessee Company required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lesseerepresentative))
            {
                throw new ArgumentException("Lessee Representative required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lesseerepresentativeposition))
            {
                throw new ArgumentException("Lessee Representative Position required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lesseecompanylocation))
            {
                throw new ArgumentException("Lessee Company Location required.");
            }

            if (string.IsNullOrWhiteSpace(request.Notarylocation))
            {
                throw new ArgumentException("Notary Location required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lessoridtype))
            {
                throw new ArgumentException("Lessor ID type required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lessoriddetail))
            {
                throw new ArgumentException("Lessor ID detail required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lesseeidtype))
            {
                throw new ArgumentException("Lessee ID type required.");
            }

            if (string.IsNullOrWhiteSpace(request.Lesseeiddetail))
            {
                throw new ArgumentException("Lessee ID detail required.");
            }

            if (string.IsNullOrWhiteSpace(request.Sealdate))
            {
                throw new ArgumentException("Seal Date required.");
            }
        }

        public async Task ValidateFetchContract (int id)
        {
            var contract = await _context.Contracts
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null)
            {
                throw new ArgumentException($"Contract id {id} not found.");
            }
        }
    }
}
