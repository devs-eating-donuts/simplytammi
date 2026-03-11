namespace SimplyTammi.Models
{
    public class GalleryImage
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public string ThumbUrl { get; set; } = "";
        public string MediumUrl { get; set; } = "";
        public string OriginalUrl { get; set; } = "";
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
