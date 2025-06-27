using CSMapi.Models;

namespace CSMapi.Helpers.Excel
{
    public class CustomerExcel : ExcelHelper
    {
        private readonly string[] customerHeader =
        {
             "First Name", "Last Name", "Position",
             "Company", "Email", "Phone",
             "Company Address", "Company Email", "Company Number"
         };
        public CustomerExcel(AppDbContext context) : base (context)
        {
            
        }

        public byte[] generatecustomertemplate()
        {
            var workSheet = createworksheet("Customer Template");

            for (int i = 0; i < customerHeader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = customerHeader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
                workSheet.Column(i + 1).AdjustToContents();
            }

            var firstName = new[] { "John", "Jane", "Alex", "Chris", "Sarah", "Michael", "Emma", "David", "Olivia", "Ethan" };
            var lastName = new[] { "Doe", "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Martinez", "Davis", "Rodriguez" };
            var position = new[] { "Manager", "Salesperson", "CEO" };
            var companyName = new[] { "FreshCo", "FarmPro", "AgriBest", "GreenHarvest", "Bounty Fresh Food Inc.", "Century Pacific Food, Inc.", "Agripacific Corporation" };
            var address = new[] { "123 Main St, Springfield", "456 Oak Rd, Rivertown", "789 Pine Ave, Lakeside", "101 Maple Dr, Hilltop", "202 Birch Blvd, Sunnyside" };

            var random = new Random();
            var usedCompanyNames = new HashSet<string>();

            for (int i = 2; i <= 8; i++)
            {
                string selectedFirstName = firstName[random.Next(firstName.Length)];
                string selectedLastName = lastName[random.Next(lastName.Length)];
                string selectedCompanyName;

                do
                {
                    selectedCompanyName = companyName[random.Next(companyName.Length)];
                }
                while (usedCompanyNames.Contains(selectedCompanyName));
                usedCompanyNames.Add(selectedCompanyName);

                string email = $"{selectedFirstName}.{selectedLastName}@{selectedCompanyName.Replace(" ", "").Replace(",", "")}.com";
                string companyEmail = $"{selectedCompanyName.Replace(" ", "").Replace(",", "")}@{selectedCompanyName.Replace(" ", "").Replace(",", "")}.com";

                workSheet.Cell(i, 1).Value = selectedFirstName;
                workSheet.Cell(i, 2).Value = selectedLastName;
                workSheet.Cell(i, 3).Value = position[random.Next(position.Length)];
                workSheet.Cell(i, 4).Value = selectedCompanyName;
                workSheet.Cell(i, 5).Value = email;
                workSheet.Cell(i, 6).Value = random.Next(10000000, 99999999);
                workSheet.Cell(i, 7).Value = address[random.Next(address.Length)];
                workSheet.Cell(i, 8).Value = companyEmail;
                workSheet.Cell(i, 9).Value = random.Next(10000000, 99999999);
            }

            save();
            return getbytes();
        }

        public byte[] exportcustomers(IEnumerable<Customer> customers)
        {
            var workSheet = createworksheet("Customers");
            int row = 2;

            for (int i = 0; i < customerHeader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = customerHeader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            foreach (var customer in customers)
            {
                var values = new object[]
                {
                     customer.Firstname, customer.Lastname, customer.Position,
                     customer.Companyname, customer.Email, customer.Phone,
                     customer.Companyaddress, customer.Companyemail, customer.Companynumber
                };
                for (int col = 0; col < values.Length; col++)
                    workSheet.Cell(row, col + 1).Value = values[col]?.ToString();
                row++;
            }

            workSheet.Columns().AdjustToContents();
            save();
            return getbytes();
        }

        public List<Customer> importcustomer(IFormFile file)
        {
            var customers = new List<Customer>();

            var rows = getworksheetrows(file);

            foreach (var row in rows)
            {
                customers.Add(new Customer
                {
                    Firstname = row.Cell(1).GetValue<string>(),
                    Lastname = row.Cell(2).GetValue<string>(),
                    Position = row.Cell(3).GetValue<string>(),
                    Companyname = row.Cell(4).GetValue<string>(),
                    Email = row.Cell(5).GetValue<string>(),
                    Phone = row.Cell(6).GetValue<string>(),
                    Companyaddress = row.Cell(7).GetValue<string>(),
                    Companyemail = row.Cell(8).GetValue<string>(),
                    Companynumber = row.Cell(9).GetValue<string>(),
                    Active = true
                });
            }
            return customers;
        }
    }
}
