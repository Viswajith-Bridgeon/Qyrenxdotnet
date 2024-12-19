namespace Qyrenx.Services.EmailServices
{
    public interface IEmailServices
    {
        Task<bool> sendOtp(string email);
        bool verifyOtp(string email, string otp);

        Task<bool> SendVerifiedmsg(string role,string name,string toAddress);

        Task<bool>  ResetPasswordOtp(string email);

    }
}
