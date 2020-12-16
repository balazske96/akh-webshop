using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AKHWebshop.Models.Shop.Data
{
    public class Order
    {
        [Required]
        [JsonPropertyName("id")]
        [Column("id", TypeName = "varchar(36)")]
        public Guid Id { get; set; }
    }
}