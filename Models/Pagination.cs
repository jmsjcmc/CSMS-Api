namespace CSMapi.Models
{
    public class Pagination<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Totalcount { get; set; }
        public int Pagenumber { get; set; }
        public int Pagesize { get; set; }
    }
}
