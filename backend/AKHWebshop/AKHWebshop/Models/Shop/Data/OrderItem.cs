using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Shop.Data
{
    [Table("order_item")]
    public class OrderItem
    {
        [Required]
        [JsonPropertyName("order_id")]
        [Column("order_id", TypeName = "varchar(36)")]
        public Guid OrderId { get; set; }

        [Required]
        [JsonPropertyName("product_id")]
        [Column("product_id", TypeName = "varchar(36)")]
        public Guid ProductId { get; set; }

        [Required]
        [DefaultValue(Size.UNDEFINED)]
        [JsonPropertyName("size")]
        [Column("size", TypeName = "varchar(36)")]
        public Size Size { get; set; }

        [Required]
        [JsonPropertyName("amount")]
        [Column("amount")]
        public ushort Amount { get; set; }

        [NotMapped]
        [JsonPropertyName("product")]
        public Product Product { get; set; }
    }
}