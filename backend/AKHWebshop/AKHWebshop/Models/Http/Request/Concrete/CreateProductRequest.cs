using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AKHWebshop.Models.Http.Request.Concrete
{
    public class CreateProductRequest
    {
        [FromBody]
        [JsonPropertyName("name")]
        [MaxLength(256)]
        public String Name { get; set; }

        [FromBody]
        [Required]
        [JsonProperty]
        [JsonPropertyName("display_name")]
        [MaxLength(256)]
        public string DisplayName { get; set; }

        [FromBody]
        [JsonPropertyName("amount")]
        public List<SizeRecord> Amount { get; set; }

        [FromBody]
        [JsonPropertyName("image_name")]
        [MaxLength(256)]
        public string ImageName { get; set; }

        [FromBody] public ProductStatus Status { get; set; } = ProductStatus.Hidden;

        [FromBody]
        [Required]
        [JsonPropertyName("price")]
        [Range(0, uint.MaxValue)]
        public uint Price { get; set; }
    }
}