using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Shop.Data
{
    public interface IAddressable
    {
        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public PublicSpaceType? PublicSpaceType { get; set; }

        public string PublicSpaceName { get; set; }

        public string State { get; set; }

        public ushort HouseNumber { get; set; }

        public byte? Floor { get; set; }

        public ushort? Door { get; set; }
    }
}