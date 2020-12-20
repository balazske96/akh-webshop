using System;
using System.ComponentModel;
using System.Net.Mail;
using AKHWebshop.Models.Shop.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AKHWebshop.Models.Mail
{
    public class AkhMailClient : IAkhMailClient
    {
        private SmtpClient Client { get; set; }
        private string BandAddress { get; set; }

        private ILogger<AkhMailClient> Logger { get; set; }

        public AkhMailClient(SmtpClient emailClient,
            ILogger<AkhMailClient> logger, string bandAddress = "noreply@shop.hu")
        {
            Client = emailClient;
            BandAddress = bandAddress;
            Logger = logger;
        }

        private static bool mailSent = false;

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = e.UserState.ToString();

            if (e.Error != null)
            {
                Logger.Log(LogLevel.Warning, "[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Logger.Log(LogLevel.Information, "email sent: " + token);
            }

            mailSent = true;
        }

        public void SendNewOrderMail(Order order)
        {
            MailMessage message = new MailMessage(BandAddress, order.Email);
            message.Body = "Thanks for making an order";
            message.Subject = "New order";
            Client.SendCompleted += (s, e) =>
            {
                SendCompletedCallback(s, e);
                message.Dispose();
                Client.Dispose();
            };
            Order userState = order;
            Client.SendAsync(message, userState);
        }
    }
}