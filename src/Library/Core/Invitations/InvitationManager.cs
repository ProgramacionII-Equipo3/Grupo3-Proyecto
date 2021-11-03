using System;
using System.Collections.Generic;
using System.Linq;
using Library.HighLevel.Companies;
using Library.HighLevel.Administers;


namespace Library.Core.Invitations
{
    /// <summary>
    /// This class acts as the highest level of abstraccion in invitation handling.
    /// </summary>
    public static class InvitationManager
    {
        /// <summary>
        /// Stores all invitations generated.
        /// </summary>
        public static List<Invitation> invitations = new List<Invitation>();

        static InvitationManager()
        {
            invitations.Add(new Library.HighLevel.Companies.CompanyInvitation("12345"));
        }

        /// <summary>
        /// Creates an invitation.
        /// </summary>
        public static void CreateInvitation()
        {
            string code = Administer.GenerateInvitation();
            Invitation invitation = new CompanyInvitation(code); 
            invitations.Add(invitation);
        }

        /// <summary>
        /// Validates an invitation with a user id, returning the response message of the validation.
        /// </summary>
        /// <param name="invitationCode">The invitation's code.</param>
        /// <param name="userId">The id of the user who validated the invitation.</param>
        /// <returns>The response message of the validation of the invitation, or an error message if there wasn't.</returns>
        public static string ValidateInvitation(string invitationCode, UserId userId)
        {
            if(
                invitations.Where(invitation => invitation.Code == invitationCode).FirstOrDefault()
                is Invitation invitation
            )
            {
                string r = invitation.Validate(userId);
                invitations.Remove(invitation);
                return r;
            } else return null;
        }
    }
}
