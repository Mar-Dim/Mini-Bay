using MiniBay.Application.Features.Contact;
using MiniBay.Shared.Features.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBay.Application.Features.Contact
{
    public class ContactService : IContactService
    {
        public Task<ContactInfoDto> GetContactInfoAsync()
        {
            var contactInfo = new ContactInfoDto
            {
                Address = "123 Calle Ficticia, Ciudad Digital",
                SupportEmail = "soporte@minibay.com",
                CustomerServicePhone = "+1 (234) 567-890",
                Hours = new List<string>
                {
                    "Soporte Online: Lunes a Viernes, 9:00 - 18:00",
                    "Plataforma: Disponible 24/7 para compras y ventas"
                }
            };
            
            return Task.FromResult(contactInfo);
        }
    }
}