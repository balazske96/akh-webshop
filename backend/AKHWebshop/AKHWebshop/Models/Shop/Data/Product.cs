using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        
        [JsonPropertyName("amount")] public List<SizeRecord> Amount { get; set; }
        
        [JsonPropertyName("image_name")]
        [Column("image_name", TypeName = "varchar(255)")]
        public string ImageName { get; set; }

        [Required]
        [JsonPropertyName("status")]
        [Column("status", TypeName = "varchar(255)")]
        public ProductStatus Status { get; set; } = ProductStatus.Hidden;

        public bool DoesAmountContainSizeDuplication()
        {
            if (Amount == null)
                return false;
            HashSet<Size> sizes = new HashSet<Size>();
            List<SizeRecord> duplicates = Amount.Where(sizeRec => !sizes.Add(sizeRec.Size)).ToList();
            return duplicates.Count != 0;
        }
    }
}