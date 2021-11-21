using Library;
using Library.Core;
using Library.Core.Distribution;

namespace UnitTests.Utils
{
    /// <summary>
    /// This class contains functions which perform common actions in the unit tests.
    /// </summary>
    public static class BasicUtils
    {
        /// <summary>
        /// Creates a new user with the id "___".
        /// </summary>
        /// <param name="state">The state of the user.</param>
        public static void CreateUser(State state)
        {
            Singleton<SessionManager>.Instance.NewUser("___", EmptyUserData(), state);
        }

        /// <summary>
        /// Creates an empty user data for mock users.
        /// </summary>
        /// <returns>A <see cref="UserData" /></returns>
        public static UserData EmptyUserData() =>
            new UserData(string.Empty, false, UserData.Type.ADMIN, null, null);
    }
}