using System;

namespace Library.Core
{
    /// <summary>
    /// This class acts as a unique identifier for each user in the platform.
    /// Different subclasses of this class represent ids of user of different messaging platforms.
    /// </summary>
    public abstract class UserId : IEquatable<UserId>, ISenderToSpecificUser
    {
        /// <summary>
        /// Compares equality of two user ids.
        /// </summary>
        /// <param name="other">The other id.</param>
        /// <returns>Whether the two ids are equal or not.</returns>
        public abstract bool Equals(UserId other);

        public abstract void SendMessage(string msg);
    }
}
