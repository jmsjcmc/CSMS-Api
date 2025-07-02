using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Excel
{
    public class ProductExcel : ExcelHelper
    {
        private readonly string[] productHeader =
        {
             "Company", "Product Code", "Product Name",
             "Variant", "Sku", "Category", 
             "Product Packaging", "Delivery Unit", "Quanntity", 
             "Unit of Measurement","Weight", "Unit"
         };
        public ProductExcel(AppDbContext context) : base (context)
        {
            
        }

        public byte[] generateproducttemplate()
        {
            var workSheet = createworksheet("Product Template");

            for (int i = 0; i < productHeader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = productHeader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
                workSheet.Column(i + 1).AdjustToContents();
            }

            var sku = new[] { "250g", "500g", "1kg" };
            var companies = new[] { "FreshCo", "FarmPro", "AgriBest", "GreenHarvest" };
            var packaging = new[] { "Can", "Bottle", "Plastic Bag", "Pack", "Galantina" };
            var deliveryUnit = new[] { "Box", "Sack", "Drum" };
            var random = new Random();

            for (int i = 2; i <= 100; i++)
            {
                string selectedPackaging = packaging[random.Next(packaging.Length)];

                workSheet.Cell(i, 1).Value = companies[random.Next(companies.Length)];
                workSheet.Cell(i, 2).Value = $"PRD-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
                workSheet.Cell(i, 3).Value = $"Product {random.Next(100, 999)}";
                workSheet.Cell(i, 4).Value = "N/A";
                workSheet.Cell(i, 5).Value = sku[random.Next(sku.Length)];
                workSheet.Cell(i, 6).Value = "Fresh Goods";
                workSheet.Cell(i, 7).Value = selectedPackaging;
                workSheet.Cell(i, 8).Value = "Box";
                workSheet.Cell(i, 9).Value = random.Next(1, 100);
                workSheet.Cell(i, 10).Value = selectedPackaging;
                workSheet.Cell(i, 11).Value = Math.Round(random.NextDouble() * 50 + 1, 2);
                workSheet.Cell(i, 12).Value = "KG";
            }

            save();
            return getbytes();
        }

        public byte[] exportproducts(IEnumerable<Product> products)
        {
            var workSheet = createworksheet("Products");
            int row = 2;

            for (int i = 0; i < productHeader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = productHeader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            foreach (var product in products)
            {
                var values = new object[]
                {
             product.Customer.Companyname, product.Productcode, product.Productname,
             product.Variant, product.Sku, product.Category,
             product.Productpackaging, product.Deliveryunit,  product.Quantity, 
             product.Uom, product.Weight, product.Unit
                };
                for (int col = 0; col < values.Length; col++)
                    workSheet.Cell(row, col + 1).Value = values[col]?.ToString();
                row++;
            }
            workSheet.Columns().AdjustToContents();
            save();
            return getbytes();
        }

        public async Task<List<Product>> importproducts(IFormFile file)
        {
            var products = new List<Product>();
            var rows = getworksheetrows(file);

            foreach (var row in rows)
            {
                var companyName = row.Cell(1).GetValue<string>();
                var categoryName = row.Cell(6).GetValue<string>();
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Companyname.ToLower() == companyName.ToLower());
                products.Add(new Product
                {
                    Customerid = customer.Id,
                    Productcode = row.Cell(2).GetValue<string>(),
                    Productname = row.Cell(3).GetValue<string>(),
                    Variant = row.Cell(4).GetValue<string>(),
                    Sku = row.Cell(5).GetValue<string>(),
                    Categoryid = category.Id,
                    Productpackaging = row.Cell(7).GetValue<string>(),
                    Deliveryunit = row.Cell(8).GetValue<string>(),
                    Quantity = row.Cell(9).GetValue<int>(),
                    Uom = row.Cell(10).GetValue<string>(),
                    Weight = row.Cell(11).GetValue<double>(),
                    Unit = row.Cell(12).GetValue<string>(),
                    Active = true
                });
            }
            return products;
        }
    }
}
