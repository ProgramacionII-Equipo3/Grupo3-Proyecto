namespace Library.Core.Invitations
{
    /// <summary>
    /// This class represents invitations, through which admins can invite other non-registered users into the platform.
    /// </summary>
    public abstract class Invitation
    {
        /// <summary>
        /// Gets the invitation's code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Invitation"/> class.
        /// </summary>
        /// <param name="code">Invitation´s code.</param>
        public Invitation(string code)
        {
            this.Code = code;
        }

        /// <summary>
        /// Validates the invitation, returning the response string for that activity.
        /// </summary>
        /// <param name="userId">The id of the user who validated the invitation.</param>
        /// <returns>The response string.</returns>
        public abstract string Validate(UserId userId);
    }
}