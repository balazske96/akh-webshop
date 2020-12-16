using System.Net.Mail;

namespace AKHWebshop.Models.Mail
{
    public class AkhMailClient : IAkhMailClient
    {
        private SmtpClient Client { get; set; }

        public AkhMailClient(SmtpClient emailClient)
        {
            Client = emailClient;
        }

        public void SendMailMessage()
        {
            new MailMessage();
        }
    }
}