namespace Library.Core.Distribution
{
    /// <summary>
    /// This class represent a user's session, being the highest-level class which represents it.
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// The id of the user.
        /// </summary>
        public string Id { get; }

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
        /// Process the received message text, returning the response message text.
        /// </summary>
        /// <param name="msg">The received message text.</param>
        /// <returns>The response message text.</returns>
        public string ProcessMessage(string msg)
        {
            var (newState, res, newData) = this.state.ProcessMessage(this.Id, this.UserData, msg);
            if(newState == null)
            {
                Singleton<SessionManager>.Instance.RemoveUser(this.Id);
                return "User eliminated.";
            }
            if(newData != null) this.UserData = newData;
            this.state = newState;
            return res ?? newState.GetDefaultResponse();
        }

        /// <summary>
        /// Checks whether this <see cref="UserSession" /> has a concrete user id.
        /// </summary>
        /// <param name="id">The id to compare with.</param>
        /// <returns>True uf the ID´s are equal and false if it not does.</returns>
        public bool MatchesId(string id) => this.Id.Equals(id);
    }
}
