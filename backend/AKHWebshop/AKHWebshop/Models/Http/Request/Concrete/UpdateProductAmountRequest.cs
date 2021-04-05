using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;

namespace AKHWebshop.Models.Http.Request.Concrete
{
    public class UpdateProductAmountRequest
    {
        [FromRoute]
        [Required]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        public string Id { get; set; }

        [FromBody] public List<SizeRecord> SizeRecords { get; set; }
    }
}