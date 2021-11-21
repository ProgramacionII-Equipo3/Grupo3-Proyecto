using System.Collections.Generic;
using System.Linq;

namespace Library.Core.Distribution
{
    /// <summary>
    /// This class handles the creation and selection of user sessions.
    /// </summary>
    public class SessionManager
    {
        /// <summary>
        /// The list of current sessions.
        /// The class <see cref="List{T}" /> is used instead of the interface <see cref="IList{T}" />
        /// because the method <see cref="List{T}.RemoveAll(System.Predicate{T})" /> is necessary for the method <see cref="SessionManager.RemoveUser(string)" />.
        /// </summary>
        private List<UserSession> sessions = new List<UserSession>();

        /// <summary>
        /// Returns the <see cref="UserSession" /> whose id equals to the given one.
        /// </summary>
        /// <param name="id">The given id.</param>
        /// <returns>Its corresponding <see cref="UserSession" />, or null if there isn't.</returns>
        public UserSession? GetById(string id) =>
            sessions.Where(session => session.MatchesId(id)).FirstOrDefault();

        /// <summary>
        /// Adds a new user into the platform.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="userData">The user's data.</param>
        /// <param name="state">The user's initial state.</param>
        /// <returns>The resulting <see cref="UserSession" />, or null if there's already one.</returns>
        public UserSession? NewUser(string id, UserData userData, State state)
        {
            if (GetById(id) != null)
            {
                return null;
            }

            UserSession result = new UserSession(id, userData, state);
            sessions.Add(result);
            return result;
        }

        /// <summary>
        /// Removes the user with a concrete id.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>Whether there was a user with the given id.</returns>
        public bool RemoveUser(string id) =>
            this.sessions.RemoveAll(session => session.Id == id) > 0;

        /// <summary>
        /// Removes the user with a concrete name.
        /// </summary>
        /// <param name="name">The user's name.</param>
        /// <returns>Whether there was a user with the given name.</returns>
        public bool RemoveUserByName(string name) =>
            this.sessions.RemoveAll(session => session.UserData.Name == name) > 0;
    }
}
