using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AKHWebshop.Models.Shop.Data;
using Newtonsoft.Json;

namespace AKHWebshop.Models.Http.Request
{
    public class CreateProductRequest
    {
        [JsonPropertyName("name")]
        [MaxLength(256)]
        public String Name { get; set; }

        [Required]
        [JsonProperty]
        [JsonPropertyName("display_name")]
        [MaxLength(256)]
        public string DisplayName { get; set; }

        [JsonPropertyName("amount")] public List<SizeRecord> Amount { get; set; }

        [JsonPropertyName("image_name")]
        [MaxLength(256)]
        public string ImageName { get; set; }

        public ProductStatus Status { get; set; } = ProductStatus.Hidden;

        [Required]
        [JsonPropertyName("price")]
        [Range(0, uint.MaxValue)]
        public uint Price { get; set; }
    }
}