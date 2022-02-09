using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace MvcWebUI.Utilities
{
    public class EmailConfiguration
    {
        public  IResult SendConfirmationEmail(string token, string email)
        {
            string confirmationGuid = token;
            string verifyUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                               "/Account/Verify?id=" +
                               confirmationGuid;

            string bodyMessage = string.Format("üyeliğiniz başarıyla oluşturulmuştur. Aşağıdaki linke tıkladığınızda hesabınızın aktif olacaktır.\n");
            bodyMessage += verifyUrl;

            var message = new MailMessage()
            {
                Subject = "Üyeliğinizi doğrulayın.",
                Body = bodyMessage
            };

            var result=SendMail(message,email);
            return result;
        }

        public IResult SendForgotPasswordEmail(string token, string email)
        {
            string confirmationGuid = token;
            string verifyUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                               "/Account/NewPassword?pas=" +
                               confirmationGuid;

            string bodyMessage = string.Format("Yeni şifre almak için linke tıklayınız. \n");
            bodyMessage += verifyUrl;

            var message = new MailMessage()
            {
                Subject = "Yeni Şifre Oluştur.",
                Body = bodyMessage
            };

            var result = SendMail(message, email);
            return result;
        }

        public  IResult SendMail(MailMessage mail,string email)
        {
            var toAddress = new MailAddress(email);
            var fromAddress = new MailAddress("mail adresi yazınız");

            using (SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(fromAddress.Address, "şifre")


            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = mail.Subject, Body = mail.Body })
                {
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception e)
                    {
                        return new ErrorResult("Mail gönderilirken bir hat meydana geldi");
                    }

                }
            }
            return new SuccessResult("Mail gönderme işlemi başarılı");
        }
    }
}
