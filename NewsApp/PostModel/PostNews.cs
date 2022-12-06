namespace NewsApp.PostModel
{
    public class PostNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Disciription { get; set; }
        public IFormFile Image { get; set; }
    }
}
