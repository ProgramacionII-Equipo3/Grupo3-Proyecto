using System;
using Ucu.Poo.Locations.Client;

namespace Library.Utils
{
    /// <summary>
    /// This class stores context-generic static methods.
    /// </summary>
    public static class BasicUtils
    {
        private static LocationApiClient locationClient = new LocationApiClient();

        /// <summary>
        /// This method returns the coordinates of a specific address using the LocationAPI.
        /// </summary>
        /// <param name="address">The site address.</param>
        /// <param name="city">The city where is the site.</param>
        /// <param name="department">The department where is the site.</param>
        /// <param name="country">The country where is the site.</param>
        /// <returns>The site´s coordinates.</returns>
        public static Location GetLocation(string address, string city, string department, string country)
        {
            return locationClient.GetLocationAsync(address, city, department, country).Result;
        }

        /// <summary>
        /// This method returns True if both strings are equals.
        /// </summary>
        /// <param name="s1">String to compare.</param>
        /// <param name="s2">Other String to compare.</param>
        /// <returns>Boolean.</returns>
        public static bool AreSimilar(string s1, string s2)
        {
            bool areEqual = string.Equals(s1, s2, System.StringComparison.OrdinalIgnoreCase);
            return areEqual;
        }

        /// <summary>
        /// This method check if a email is valid.
        /// </summary>
        /// <param name="s">The email to check.</param>
        /// <returns>Boolean.</returns>
        public static bool IsValidEmail(string s)
        {
            if (s.Contains("@"))
            {
                string[] mailSplitted = s.Split("@");
                string[] invalids = { "!", "¡" ,"¿", "?", "#", "´", "+", "-", "<", ">", "&", "/", "*", "[", "]", "{", "}", "$", "|", "°", ";", ":", "=", "," };
                char[] invalidFirst = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                if (mailSplitted.Length == 2)
                {
                    // This verificates the string before the "@"
                    foreach (var invalidCaracter in invalids)
                    {
                        if (mailSplitted[0].Contains(invalidCaracter))
                        {
                            return false;
                        }
                    }

                    string part1 = mailSplitted[0].Trim();
                    // This verificates if the first character of the email is a number, and if it is, return false.
                    foreach (var num in invalidFirst)
                    {
                        if(part1[0] == num)
                        {
                            return false;
                        }
                    }

                    // This verifies that the email does not have a consecutive '.'.
                    foreach (var stringPart in mailSplitted)
                    {
                        foreach (var character in stringPart)
                        {
                            if (character == '.')
                            {
                                if (character == stringPart[stringPart.IndexOf(character)+1])
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    // This verificates the string after the "@".
                    foreach (var invalidCaracter in invalids)
                    {
                        if (mailSplitted[1].Contains(invalidCaracter))
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

        /// <summary>
        /// This method checks if a phone number is valid.
        /// </summary>
        /// <param name="number">The phone number input.</param>
        /// <returns>True if the number is valid and false if it not does.</returns>
        public static bool IsValidPhoneNumber(int number)
        {
            string numberString = number.ToString();
            if (numberString.Length == 9)
            {
                if (numberString[0] == '0' && numberString[1] == '9')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a string is a valid hypertext link.
        /// </summary>
        /// <param name="link">The string.</param>
        /// <returns>Whether it's a valid link or not.</returns>
        public static bool IsValidHyperTextLink(string link)
        {
            return true;
        }

        /// <summary>
        /// Converts a location into a string equivalent.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The string equivalent.</returns>
        public static string LocationToString(Location location) =>
            $"{location.AddresLine}, {location.Locality}, {location.CountryRegion}";

    }
}
