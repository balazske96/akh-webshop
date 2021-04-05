using System;
using AKHWebshop.Models.Auth;

namespace AKHWebshop.Models
{
    // Constant values
    public class K
    {
        public static readonly int minUsernameLength = 8;
        public static readonly int maxUsernameLength = 128;
        public static readonly int minPasswordLength = 8;
        public static readonly int maxPasswordLength = 64;
        public static readonly string passwordContainsNumberRegex = @"\d+";
        public static readonly string passwordContainsLetterRegex = @"[a-zA-Z]+";
        public static readonly string userAuthCookieName = "_uc";
        public static readonly string guidRegex = @"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?";
        public static readonly int DefaultQueryLimit = 10;
    }
}