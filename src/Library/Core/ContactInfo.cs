namespace Library.Core
{
    /// <summary>
    /// This class represent contact information data associated with a user,
    /// a company, or another entity with contact information.
    /// </summary>
    public struct ContactInfo
    {
        /// <summary>
        /// The entity's email (null if non-existent).
        /// </summary>
        public string Email;

        /// <summary>
        /// The entity's phone number (-1 if non-existent).
        /// </summary>
        public int PhoneNumber;
    }
}
