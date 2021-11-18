using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library.Core.Invitations
{
    /// <summary>
    /// This class represents a list of invitations of a concrete class.
    /// </summary>
    /// <typeparam name="T">The class of the invitations.</typeparam>
    public class InvitationList<T> where T : Invitation
    {
        /// <summary>
        /// Gets the list of functions which remove invitations from the <see cref="InvitationList{T}" /> instances.
        /// </summary>
        private IList<Action<string>> removers = new List<Action<string>>();

        /// <summary>
        /// The instance of the <see cref="InvitationList{T}" />.
        /// </summary>
        public static readonly InvitationList<T> Instance = new InvitationList<T>();


        /// <summary>
        /// The set of invitations.
        /// It's an <see cref="ISet{T}" /> due to ISP,
        /// since only its functionality as a set is used.
        /// </summary>
        private ISet<T> invitations = new HashSet<T>();


        /// <summary>
        /// Gets the set of invitations.<br />
        /// This property is used to serialize invitations into JSON.
        /// </summary>
        public ISet<T> Invitations => invitations;

        /// <summary>
        /// Sets the set of invitations.<br />
        /// This function is used to deserialize invitations from JSON.
        /// </summary>
        /// <param name="value">An enumerable with the invitations.</param>
        public void SetInvitations(IEnumerable<T> value)
        {
            if(value is null) return;

            foreach(T i in value)
            {
                Singleton<InvitationManager>.Instance.AddInvitation(i);
            }
            this.invitations = value.ToHashSet();
        }

        /// <summary>
        /// Adds an invitation into the set.<br />
        /// This function should only be used by the <see cref="InvitationManager.CreateInvitation{T}(string, System.Func{string, T})" /> function.<br />
        /// For other contexts, use <see cref="InvitationManager.CreateInvitation{T}(string, System.Func{string, T})" />.
        /// </summary>
        /// <param name="invitation">The invitation.</param>
        public void AddInvitation(T invitation)
        {
            this.invitations.Add(invitation);
        }
    }
}
