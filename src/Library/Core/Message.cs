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
        /// Initializes a new instance of the <see cref="Message"/> struct.
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
