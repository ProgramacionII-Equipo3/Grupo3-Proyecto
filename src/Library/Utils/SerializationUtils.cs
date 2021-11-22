using System;
using System.IO;
using System.Text.Json;
using Library.Core.Distribution;
using Library.Core.Invitations;
using Library.HighLevel.Companies;
using Library.HighLevel.Entrepreneurs;

namespace Library.Utils
{
    /// <summary>
    /// This class contains methods which handle the serialization and deserialization of the bot's state.
    /// </summary>
    public static class SerializationUtils
    {
        private static JsonSerializerOptions defaultOptions = new JsonSerializerOptions
        {
            
        };

        /// <summary>
        /// Loads a JSON file and deserializes its contents. 
        /// </summary>
        /// <param name="path">The path of the JSON file.</param>
        /// <typeparam name="T">The type to deserialize the file's contents into.</typeparam>
        /// <returns></returns>
        public static T DeserializeJSON<T>(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open);
            if (JsonSerializer.DeserializeAsync<T>(fileStream, defaultOptions).Result is T result)
            {
                return result;
            } else
            {
                // TODO: specify a better exception
                throw new Exception();
            }
        }

        /// <summary>
        /// Deserialize the bot's state from JSON from a directory.
        /// </summary>
        /// <param name="path">The directory's path.</param>
        public static void DeserializeAllFromJSON(string path)
        {
            Singleton<InvitationManager>.Instance.LoadInvitations<CompanyInvitation>(path, "company_invitations.json");
            Singleton<SessionManager>.Instance.LoadUserSessions(path);
            Singleton<CompanyManager>.Instance.LoadCompanies(path);
            Singleton<EntrepreneurManager>.Instance.LoadEntrepreneurs(path);
        }
    }
}
