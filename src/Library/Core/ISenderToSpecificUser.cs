namespace Library.Core
{
    /// <summary>
    /// This interface represents an object which knows how to send a message to a particular user in the bot.
    /// </summary>
    public interface ISenderToSpecificUser
    {
        /// <summary>
        /// Send a message to a concrete user.
        /// </summary>
        /// <param name="msg">The message.</param>
        public void SendMessage(string msg);
    }
}
