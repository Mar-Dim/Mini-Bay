using MiniBay.Shared.Features.Contact;
using System.Threading.Tasks;

namespace MiniBay.Application.Features.Contact
{
    public interface IContactService
    {
        Task<ContactInfoDto> GetContactInfoAsync();
    }
}