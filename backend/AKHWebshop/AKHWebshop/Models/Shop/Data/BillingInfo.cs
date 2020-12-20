using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Shop.Data
{
    public class BillingInfo : IAddressable, IIdentifiable
    {
        [Required]
        [JsonPropertyName("id")]
        [Column("id", TypeName = "varchar(36)")]
        public Guid Id { get; set; }

        [Required]
        [JsonPropertyName("country")]
        [Column("country", TypeName = "varchar(256)")]
        public string Country { get; set; }

        [Required]
        [JsonPropertyName("zip_code")]
        [Column("zip_code", TypeName = "varchar(4)")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "incorrect zip code format")]
        public string ZipCode { get; set; }

        [Required]
        [JsonPropertyName("city")]
        [Column("city", TypeName = "varchar(256)")]
        public string City { get; set; }

        [Required]
        [JsonPropertyName("public_space_type")]
        [Column("public_space_type", TypeName = "varchar(20)")]
        public PublicSpaceType? PublicSpaceType { get; set; }

        [Required]
        [JsonPropertyName("public_space_name")]
        [Column("public_space_name", TypeName = "varchar(256)")]
        public string PublicSpaceName { get; set; }

        [JsonPropertyName("state")]
        [Column("state", TypeName = "varchar(256)")]
        public string State { get; set; }

        [Required]
        [JsonPropertyName("house_number")]
        [Column("house_number")]
        public ushort HouseNumber { get; set; }

        [JsonPropertyName("floor")]
        [Column("floor")]
        public byte? Floor { get; set; }

        [JsonPropertyName("door")]
        [Column("door")]
        public ushort? Door { get; set; }

        [Required]
        [JsonPropertyName("first_name")]
        [Column("first_name", TypeName = "varchar(256)")]
        public string FirstName { get; set; }

        [Required]
        [JsonPropertyName("last_name")]
        [Column("last_name", TypeName = "varchar(256)")]
        public string LastName { get; set; }
    }
}