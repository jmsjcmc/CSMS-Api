using ClosedXML.Excel;
using CSMapi.Models;

namespace CSMapi.Helpers.Excel
{
    public class ReceivingExcel : ExcelHelper
    {
        private readonly string[] receivingheader =
        {
            "Category", "Document Number", "Product Name",
            "Expiration Date", "Cv Number", "Plate Number",
            "Arrival Date", "Unloading Start", "Unloading End",
            "Overall Weight", "Note", "Date Received",
            "Date Declined", "Pending", "Received",
            "Declined", "Requestor",
            "Approver"
        };
        public readonly string[] detailHeader =
        {
            "Product Name", "Pallet Number", "Wing",
            "Floor", "Column", "Side",
            "Production Date", "Quantity", "Total Weight",
            "Full Dispatched", "Partial Dispatched"
        };
        public ReceivingExcel(AppDbContext context) : base (context)
        {
            
        }
        // Receiving template method
        public byte[] generatereceivingtemplate()
        {
            var worksheet = createworksheet("Receiving Template");

            for (int i = 0; i < receivingheader.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = receivingheader[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                worksheet.Column(i + 1).AdjustToContents();
            }
            save();
            return getbytes();
        }
        // Export receivings method
        public async Task<byte[]> exportreceivings(IEnumerable<Receiving> receivings)
        {
            var worksheet = createworksheet("Receivings");
            int row = 2;
            for (int i = 0; i < receivingheader.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = receivingheader[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            foreach (var receiving in receivings)
            {
                var productName = $"{receiving.Product.Productcode} - {receiving.Product.Productname} - {receiving.Product.Variant} - {receiving.Product.Sku}";
                var requestor = $"{receiving.Requestor.Firstname} {receiving.Requestor.Lastname}";
                var approver = receiving.Approver != null
                    ? $"{receiving.Approver.Firstname} {receiving.Approver.Lastname}"
                    : "";

                var values = new object[]
                {
                    receiving.Product.Category, 
                    receiving.Document.Documentno, 
                    productName,
                    receiving.Expirationdate.ToString("MM-dd-yyyy"), 
                    receiving.Cvnumber, 
                    receiving.Platenumber,
                    receiving.Arrivaldate.ToString("MM-dd-yyyy"), 
                    receiving.Unloadingstart, 
                    receiving.Unloadingend,
                    receiving.Overallweight, 
                    receiving.Note, 
                    receiving.Datereceived,
                    receiving.Datedeclined,
                    receiving.Pending, 
                    receiving.Received, 
                    receiving.Declined,
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
            var detail = createworksheet("ReceivingDetails");

            for (int i = 0; i < detailHeader.Length; i++)
            {
                detail.Cell(1, i + 1).Value = detailHeader[i];
                detail.Cell(1, i + 1).Style.Font.Bold = true;
            }

            foreach (var receiving in receivings)
            {
                var productName = $"{receiving.Product.Productcode} - {receiving.Product.Productname} - {receiving.Product.Variant} - {receiving.Product.Sku}";
                foreach (var receivingDetail in receiving.Receivingdetails)
                {
                    var values = new object[]
                    {
                        productName, 
                        receivingDetail.Pallet.Palletno, 
                        receivingDetail.PalletPosition.Wing,
                        receivingDetail.PalletPosition.Floor, 
                        receivingDetail.PalletPosition.Column, 
                        receivingDetail.PalletPosition.Side,
                        receivingDetail.Productiondate.ToString("MM-dd-yyyy"),
                        receivingDetail.Quantityinapallet, 
                        receivingDetail.Totalweight,
                        receivingDetail.Fulldispatched,
                        receivingDetail.Partialdispatched
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
