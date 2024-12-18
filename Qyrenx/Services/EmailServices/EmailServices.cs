using Qyrenx.ApplicationDbContext;
using System.Net.Mail;
using System.Net;
using static System.Net.WebRequestMethods;

namespace Qyrenx.Services.EmailServices
{
    public class EmailServices : IEmailServices
    {



        private static Dictionary<string, string> otpDictionary = new Dictionary<string, string>();


        public async Task<bool> sendOtp(string email)
        {
            try
            {
                string otp = GenerateOTP();

                await SendEmail(email, otp);
                otpDictionary[email] = otp;
                return true;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool verifyOtp(string email, string otp)
        {
            if (otpDictionary.ContainsKey(email) && otpDictionary[email] == otp)
            {
                otpDictionary.Remove(email);
                return true;
            }
            else
            {
                return false;
            }
        }


        private string GenerateOTP()
        {
            Random rand = new Random();
            int otp = rand.Next(100000, 999999);
            return otp.ToString();
        }

        public async Task SendEmail(string toAddress, string otp)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("qyrenxq@gmail.com", "zrdf tdrq wgxc ummo");
                smtpClient.EnableSsl = true;


                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("qyrenxq@gmail.com");
                mailMessage.To.Add(toAddress);
                mailMessage.Subject = "OTP Verification";
                //mailMessage.Body = "Your OTP for email verification is: " + otp;
                mailMessage.Body = GenerateEmailBody(otp);
                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        private string GenerateEmailBody(string otp)
        {
            string emailbody = string.Empty;



            emailbody += $@"
            <div style='width:100%; max-width:600px; margin:0 auto; font-family:Arial, sans-serif; background-color:#f9f9f9; padding:20px; border:1px solid #ddd; border-radius:8px;'>
                <div style='text-align:center;'>
                    <img src='https://res.cloudinary.com/dg9yt0gqk/image/upload/v1734424486/enidqlzb7nqyone7r8v2.webp' alt='Qyrenx Logo' style='margin-bottom:20px;' />
                </div>
                <h1 style='color:#333; text-align:center;'>Welcome to Qyrenx!</h1>
                <p style='font-size:16px; color:#555; text-align:center;'>
                    Thank you for registering with <strong>Qyrenx</strong>. We are excited to have you on board!
                </p>
                <p style='font-size:16px; color:#555; text-align:center;'>
                    Please use the One-Time Password (OTP) below to complete your registration.
                </p>
                <div style='margin:20px auto; text-align:center;'>
                    <span style='font-size:24px; color:#4CAF50; font-weight:bold; padding:10px 20px; border:2px dashed #4CAF50; display:inline-block; border-radius:8px;'>{otp}</span>
                </div>
                <p style='font-size:14px; color:#999; text-align:center; margin-top:20px;'>
                    If you did not request this email, please ignore it or contact our support team at <a href='mailto:qyrenxq@gmail.com' style='color:#007BFF;'>qyrenxq@gmail.com</a>.
                </p>
                <hr style='border:0; border-top:1px solid #ddd; margin:20px 0;' />
                <footer style='text-align:center; font-size:12px; color:#aaa;'>
                    &copy; 2024 Qyrenx. All rights reserved.<br />
                    Visit us at <a href='https://www.qyrenx.com' style='color:#007BFF;'>www.qyrenx.com</a>
                </footer>
            </div>";

            return emailbody;
        }



        public async Task<bool> SendVerifiedmsg(string role, string name,string toAddress)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("qyrenxq@gmail.com", "zrdf tdrq wgxc ummo");
                smtpClient.EnableSsl = true;
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("qyrenxq@gmail.com");
                mailMessage.To.Add(toAddress);
                mailMessage.Subject = "Vendor verification";

                mailMessage.Body = GenerateEmailBodyforVerify(role,name);
                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }


        private string GenerateEmailBodyforVerify(string role,string name)
        {
            string emailbody = string.Empty;
            emailbody += $@"
    <div style='width:100%; max-width:600px; margin:0 auto; font-family:Arial, sans-serif; background-color:#f9f9f9; padding:20px; border:1px solid #ddd; border-radius:8px;'>
        <div style='text-align:center;'>
            <img src='https://res.cloudinary.com/dg9yt0gqk/image/upload/v1734424486/enidqlzb7nqyone7r8v2.webp' alt='Qyrenx Logo' style='margin-bottom:20px;' />
        </div>
        <h1 style='color:#4CAF50; text-align:center;'>Verification Completed</h1>
        <p style='font-size:16px; color:#555; text-align:center;'>
            Dear <strong>{name}</strong>,
        </p>
        <p style='font-size:16px; color:#555; text-align:center;'>
            Congratulations! Your {role} verification process with <strong>Qyrenx</strong> has been successfully completed.
        </p>
        <p style='font-size:16px; color:#555; text-align:center;'>
            You are now a valued member of our {role} community. You can start accessing our platform and managing your profile.
        </p>
        <div style='text-align:center; margin:20px 0;'>
            <a href='https://www.qyrenx.com/vendor-dashboard' style='display:inline-block; padding:10px 20px; background-color:#4CAF50; color:#fff; text-decoration:none; border-radius:5px; font-size:16px;'>Go to Dashboard</a>
        </div>
        <p style='font-size:14px; color:#999; text-align:center;'>
            If you have any questions, feel free to contact us at <a href='mailto:qyrenxq@gmail.com' style='color:#007BFF;'>support@qyrenx.com</a>.
        </p>
        <hr style='border:0; border-top:1px solid #ddd; margin:20px 20px;' />
        <footer style='text-align:center; font-size:12px; color:#aaa;'>
            &copy; 2024 Qyrenx. All rights reserved.<br />
            Visit us at <a href='https://www.qyrenx.com' style='color:#007BFF;'>www.qyrenx.com</a>
        </footer>
    </div>";

            return emailbody;
        }



    }
}
