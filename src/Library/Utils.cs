using System;
namespace Library
{
    /// <summary>
    /// This class stores context-generic static methods.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// This method returns True if both strings are equals.
        /// </summary>
        /// <param name="s1">String to compare</param>
        /// <param name="s2">Other String to compare</param>
        /// <returns></returns>
        public static bool AreSimilar(string s1, string s2)
        {
            bool areEqual = string.Equals(s1, s2, System.StringComparison.OrdinalIgnoreCase);
            return areEqual;
        }

        /// <summary>
        /// This method check if a email is valid.
        /// </summary>
        /// <param name="s">The email to check</param>
        /// <returns></returns>
        public static bool IsValidEmail(string s)
        {
            if(s.Contains("@"))
            {
                string[] mailSplitted = s.Split("@");
                string[] invalids = {"!", "¡" ,"¿", "?", "#", "´", "+", "-", "<", ">", "&", "/", "*", "[", "]", "{", "}", "$", "|", "°", ";", ":", "=", ","};
                char[] invalidFirst = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
                if(mailSplitted.Length == 2)
                {
                    // This verificates the string before the "@"
                    foreach(var invalidCaracter in invalids)
                    {
                        if(mailSplitted[0].Contains(invalidCaracter))
                        {
                            return false;
                        }
                    }
                    string part1 = mailSplitted[0].Trim();
                    // This verificates if the first character of the email is a number, and if it is, return false.
                    foreach(var num in invalidFirst)
                    {
                        if(part1[0] == num)
                        {
                            return false;
                        }
                    }
                    // This verifies that the email does not have a consecutive '.'
                    foreach(var stringParts in mailSplitted)
                    {
                        foreach(var caracter in stringParts)
                        {
                            if(caracter == '.')
                            {
                                if(caracter == stringParts[stringParts.IndexOf(caracter)+1])
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    // This verificates the string after the "@"
                    foreach(var invalidCaracter in invalids)
                    {
                        if(mailSplitted[1].Contains(invalidCaracter))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}