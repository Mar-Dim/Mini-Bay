using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiniBay.Application.Contracts.Infrastructure
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string containerName);
    }
}