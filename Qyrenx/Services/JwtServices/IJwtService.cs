namespace Qyrenx.Services.JwtServices
{
    public interface IJwtService
    {
        string GenerateJwt(Guid Id, string Email, string role);
    }
}
