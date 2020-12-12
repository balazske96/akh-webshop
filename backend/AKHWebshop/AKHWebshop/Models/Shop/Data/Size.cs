using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AKHWebshop.Models.Shop.Data
{
    public enum Size
    {
        [Description("UNDEFINED")] UNDEFINED,
        [Description("XXS")] XXS,
        [Description("XS")] XS,
        [Description("S")] S,
        [Description("M")] M,
        [Description("L")] L,
        [Description("XL")] XL,
        [Description("XXL")] XXL,
        [Description("XXXL")] XXXL
    }
}