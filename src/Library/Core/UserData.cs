using System.Text.Json.Serialization;

namespace Library.Core
{
    /// <summary>
    /// This class represents additional data that all users must have.
    /// We created this class because of SRP, this class in specific is
    /// the one in charge of saving all the information of an user.
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// The user's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's contact information.
        /// </summary>
        public ContactInfo ContactInfo { get; set; }

        /// <summary>
        /// Determines whether the user is complete,
        /// that is, if the process of registering to the platform has already finished.
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Determines for what kind of user this state is oriented towards.
        /// </summary>
        public Type UserType { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="UserData" />.
        /// </summary>
        /// <param name="name">The user's name.</param>
        /// <param name="isComplete">Whether the user is complete.</param>
        /// <param name="userType">The user's type.</param>
        /// <param name="email">The user's email.</param>
        /// <param name="phoneNumber">The user's phone number.</param>
        public UserData(string name, bool isComplete, Type userType, string? email, int? phoneNumber)
        {
            this.Name = name;
            this.IsComplete = isComplete;
            this.UserType = userType;
            this.ContactInfo = new ContactInfo
            {
                Email = email,
                PhoneNumber = phoneNumber
            };
        }

        /// <summary>
        /// Initializes an instance of <see cref="UserData" /> from JSON data.
        /// </summary>
        /// <param name="name">The user's name.</param>
        /// <param name="isComplete">Whether the user is complete.</param>
        /// <param name="userType">The user's type.</param>
        /// <param name="contactInfo">The user's contact info.</param>
        [JsonConstructor]
        public UserData(string name, bool isComplete, Type userType, ContactInfo contactInfo)
        {
            this.Name = name;
            this.IsComplete = isComplete;
            this.UserType = userType;
            this.ContactInfo = contactInfo;
        }

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
