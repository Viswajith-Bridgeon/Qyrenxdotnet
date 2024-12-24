namespace Qyrenx.Business.Services.JwtServices
{
    public interface IJwtService
    {
        string GenerateJwt(Guid Id, string Email, string role);
    }
}
