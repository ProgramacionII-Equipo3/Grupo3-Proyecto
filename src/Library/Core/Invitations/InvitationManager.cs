using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library.Core.Invitations
{
    /// <summary>
    /// This class acts as the highest level of abstraccion in invitation handling.
    /// </summary>
    public class InvitationManager
    {
        /// <summary>
        /// A list of all the invitations.
        /// </summary>
        private IList<Invitation> invitations = new List<Invitation>();

        /// <summary>
        /// Gets the number of invitations.
        /// </summary>
        public int InvitationCount => invitations.Count;

        /// <summary>
        /// Adds an invitation into the list.
        /// </summary>
        /// <param name="code">The invitation´s code.</param>
        /// <param name="f">Function that takes string like a parameter, and return an Invitation.</param>
        public void CreateInvitation(string code, Func<string, Invitation> f)
        {
            if (f != null)
            {
                Invitation invitation = f(code);
                if (!invitations.Any(inv => inv.Code == code))
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
        public string ValidateInvitation(string invitationCode, string userId)
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