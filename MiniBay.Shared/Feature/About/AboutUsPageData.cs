namespace MiniBay.Shared.Features.About
{
    public class AboutUsPageData
    {
        public BannerSection Banner { get; set; } = new();
        public IntroSection Introduction { get; set; } = new();
        public PillarsSection Pillars { get; set; } = new();
        public GallerySection Gallery { get; set; } = new();
    }

    public class BannerSection
    {
        public string Subtitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }

    public class IntroSection
    {
        public string Heading { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> Paragraphs { get; set; } = new();
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class PillarsSection
    {
        public string Title { get; set; } = string.Empty;
        public List<PillarItem> PillarItems { get; set; } = new();
    }

    public class PillarItem
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class GallerySection
    {
        public List<string> ImageUrls { get; set; } = new();
    }
}
