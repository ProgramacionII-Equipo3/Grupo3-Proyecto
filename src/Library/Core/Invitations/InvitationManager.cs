using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library.Core.Invitations
{
    /// <summary>
    /// This class acts as the highest level of abstraccion in invitation handling.
    /// </summary>
    public static class InvitationManager
    {
        private static List<Invitation> invitations = new List<Invitation>();

        /// <summary>
        /// Gets a public read-only list of the invitations.
        /// </summary>
        public static ReadOnlyCollection<Invitation> InvitationsReadOnly => invitations.AsReadOnly();

        /// <summary>
        /// Creates an invitation for the companies.
        /// </summary>
        /// <param name="code">The invitation´s code.</param>
        /// <param name="f">Function that takes string like a parameter, and return an Invitation.</param>
        public static void CreateInvitation(string code, Func<string, Invitation> f)
        {
            if (f != null)
            {
                Invitation invitation = f(code);
                if (!invitations.Contains(invitation))
                {
                    invitations.Add(invitation);
                }
            }
        }

        /// <summary>
        /// Validates an invitation with a user id, returning the response message of the validation.
        /// </summary>
        /// <param name="invitationCode">The invitation's code.</param>
        /// <param name="userId">The id of the user who validated the invitation.</param>
        /// <returns>The response message of the validation of the invitation, or an error message if there wasn't.</returns>
        public static string ValidateInvitation(string invitationCode, UserId userId)
        {
            if (
                invitations.Where(invitation => invitation.Code == invitationCode).FirstOrDefault()
                is Invitation invitation)
            {
                string r = invitation.Validate(userId);
                invitations.Remove(invitation);
                return r;
            }
            else
            {
                return null;
            }
        }
    }
}