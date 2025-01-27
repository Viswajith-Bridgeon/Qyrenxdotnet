namespace Qyrenx.Business.Services.EmailServices
{
    public interface IEmailServices
    {
        Task<bool> sendOtp(string email);
        bool verifyOtp(string email, string otp);

        Task<bool> SendVerifiedmsg(string role,string name,string toAddress);

        Task<bool>  ResetPasswordOtp(string email);

        Task<bool> SendOtpForDeliveryBoyVerification(string UserEmail);

        Task<bool>  UserToDeliverPersonVerifyOtp(string UserEmail,string otp);

        Task<bool> SendOtpForVendorVerification(string vendorEmail);


    }
}
