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
        public async void SendNewOrderMail(Order order)
        {
            throw new NotImplementedException();
        }
    }
}