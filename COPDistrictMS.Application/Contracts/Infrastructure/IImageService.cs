using Microsoft.AspNetCore.Http;

namespace COPDistrictMS.Application.Contracts.Infrastructure;

public interface IImageService
{
    Task<Uri> UploadFileToFirebase(IFormFile file);
}