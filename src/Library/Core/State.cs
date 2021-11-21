namespace Library.Core
{
    /// <summary>
    /// This class represents a state of a user session.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Determines whether the user who has this <see cref="State" /> is complete,
        /// that is, if the process of registering to the platform has already finished.
        /// </summary>
        public abstract bool IsComplete { get; }

        /// <summary>
        /// Determines for what kind of user this state is oriented towards.
        /// </summary>
        public abstract Type UserType { get; }

        /// <summary>
        /// Processes a received message, returning the next state and the response message.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="data">The user's data.</param>
        /// <param name="msg">The message's text.</param>
        /// <returns>The next state and the response message. If the response message is null, the new state's default message can be used instead.</returns>
        public abstract (State, string) ProcessMessage(string id, UserData data, string msg);

        /// <summary>
        /// Returns the first message the object uses to indicate what kind of input it wants.
        /// </summary>
        /// <returns>A string.</returns>
        public abstract string GetDefaultResponse();

        /// <summary>
        /// This enumeration represents the three kinds of users the state can belong to.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// The administer.
            /// </summary>
            ADMIN,
            
            /// <summary>
            /// The entrepreneur.
            /// </summary>
            ENTREPRENEUR,
            
            /// <summary>
            /// The company.
            /// </summary>
            COMPANY
        }
    }
}
