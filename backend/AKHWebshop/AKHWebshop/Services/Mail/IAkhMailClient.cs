using AKHWebshop.Models.Shop.Data;

namespace AKHWebshop.Services.Mail
{
    public interface IAkhMailClient
    {
        public void SendNewOrderMail(Order order);
    }
}