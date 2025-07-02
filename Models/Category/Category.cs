namespace CSMapi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Removed { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
