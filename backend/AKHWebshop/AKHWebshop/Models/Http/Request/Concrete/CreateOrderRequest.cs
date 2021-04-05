using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;

namespace AKHWebshop.Models.Http.Request.Concrete
{
    public class CreateOrderRequest
    {
        [FromBody]
        [Required]
        [JsonPropertyName("country")]
        [MaxLength(256)]
        public string Country { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("zip_code")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "incorrect zip code format")]
        [MaxLength(4)]
        public string ZipCode { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("city")]
        [MaxLength(256)]
        public string City { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("public_space_type")]
        [MaxLength(20)]
        public PublicSpaceType? PublicSpaceType { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("public_space_name")]
        [Column("public_space_name", TypeName = "varchar(256)")]
        public string PublicSpaceName { get; set; }

        [FromBody]
        [JsonPropertyName("state")]
        [MaxLength(256)]
        public string State { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("house_number")]
        public ushort HouseNumber { get; set; }

        [FromBody] [JsonPropertyName("floor")] public byte? Floor { get; set; }

        [FromBody] [JsonPropertyName("door")] public ushort? Door { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("total_price")]
        public uint TotalPrice { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("first_name")]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("last_name")]
        [MaxLength(256)]
        public string LastName { get; set; }

        [FromBody]
        [JsonPropertyName("comment")]
        [MaxLength(256)]
        public string Comment { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("email")]
        [MaxLength(256)]
        [RegularExpression(
            @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "incorrect email format")]
        public string Email { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("billing_info")]
        public BillingInfo BillingInfo { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("same_billing_info")]
        public bool BillingInfoSameAsOrderInfo { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("order_items")]
        public List<OrderItem> OrderItems { get; set; }
    }
}