namespace Library.Core
{
    /// <summary>
    /// Represents a message received from a messaging platform.
    /// </summary>
    public struct Message
    {
        /// <summary>
        /// The content of the image.
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// The id of the user who sent the image.
        /// </summary>
        public readonly UserId Id;

        /// <summary>
        /// Creates a message.
        /// </summary>
        /// <param name="text">The message's text.</param>
        /// <param name="id">The message's id.</param>
        public Message(string text, UserId id)
        {
            this.Text = text;
            this.Id = id;
        }
    }
}
