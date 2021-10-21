using System;
using Library.Core.Processing;

namespace Library.Core.Distribution
{
    public class UserSession
    {
        /// <summary>
        /// The id of the user.
        /// </summary>
        public readonly UserId Id;

        /// <summary>
        /// Data associated with the user.
        /// </summary>
        private UserData userData;

        /// <summary>
        /// The current state of the user's session.
        /// </summary>
        private IState state;

        public UserSession(UserId id, UserData userData, IState state)
        {
            this.Id = id;
            this.userData = userData;
            this.state = state;
        }

        /// <summary>
        /// Process the received message text, returning the response message text.
        /// </summary>
        /// <param name="msg">The received message text.</param>
        /// <returns>The response message text.</returns>
        public string ProcessMessage(string msg)
        {
            var (newState, res) = this.state.ProcessMessage(this.Id, this.userData, msg);
            this.state = newState;
            return res;
        }
    }
}
