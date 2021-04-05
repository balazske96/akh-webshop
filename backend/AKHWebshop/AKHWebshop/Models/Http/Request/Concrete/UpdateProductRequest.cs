using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AKHWebshop.Models.Http.Request.Concrete
{
    public class UpdateProductRequest
    {
        [FromRoute]
        [Required]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        public string Id { get; set; }

        [FromBody]
        [JsonPropertyName("name")]
        [MaxLength(256)]
        public string Name { get; set; }

        [FromBody]
        [Required]
        [JsonProperty]
        [JsonPropertyName("display_name")]
        [MaxLength(256)]
        public string DisplayName { get; set; }

        [FromBody]
        [JsonPropertyName("image_name")]
        [MaxLength(256)]
        public string ImageName { get; set; }

        [FromBody]
        [Required]
        [JsonPropertyName("price")]
        [Range(0, uint.MaxValue)]
        public uint Price { get; set; }
    }
}