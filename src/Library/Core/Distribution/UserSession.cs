using System.Text.Json.Serialization;

namespace Library.Core.Distribution
{
    /// <summary>
    /// This class represent a user's session, being the highest-level class which represents it.
    /// This class uses the Expert pattern, it is the one responsible of processing the
    /// message and verify if the id matches.
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// The id of the user.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Data associated with the user.
        /// </summary>
        public UserData UserData { get; private set; }

        /// <summary>
        /// The current state of the user's session.
        /// </summary>
        private State state;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSession"/> class.
        /// </summary>
        /// <param name="id">The session's user's id.</param>
        /// <param name="userData">The session's user's data.</param>
        /// <param name="state">The session's initial state.</param>
        public UserSession(string id, UserData userData, State state)
        {
            this.Id = id;
            this.UserData = userData;
            this.state = state;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSession"/> class from JSON data.
        /// </summary>
        /// <param name="id">The session's user's id.</param>
        /// <param name="userData">The session's user's data.</param>
        [JsonConstructor]
        public UserSession(string id, UserData userData): this(id, userData, State.FromUserData(id, userData)) {}

        /// <summary>
        /// Process the received message text, returning the response message text.
        /// </summary>
        /// <param name="msg">The received message text.</param>
        /// <returns>The response message text.</returns>
        public string ProcessMessage(string msg)
        {
            if (msg == "/help")
            {
                return this.state.GetDefaultResponse();
            }
            UserData userData = this.UserData;
            var (newState, res) = this.state.ProcessMessage(this.Id, ref userData, msg);
            if (newState == null)
            {
                Singleton<SessionManager>.Instance.RemoveUser(this.Id);
                return "User eliminated.";
            }
            this.UserData = userData;
            this.state = newState;
            return res ?? newState.GetDefaultResponse();
        }

        /// <summary>
        /// Checks whether this <see cref="UserSession" /> has a concrete user id.
        /// </summary>
        /// <param name="id">The id to compare with.</param>
        /// <returns>True uf the IDs are equal and false if it not does.</returns>
        public bool MatchesId(string id) => this.Id.Equals(id);
    }
}
