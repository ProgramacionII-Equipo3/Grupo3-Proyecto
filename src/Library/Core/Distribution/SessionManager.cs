using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;

namespace Library.Core.Distribution
{
    /// <summary>
    /// This class handles the creation and selection of user sessions.
    /// </summary>
    public static class SessionManager
    {
        /// <summary>
        /// The list of current sessions.
        /// </summary>
        private static List<UserSession> sessions = new List<UserSession>();

        // static SessionManager()
        // {
        //     sessions.Add(
        //         new UserSession(
        //             id: new Library.Platforms.Telegram.TelegramId(1883636472),
        //             userData: new UserData
        //             {
        //                 Name = "Santiago De Olivera",
        //                 ContactInfo = new ContactInfo
        //                 {
        //                     Email = "santiagodeolivera@gmail.com",
        //                     PhoneNumber = 098553946
        //                 }
        //             },
        //             state: new Library.States.InitialMenuState()
        //         )
        //     );
        // }

        /// <summary>
        /// Returns the <see cref="UserSession" /> whose id equals to the given one.
        /// </summary>
        /// <param name="id">The given id.</param>
        /// <returns>Its corresponding <see cref="UserSession" />, or null if there isn't.</returns>
        public static UserSession GetById(UserId id) =>
<<<<<<< HEAD
            sessions.Where(session => session.Id.Equals(id)).FirstOrDefault();

        /// <summary>
        /// Adds a new user into the platform.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="userData">The user's data.</param>
        /// <param name="state">The user's initial state.</param>
        /// <returns>The resulting <see cref="UserSession" />, or null if there's already one.</returns>
        public static UserSession NewUser(UserId id, UserData userData, State state)
        {
            if(GetById(id) != null) return null;
            UserSession result = new UserSession(id, userData, state);
            sessions.Add(result);
            return result;
        }
=======
            sessions.Where(session => session.MatchesId(id)).FirstOrDefault();
>>>>>>> b2a920e097eca6c9536beffc29a0154d437e95fe
    }
}
