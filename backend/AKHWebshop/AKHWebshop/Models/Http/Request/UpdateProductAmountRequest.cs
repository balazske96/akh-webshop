using System.Collections.Generic;
using AKHWebshop.Models.Shop.Data;

namespace AKHWebshop.Models.Http.Request
{
    public class UpdateProductAmountRequest
    {
        public List<SizeRecord> SizeRecords { get; set; }
    }
}