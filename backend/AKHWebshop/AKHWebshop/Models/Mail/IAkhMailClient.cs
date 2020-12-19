using AKHWebshop.Models.Shop.Data;

namespace AKHWebshop.Models.Mail
{
    public interface IAkhMailClient
    {
        public void SendNewOrderMail(Order order)
        {
        }
    }
}