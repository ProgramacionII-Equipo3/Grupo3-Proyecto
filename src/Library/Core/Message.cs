namespace Library.Core
{
    /// <summary>
    /// Represents a message (either received from a messaging platform or sent to one)
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

        public Message(string text, UserId id)
        {
            this.Text = text;
            this.Id = id;
        }
    }
}
