using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;

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
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("display_name")]
        [Column("display_name", TypeName = "varchar(255)")]
        public string DisplayName { get; set; }

        [JsonPropertyName("quantity")]
        [Column("sizes", TypeName = "smallint unsigned")]
        public List<SizeRecord> Sizes { get; set; }


        [JsonPropertyName("image_name")]
        [Column("image_name", TypeName = "varchar(255)")]
        public string ImageName { get; set; }
    }
}