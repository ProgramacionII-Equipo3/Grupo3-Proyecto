namespace Library.Core
{
    /// <summary>
    /// This struct represents contact information data associated with a user,
    /// a company, or another entity with contact information.
    /// </summary>
    public struct ContactInfo
    {
        /// <summary>
        /// Gets the entity's email (null if non-existent).
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets the entity's phone number (null if non-existent).
        /// </summary>
        public int? PhoneNumber { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="ContactInfo" />.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        public ContactInfo(string? email = null, int? phoneNumber = null)
        {
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }
    }
}
