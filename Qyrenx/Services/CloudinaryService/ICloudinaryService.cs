namespace Qyrenx.Services.CloudinaryService
{
	public interface ICloudinaryService
	{
		Task<string> UploadDocumentAsync(IFormFile file);

	}
}
