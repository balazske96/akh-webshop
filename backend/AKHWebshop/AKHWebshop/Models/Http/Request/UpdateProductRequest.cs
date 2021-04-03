using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AKHWebshop.Models.Shop.Data;
using Newtonsoft.Json;

namespace AKHWebshop.Models.Http.Request
{
    public class UpdateProductRequest
    {
        [Required]
        [JsonPropertyName("id")]
        [MaxLength(36)]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [JsonProperty]
        [JsonPropertyName("display_name")]
        [MaxLength(256)]
        public string DisplayName { get; set; }

        [JsonPropertyName("image_name")]
        [MaxLength(256)]
        public string ImageName { get; set; }

        [Required]
        [JsonPropertyName("price")]
        [Range(0, uint.MaxValue)]
        public uint Price { get; set; }
    }
}