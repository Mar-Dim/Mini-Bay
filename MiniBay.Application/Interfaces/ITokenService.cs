using MiniBay.Domain.Entities;

namespace MiniBay.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}