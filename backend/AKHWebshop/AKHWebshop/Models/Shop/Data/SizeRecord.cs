using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AKHWebshop.Models.Shop.Data
{
    public class SizeRecord
    {
        [Required]
        [JsonPropertyName("product_id")]
        [Column(TypeName = "varchar(36)")]
        public Guid ProductId { get; set; }
        
        [JsonPropertyName("size")]
        [Column(TypeName = "enum")]
        public Size Size { get; set; }

        [Required]
        [JsonPropertyName("quantity")]
        [Column(TypeName = "smallint unsigned")]
        public ushort Quantity { get; set; }
    }
}
