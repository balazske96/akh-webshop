using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Shop.Data
{
    [Table("order")]
    public class Order
    {
        [Required]
        [JsonPropertyName("id")]
        [Column("id", TypeName = "varchar(36)")]
        public Guid Id { get; set; }

        [Required]
        [JsonPropertyName("created_at")]
        [Column("created_at")]
        public DateTime CreatedAt { get; } = DateTime.Now;

        [Required]
        [JsonPropertyName("updated_at")]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

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
        public PublicSpaceType PublicSpaceType { get; set; } = PublicSpaceType.Utca;

        [JsonPropertyName("state")]
        [Column("state", TypeName = "varchar(512)")]
        public string State { get; set; }

        [Required]
        [JsonPropertyName("house_number")]
        [Column("house_number")]
        public ushort HouseNumber { get; set; }

        [JsonPropertyName("floor")]
        [Column("floor")]
        public byte Floor { get; set; }

        [JsonPropertyName("door")]
        [Column("door")]
        public ushort Door { get; set; }

        [Required]
        [JsonPropertyName("shipped")]
        [Column("shipped")]
        public bool Shipped { get; set; }

        [Required]
        [JsonPropertyName("payed")]
        [Column("payed")]
        public bool Paid { get; set; }

        [Required]
        [JsonPropertyName("total")]
        [Column("total")]
        public uint Total { get; set; }

        [JsonPropertyName("order_items")] public List<OrderItem> OrderItems { get; set; }
    }
}