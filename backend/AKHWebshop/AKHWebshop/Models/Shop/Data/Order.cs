using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Shop.Data
{
    [Table("order")]
    public class Order : DatedEntity
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
        public PublicSpaceType PublicSpaceType { get; set; } = PublicSpaceType.Utca;

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
        [JsonPropertyName("shipped")]
        [Column("shipped")]
        public bool Shipped { get; set; }

        [Required]
        [JsonPropertyName("payed")]
        [Column("payed")]
        public bool Paid { get; set; }

        [Required]
        [JsonPropertyName("total_price")]
        [Column("total_price")]
        public uint TotalPrice { get; set; }

        [Required]
        [JsonPropertyName("first_name")]
        [Column("first_name", TypeName = "varchar(256)")]
        public string FirstName { get; set; }

        [Required]
        [JsonPropertyName("last_name")]
        [Column("last_name", TypeName = "varchar(256)")]
        public string LastName { get; set; }

        [JsonPropertyName("comment")]
        [Column("comment", TypeName = "varchar(256)")]
        public string Comment { get; set; }

        [Required]
        [JsonPropertyName("email")]
        [Column("email", TypeName = "varchar(256)")]
        [RegularExpression(
            @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "incorrect email format")]
        public string Email { get; set; }

        [JsonPropertyName("order_items")] public List<OrderItem> OrderItems { get; set; }

        public override string ToString()
        {
            return $"order_id:{Id.ToString()},first_name:{FirstName},last_name:{LastName},zip_code:{ZipCode}";
        }
    }
}