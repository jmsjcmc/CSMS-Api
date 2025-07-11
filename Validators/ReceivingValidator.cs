using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Validators
{
    public class ReceivingValidator
    {
        private readonly AppDbContext _context;
        public ReceivingValidator(AppDbContext context)
        {
            _context = context;
        }
        public async Task ValidateReceivingRequest(ReceivingRequest request)
        {

            if (string.IsNullOrWhiteSpace(request.Documentno))
            {
                throw new ArgumentException("Document Number required.");
            }

            if(await _context.Receivings.AnyAsync(r => r.Document.Documentno == request.Documentno))
            {
                throw new ArgumentException("Document Number required.");
            }

            if (request.Productid < 0)
            {
                throw new ArgumentException("Product Id required and must be valid identifier.");
            }

            if (request.Expirationdate == DateTime.MinValue)
            {
                throw new ArgumentException("Expiration Date not valid");
            }

            if (string.IsNullOrWhiteSpace(request.Cvnumber))
            {
                throw new ArgumentException("Cv Number required.");
            }

            if (string.IsNullOrWhiteSpace(request.Platenumber))
            {
                throw new ArgumentException("Plate Number required.");
            }

            if (request.Arrivaldate == DateTime.MinValue)
            {
                throw new ArgumentException("Arrival Date not valid.");
            }

            if (string.IsNullOrWhiteSpace(request.Unloadingstart))
            {
                throw new ArgumentException("Unloading Start required.");
            }

            if (string.IsNullOrWhiteSpace(request.Unloadingend))
            {
                throw new ArgumentException("Unloading End required.");
            }

            if (request.Overallweight < 0)
            {
                throw new ArgumentException("Overall Weight required and must be greater than zero.");
            }

            if (request.ReceivingDetail == null)
            {
                throw new ArgumentException("Atleast one receiving detail is required.");
            }

            foreach (var detail in request.ReceivingDetail)
            {
                
                if (detail.Palletid <= 0)
                {
                    throw new ArgumentException("Pallet Id required and must be valid identifier.");
                }

                if (detail.Quantityinapallet <= 0)
                {
                    throw new ArgumentException("Quantity required and must be greater than zero.");
                }

                if (detail.Totalweight <= 0)
                {
                    throw new ArgumentException("Total Weight required and must be greater than zero.");
                }
            }
        }

        public async Task ValidateCategoryRequest(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("Category required.");
            }
        }

        public int GetNextSequence(IEnumerable<string> documentNos)
        {
            return documentNos
                .Select(doc =>
                {
                    var parts = doc.Split('-');
                    return parts.Length == 3 && int.TryParse(parts[2], out var number) ? number : 0;
                })
                .DefaultIfEmpty(0)
                .Max() + 1;
        }

        public string? GetPrefixByCategory(string category) =>
            category.Trim().ToLower() switch
            {
                "raw mats" => "R2",
                "fresh goods" => "R3",
                _ => null
            };
    }
}
