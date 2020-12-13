using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AKHWebshop.Models.Shop.Data
{
    [Table("size_record")]
    public class SizeRecord
    {
        [Required]
        [JsonPropertyName("product_id")]
        [JsonIgnore]
        [Column("product_id", TypeName = "varchar(36)")]
        public Guid ProductId { get; set; }

        [Required]
        [DefaultValue(Size.UNDEFINED)]
        [JsonPropertyName("size")]
        [Column("size", TypeName = "varchar(36)")]
        public Size Size { get; set; }

        [Required]
        [JsonPropertyName("quantity")]
        [Column("quantity", TypeName = "smallint unsigned")]
        public ushort Quantity { get; set; }
    }
}