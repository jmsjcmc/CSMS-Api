using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Validators
{
    public class PalletValidator
    {
        private readonly AppDbContext _context;
        public PalletValidator(AppDbContext context)
        {
            _context = context;   
        }
        public async Task ValidatePalletRequest(PalletRequest request)
        {
            if (await _context.Pallets.AnyAsync(p => p.Palletno == request.Palletno))
            {
                throw new ArgumentException("Pallet Number taken.");
            }
            if (string.IsNullOrWhiteSpace(request.Pallettype))
            {
                throw new ArgumentException("Pallet Type required.");
            }
            if (request.Palletno < 0)
            {
                throw new ArgumentException("Pallet Number required and must be greater than zero.");
            }
        }
        public async Task ValidatePalletUpdateRequest(PalletRequest request, int id)
        {
            var pallet = await _context.Pallets
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pallet == null)
            {
                throw new ArgumentException($"Pallet id {id} not found.");
            }
        }
        public void ValidatePalletPosition(PalletPositionRequest request)
        {
            if (request.Csid < 0)
            {
                throw new ArgumentException("Cs Id required and must be valid identifier.");
            }
            if (string.IsNullOrWhiteSpace(request.Wing))
            {
                throw new ArgumentException("Wing required.");
            }
            if (string.IsNullOrWhiteSpace(request.Floor))
            {
                throw new ArgumentException("Floor required.");
            }
            if (string.IsNullOrWhiteSpace(request.Column))
            {
                throw new ArgumentException("Column required.");
            }
            if (string.IsNullOrWhiteSpace(request.Side))
            {
                throw new ArgumentException("Side required");
            }
        }
    }
}
