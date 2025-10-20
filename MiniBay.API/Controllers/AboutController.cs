using Microsoft.AspNetCore.Mvc;
using MiniBay.Application.Interfaces;
using MiniBay.Application.Features.About;
using MiniBay.Shared.Features.About; 
namespace MiniBay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AboutController : ControllerBase
{
    private readonly IAboutService _aboutService;

    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    [HttpGet]
    // El tipo de retorno ActionResult<AboutUsPageData> ahora encontrará la clase
    // gracias al 'using' corregido.
    public ActionResult<AboutUsPageData> Get()
    {
        var data = _aboutService.GetAboutPageData();
        return Ok(data);
    }
}