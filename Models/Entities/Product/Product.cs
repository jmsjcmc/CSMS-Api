namespace csms_backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
        public string Packaging { get; set; }
        public string DeliveryUnit { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
        public Status Status { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
