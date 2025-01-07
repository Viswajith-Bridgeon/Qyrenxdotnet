using Qyrenx.Dataccess.Models.Entities;

namespace Qyrenx.Business.Services.JwtServices
{
    public interface IJwtService
    {
        string GenerateJwt(Guid Id, string Email, string role);

        Task<string> CreaterefreshToken(Guid Id, string Email, string role);


    }
}
