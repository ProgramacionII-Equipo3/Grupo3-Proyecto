using System;
using System.Collections.Generic;
using Library.Core.Invitations;
using Library.Core;
using Library.HighLevel.Companies;

namespace Library.HighLevel.Administers
{
    /// <summary>
    /// This class represents an Administer.
    /// We used the Expert principle, this class is the one that
    /// generates the invitations code (as it should be).
    /// At the same time we used Creator, for example the list of admin
    /// is created by this class.
    /// </summary>
    public class Administer
    {
        private List<UserId> administerList = new List<UserId>();

        /// <summary>
        /// This method create's an invitation code.
        /// </summary>
        /// <returns>Invitation´s code.</returns>
        public static string GenerateInvitation()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrsteuvwxyz0123456789";
            char[] charsArray = new char[8];
            Random random = new Random();
            for (int i = 0; i < charsArray.Length; i++)
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
        /// This method creates an invitation for a company.
        /// </summary>
        public static void CreateCompanyInvitation()
        {
            Singleton<InvitationManager>.Instance.CreateInvitation(Administer.GenerateInvitation(), code => new CompanyInvitation(code));
        }
    }
}
