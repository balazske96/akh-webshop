using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Shop.Data
{
    public interface IIdentifiable
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}