using ClosedXML.Excel;
using CSMapi.Models;

namespace CSMapi.Helpers.Excel
{
    public class DispatchingExcel : ExcelHelper
    {
        private readonly string[] dispatchingheader =
        {
            "Category", "Document Number", "Product Name",
            "Dispatch Time Start", "Dispatch Time End", "NMIS Certificate",
            "Dispatch Plate Number", "Seal Number", "Overall Weight",
            "Temperature", "Production Date", "Note",
            "Dispatched", "Declined", "Pending",
            "Requestor", "Approver"
        };

        private readonly string[] detailheader =
        {
            "Product Name", "Pallet Number", "Wing",
            "Floor", "Column", "Side",
            "Quantity", "Total Weight", "Full Dispatched",
            "Partial Dispatched"
        };
        public DispatchingExcel(AppDbContext context) : base(context)
        {

        }
        public async Task<byte[]> exportdispatching(IEnumerable<Dispatching> dispatchings)
        {
            var worksheet = createworksheet("Dispatchings");
            int row = 2;
            for (int i = 0; i < dispatchingheader.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = dispatchingheader[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            foreach (var dispatching in dispatchings)
            {
                var productname = $"{dispatching.Product.Productcode} - {dispatching.Product.Productname} - {dispatching.Product.Variant} - {dispatching.Product.Sku}";
                var requestor = $"{dispatching.Requestor.Firstname} {dispatching.Requestor.Lastname}";
                var approver = dispatching.Approver != null
                    ? $"{dispatching.Approver.Firstname} {dispatching.Approver.Lastname}"
                    : "";

                var values = new object[]
                {
                    dispatching.Product.Category,
                    dispatching.Document.Documentno,
                    productname,
                    dispatching.Dispatchtimestart,
                    dispatching.Dispatchtimeend,
                    dispatching.Nmiscertificate,
                    dispatching.Dispatchplateno,
                    dispatching.Sealno,
                    dispatching.Overallweight,
                    dispatching.Temperature,
                    dispatching.Productiondate,
                    dispatching.Note,
                    dispatching.Dispatched,
                    dispatching.Pending,
                    dispatching.Declined,
                    requestor,
                    approver
                };
                for (int col = 0; col < values.Length; col++)
                {
                    var cell = worksheet.Cell(row, col + 1);
                    cell.Value = values[col]?.ToString();

                    if (values[col] is bool boolvalue)
                    {
                        cell.Value = "";
                        cell.Style.Fill.BackgroundColor = boolvalue
                            ? XLColor.Green
                            : XLColor.Red;
                    }
                }
                row++;
            }
            worksheet.Columns().AdjustToContents();
            var detail = createworksheet("DispatchingDetails");
            for(int i = 0; i < detailheader.Length; i++)
            {
                detail.Cell(1, i + 1).Value = detailheader[i];
                detail.Cell(1, i + 1).Style.Font.Bold = true;
            }

            foreach (var dispatching in dispatchings)
            {
                foreach(var dispatchingdetail in dispatching.Dispatchingdetails)
                {
                    var productname = $"{dispatching.Product.Productcode} - {dispatching.Product.Productname} - {dispatching.Product.Variant} - {dispatching.Product.Sku}";
                    var values = new object[]
                    {
                        productname,
                        dispatchingdetail.Pallet.Palletno,
                        dispatchingdetail.PalletPosition.Wing,
                        dispatchingdetail.PalletPosition.Floor,
                        dispatchingdetail.PalletPosition.Column,
                        dispatchingdetail.PalletPosition.Side,
                        dispatchingdetail.Quantity,
                        dispatchingdetail.Totalweight,
                        dispatchingdetail.Fulldispatched,
                        dispatchingdetail.Partialdispatched
                    };
                    for (int col = 0; col < values.Length; col++)
                    {
                        var cell = detail.Cell(row, col + 1);
                        cell.Value = values[col]?.ToString();
                        if (values[col] is bool boolvalue)
                        {
                            cell.Value = "";
                            cell.Style.Fill.BackgroundColor = boolvalue
                                ? XLColor.Green
                                : XLColor.Red;
                        }
                    }
                    row++;
                }
            }
            detail.Columns().AdjustToContents();
            save();
            return getbytes();
        }
    }
}
