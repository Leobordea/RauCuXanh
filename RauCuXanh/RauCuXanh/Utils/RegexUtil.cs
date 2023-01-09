using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RauCuXanh.Utils
{
    public static class RegexUtil
    {
        public static Regex ValidateEmailAddress()
        {
            return new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public static Regex ValidatePassword()
        {
            //Minimum eight characters, at least one letter and one number:
            return new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
        }

        public static Regex MinLength(int length)
        {
            return new Regex(@"(\s*(\S)\s*){" + length + @",}");
        }

        public static Regex ValidZipCode()
        {
            return new Regex(@"^\d{5}(?:[-\s]\d{4})?$");
        }
    }
}
