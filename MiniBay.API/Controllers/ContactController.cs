using Microsoft.AspNetCore.Mvc;
using MiniBay.Application.Features.Contact;
using MiniBay.Shared.Features.Contact;
using System.Threading.Tasks;


namespace MiniBay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<ContactInfoDto>> GetContactInfo()
        {
            var result = await _contactService.GetContactInfoAsync();
            return Ok(result);
        }
    }
}