using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CSMapi.Helpers.Excel
{
    public class PalletExcel : ExcelHelper
    {
        private readonly string[] palletheader =
        {
            "Pallet Type", "Pallet Number"
        };

        private readonly string[] positionHeader =
        {
             "Cold Storage Number", "Wing", "Floor",
             "Column", "Side"
        };
        public PalletExcel(AppDbContext context) : base (context)
        {
            
        }

        public byte[] generatepallettemplate()
        {
            var workSheet = createworksheet("Pallet Template");

            for (int i = 0; i < palletheader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = palletheader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
                workSheet.Column(i + 1).AdjustToContents();
            }

            for (int i = 2; i <= 100; i++)
            {
                workSheet.Cell(i, 1).Value = "Plastic";
                workSheet.Cell(i, 2).Value = 123123 + i;
            }

            save();
            return getbytes();
        }

        public byte[] generatepalletposition()
        {
            var workSheet = createworksheet("Pallet Position");
            var row = 2;
            for (int i = 0; i < positionHeader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = positionHeader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
                workSheet.Column(i + 1).AdjustToContents();
            }

            workSheet.Cell(row, 1).Value = 1;
            workSheet.Cell(row, 2).Value = "Left";
            workSheet.Cell(row, 3).Value = "1";
            workSheet.Cell(row, 4).Value = "1";
            workSheet.Cell(row, 5).Value = "Front";

            save();
            return getbytes();
        }

        public byte[] exportpallets(IEnumerable<Pallet> pallets)
        {
            var workSheet = createworksheet("Pallets");
            int row = 2;

            for (int i = 0; i < palletheader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = palletheader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            foreach (var pallet in pallets)
            {
                var values = new object[]
                {
             pallet.Pallettype, pallet.Palletno
                };
                for (int col = 0; col < values.Length; col++)
                    workSheet.Cell(row, col + 1).Value = values[col]?.ToString();
                row++;
            }

            workSheet.Columns().AdjustToContents();
            save();
            return getbytes();
        }

        public byte[] exportpositions(IEnumerable<PalletPosition> positions)
        {
            var workSheet = createworksheet("Pallet Positions");
            int row = 2;

            for (int i = 0; i < positionHeader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = positionHeader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            foreach (var position in positions)
            {
                var cs = _context.Coldstorages
                    .FirstOrDefaultAsync(c => c.Id == position.Csid);


                var values = new object[]
                {
             cs, position.Wing, position.Floor, position.Column,
             position.Side
                };
                for (int col = 0; col < values.Length; col++)
                    workSheet.Cell(row, col + 1).Value = values[col]?.ToString();
            }
            workSheet.Columns().AdjustToContents();
            save();
            return getbytes();
        }

        public List<Pallet> importpallets(IFormFile file, ClaimsPrincipal user)
        {
            var pallets = new List<Pallet>();

            var rows = getworksheetrows(file);

            foreach (var row in rows)
            {
                pallets.Add(new Pallet
                {
                    Pallettype = row.Cell(1).GetValue<string>(),
                    Palletno = row.Cell(2).GetValue<int>(),
                    Createdon = TimeHelper.GetPhilippineStandardTime(),
                    Creatorid = AuthUserHelper.GetUserId(user),
                    Occupied = false,
                    Active = true,
                    Removed = false
                });
            }
            return pallets;
        }

        public async Task<List<PalletPosition>> importpositions(IFormFile file)
        {
            var positions = new List<PalletPosition>();
            var rows = getworksheetrows(file);

            foreach (var row in rows)
            {
                string csNumber = row.Cell(1).GetValue<string>();
                var cs = await _context.Coldstorages
                    .FirstOrDefaultAsync(c => c.Csnumber == csNumber);

                positions.Add(new PalletPosition
                {
                    Csid = cs.Id,
                    Wing = row.Cell(2).GetValue<string>(),
                    Floor = row.Cell(3).GetValue<string>(),
                    Column = row.Cell(4).GetValue<string>(),
                    Side = row.Cell(5).GetValue<string>()
                });
            }

            return positions;
        }
    }
}
