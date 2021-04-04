using System;
using AKHWebshop.Models.Auth;

namespace AKHWebshop.Models
{
    // Constant values
    public class K
    {
        public static int minUsernameLength = 8;
        public static int maxUsernameLength = 128;
        public static int minPasswordLength = 8;
        public static int maxPasswordLength = 64;
        public static string passwordContainsNumberRegex = @"\d+";
        public static string passwordContainsLetterRegex = @"[a-zA-Z]+";
        public static string userAuthCookieName = "_uc";
    }
}