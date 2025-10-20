using MiniBay.Shared.Features.About;
using MiniBay.Application.Interfaces;
using MiniBay.Application.Features.About;
namespace MiniBay.Infrastructure.Services
{
    public class AboutService : IAboutService
    {
        public AboutUsPageData GetAboutPageData()
        {
  
            return new AboutUsPageData
            {
                Banner = new BannerSection { Subtitle = "Nuestra Misión", Title = "Conecta, Vende <br/> y Descubre" },
                Introduction = new IntroSection
                {
                    Heading = "Nuestra Esencia",
                    Title = "Un Mercado Impulsado por la Comunidad",
                    Paragraphs = new List<string>
                    {
                        "En el corazón de MiniBay, creemos que cada objeto tiene una historia y un valor.",
                        "Con MiniBay, subir un producto con su imagen y descripción es cuestión de minutos."
                    },
                    ImageUrl = "https://images.pexels.com/photos/7988086/pexels-photo-7988086.jpeg"
                },
                Pillars = new PillarsSection
                {
                    Title = "Nuestros Pilares",
                    PillarItems = new List<PillarItem>
                    {
                        new PillarItem { Icon = "fa-solid fa-cloud-arrow-up", Title = "Venta Simplificada", Description = "Publicar un artículo es tan fácil como tomar una foto." },
                        new PillarItem { Icon = "fa-solid fa-shield-halved", Title = "Compras Seguras", Description = "Protegemos cada transacción." },
                        new PillarItem { Icon = "fa-solid fa-users", Title = "Comunidad Conectada", Description = "Somos más que un mercado." },
                        new PillarItem { Icon = "fa-solid fa-gem", Title = "Descubrimientos Únicos", Description = "Explora artículos únicos." }
                    }
                },
                Gallery = new GallerySection
                {
                    ImageUrls = new List<string>
                    {
                        "https://images.pexels.com/photos/276528/pexels-photo-276528.jpeg?auto=compress&cs=tinysrgb&w=600",
                        "https://images.pexels.com/photos/5632398/pexels-photo-5632398.jpeg?auto=compress&cs=tinysrgb&w=600",
                        "https://images.pexels.com/photos/1034664/pexels-photo-1034664.jpeg?auto=compress&cs=tinysrgb&w=600",
                        "https://images.pexels.com/photos/264636/pexels-photo-264636.jpeg?auto=compress&cs=tinysrgb&w=600",
                        "https://images.pexels.com/photos/4123899/pexels-photo-4123899.jpeg?auto=compress&cs=tinysrgb&w=600",
                        "https://images.pexels.com/photos/276528/pexels-photo-276528.jpeg?auto=compress&cs=tinysrgb&w=600",
                        "https://images.pexels.com/photos/5632398/pexels-photo-5632398.jpeg?auto=compress&cs=tinysrgb&w=600",
                        "https://images.pexels.com/photos/1034664/pexels-photo-1034664.jpeg?auto=compress&cs=tinysrgb&w=600"
                    }
                }
            };
        }
    }
}
