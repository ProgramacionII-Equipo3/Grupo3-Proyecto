using System.Collections.Generic;
using Library.Core;
using Library.Core.Invitations;
using System;

namespace Library.HighLevel.Administers
{
    public class Administer
    {
        private List<UserId> administerList = new List<UserId>;

        
        public void GenerateInvitation()
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
            InvitationManager.CreateInvitation(result, );
        }
       
    }
}