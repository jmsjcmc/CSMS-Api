namespace CSMapi.Models
{
    public class CategoryRequest
    {
        public string Name { get; set; }
    }
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Removed { get; set; }
    }
}
