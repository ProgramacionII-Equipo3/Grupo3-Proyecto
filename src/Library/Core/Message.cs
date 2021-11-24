namespace Library.Core
{
    /// <summary>
    /// Represents a message received from a messaging platform.
    /// We created this class because of SRP, this class in particular is the one that
    /// saves the message. Also OCP, because if you want to add information it would
    /// not affect how it works.
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
        public readonly string Id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> struct.
        /// </summary>
        /// <param name="text">The message's text.</param>
        /// <param name="id">The message's id.</param>
        public Message(string text, string id)
        {
            this.Text = text;
            this.Id = id;
        }
    }
}
