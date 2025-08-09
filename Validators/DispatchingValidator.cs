using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Validators
{
    public class DispatchingValidator
    {
        private readonly AppDbContext _context;
        public DispatchingValidator(AppDbContext context)
        {
            _context = context;
        }
        public async Task ValidateDispatchingRequest(DispatchingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("Dispatching request required.");
            }

            if (request.Productid <= 0)
            {
                throw new ArgumentException("Product Id required and must be valid identifier.");
            }

            if (string.IsNullOrWhiteSpace(request.Documentno))
            {
                throw new ArgumentException("Document Number required.");
            }

            if (await _context.Dispatchings.AnyAsync(d => d.Document.Documentno == request.Documentno))
            {
                throw new ArgumentException("Document Number taken.");
            }

            if (string.IsNullOrWhiteSpace(request.Nmiscertificate))
            {
                throw new ArgumentException("Nmis Certificate required.");
            }

            if (string.IsNullOrWhiteSpace(request.Dispatchplateno))
            {
                throw new ArgumentException("Dispatch Plate Number required.");
            }

            if (string.IsNullOrWhiteSpace(request.Sealno))
            {
                throw new ArgumentException("Seal Number required.");
            }

            if (request.Overallweight <= 0)
            {
                throw new ArgumentException("Overall Weight required and must be greater than zero.");
            }

            if (request.DispatchingDetail == null)
            {
                throw new ArgumentException("Atleast one dispatching detail is required.");
            }

            foreach (var detail in request.DispatchingDetail)
            {
                if (detail.Receivingdetailid <= 0)
                {
                    throw new ArgumentException("Receiving detail id required and must be valid identifier.");
                }

                if (detail.Palletid <= 0)
                {
                    throw new ArgumentException("Pallet id required and must be valid identifier.");
                }

                if (detail.Quantity <= 0)
                {
                    throw new ArgumentException("Quantity required and must be greater than zero.");
                }

                if (detail.Totalweight <= 0)
                {
                    throw new ArgumentException("Total Weight required and must be greater thann zero.");
                }

                bool palletExist = await _context.Pallets
                    .AnyAsync(p => p.Id == detail.Palletid);
                if (!palletExist)
                {
                    throw new ArgumentException($"Pallet Id {detail.Palletid} not found.");
                }
            }
        }
        public async Task ValidateSpecificDispatching(int id)
        {
            if (!await _context.Dispatchings.AnyAsync(d => d.Id == id))
            {
                throw new ArgumentException($"Dispatching ID {id} not found.");
            }

            if (!await _context.Dispatchingdetails.AnyAsync(d => d.Id == id))
            {
                throw new ArgumentException($"Dispatching Detail ID {id} not found.");
            }
        }
    }
}
