using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AKHWebshop.Models.Shop.Data
{
    [Table("product")]
    public class Product
    {
        [Required]
        [JsonPropertyName("id")]
        [Column("id", TypeName = "varchar(36)")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        [Column("name", TypeName = "varchar(256)")]
        public string Name { get; set; }

        [Required]
        [JsonProperty]
        [JsonPropertyName("display_name")]
        [Column("display_name", TypeName = "varchar(256)")]
        public string DisplayName { get; set; }

        [JsonPropertyName("amount")] public List<SizeRecord> Amount { get; set; }

        [JsonPropertyName("image_name")]
        [Column("image_name", TypeName = "varchar(256)")]
        public string ImageName { get; set; }

        [Required]
        [JsonPropertyName("status")]
        [Column("status", TypeName = "varchar(256)")]
        public ProductStatus Status { get; set; } = ProductStatus.Hidden;

        [Required]
        [JsonPropertyName("price")]
        [Column("price")]
        public uint Price { get; set; }
    }
}