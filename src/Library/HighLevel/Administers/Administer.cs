using System.Collections.Generic;
using Library.Core;
using Library.Core.Invitations;
using Library.HighLevel.Companies;
using System;

namespace Library.HighLevel.Administers
{
    /// <summary>
    /// This class represents an Administer.
    /// We used the Creator principle, this class is the one that 
    /// generates the invitations code.
    /// </summary>
    public class Administer
    {
        private List<UserId> administerList = new List<UserId>();

        /// <summary>
        /// This method create's an invitation code.
        /// </summary>
        public static string GenerateInvitation()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrsteuvwxyz0123456789";
            char[] charsArray = new char[8];
            Random random = new Random();
            for (int i=0; i<charsArray.Length; i++)
            {
                charsArray[i] = characters[random.Next(characters.Length)];
            }
            string result = string.Empty;
            foreach (var item in charsArray)
            {
                result += item; 
            }    
            
            return result;
        }

        /// <summary>
        /// This method has the responsibility of validating the entrepreneurs habilitations.
        /// </summary>
        /// <param name="userId"></param>
        public void ValidateHabilitation()
        {

        }

    }
}