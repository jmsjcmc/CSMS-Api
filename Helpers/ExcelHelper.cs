using ClosedXML.Excel;

namespace CSMapi.Helpers
{
    public class ExcelHelper 
    {
        protected readonly AppDbContext _context;
        protected readonly XLWorkbook _workbook;
        protected readonly MemoryStream _stream;
        public ExcelHelper(AppDbContext context)
        {
            _context = context;
            _workbook = new XLWorkbook();
            _stream = new MemoryStream();
        }

        protected IXLWorksheet createworksheet(string sheetName)
        {
            return _workbook.Worksheets.Add(sheetName);
        }

        protected void save()
        {
            _workbook.SaveAs(_stream);
        }

        protected byte[] getbytes()
        {
            return _stream.ToArray();
        }

        protected IEnumerable<IXLRow> getworksheetrows(IFormFile file, int skipRows = 1)
        {
            var stream = new MemoryStream();
            file.CopyTo(stream);
            var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();
            return worksheet.RowsUsed().Skip(skipRows);
        }
    }
}
