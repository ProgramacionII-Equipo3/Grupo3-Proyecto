using System;
using System.Collections.Generic;
using System.Linq;

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

        static SessionManager()
        {
            sessions.Add(
                new UserSession(
                    id: new Library.Platforms.Telegram.TelegramId(1883636472),
                    userData: new UserData
                    {
                        Name = "Santiago De Olivera",
                        ContactInfo = new ContactInfo
                        {
                            Email = "santiagodeolivera@gmail.com",
                            PhoneNumber = 098553946
                        }
                    },
                    state: new Library.States.InitialMenuState()
                )
            );
        }

        /// <summary>
        /// Returns the <see cref="UserSession" /> whose id equals to the given one.
        /// </summary>
        /// <param name="id">The given id.</param>
        /// <returns>Its corresponding <see cref="UserSession" />, or null if there isn't.</returns>
        public static UserSession GetById(UserId id) =>
            sessions.Where(session => session.Id.Equals(id)).FirstOrDefault();
    }
}
