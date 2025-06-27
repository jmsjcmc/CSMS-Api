namespace CSMapi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int Customerid { get; set; }
        public Customer Customer { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
        public string Productpackaging { get; set; }
        public string Deliveryunit { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
        public bool Active { get; set; }
        public bool Removed { get; set; }
        public ICollection<Receiving> Receiving { get; set; }
        public ICollection<Dispatching> Dispatching { get; set; }
    }
}
