namespace Blog_Api
{
    public class Blog
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public List<string>? Comment { get; set; }
    }
}