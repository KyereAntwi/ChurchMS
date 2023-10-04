using COPDistrictMS.Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace COPDistrictMS.Infrastructure.Services;

public class ImageService : IImageService
{
    public ImageService()
    {
        
    }

    public Task<Uri> UploadFileToFirebase(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
