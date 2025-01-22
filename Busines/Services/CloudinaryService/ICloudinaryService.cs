using Microsoft.AspNetCore.Http;

namespace Qyrenx.Business.Services.CloudinaryService
{
	public interface ICloudinaryService
	{
		Task<string> UploadDocumentAsync(IFormFile file);

	}
}
