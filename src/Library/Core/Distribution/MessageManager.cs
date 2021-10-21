using System;

namespace Library.Core.Distribution
{
    /// <summary>
    /// This class represents the highest level of encapsulation in message processing.
    /// </summary>
    public static class MessageManager
    {
        /// <summary>
        /// Processes a received message, returning the text of the response message.
        /// </summary>
        /// <param name="msg">The received message.</param>
        /// <returns>The response message's text.</returns>
        public static string ProcessMessage(Message msg)
        {
            if(SessionManager.GetById(msg.Id) is UserSession session)
            {
                return session.ProcessMessage(msg.Text);
            } else
            {
                // TODO: Write code for non-registered users
                if(msg.Id is Library.Platforms.Telegram.TelegramId telegramId)
                    Console.WriteLine(telegramId.ChatId);
                return "You are not a user in this bot 😐";
            }
        }
    }
}
