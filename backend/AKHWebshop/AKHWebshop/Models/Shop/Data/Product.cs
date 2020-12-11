using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Shop.Data
{
    public class Product
    {
        
        [Required]
        [JsonPropertyName("id")]
        [Column(TypeName = "varchar(36)")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("display_name")]
        [Column(TypeName = "varchar(255)")]
        public string DisplayName { get; set; }

        [Required]
        [JsonPropertyName("quantity")]
        [Column(TypeName = "smallint unsigned")]
        public List<SizeRecord> Sizes { get; set; }
        

        [JsonPropertyName("image_name")]
        [Column(TypeName = "varchar(255)")]
        public string ImageName { get; set; }

    }
}