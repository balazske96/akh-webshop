using System;
using System.ComponentModel;
using System.Net.Mail;
using AKHWebshop.Models.Shop.Data;
using Microsoft.Extensions.Configuration;

namespace AKHWebshop.Models.Mail
{
    public class AkhMailClient : IAkhMailClient
    {
        private SmtpClient Client { get; set; }
        private string BandAddress { get; set; }

        public AkhMailClient(SmtpClient emailClient, string bandAddress = "noreply@shop.hu")
        {
            Client = emailClient;
            BandAddress = bandAddress;
        }

        private static bool mailSent = false;

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = e.UserState.ToString();

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }

            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
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