using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// <returns>The resulting value.</returns>
        public static T DeserializeJson<T>(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open);
            if (JsonSerializer.DeserializeAsync<T>(fileStream, defaultOptions).Result is T result)
            {
                return result;
            } else
            {
#warning Specify a better exception for when the method JsonSerializer.DeserializeAsync<T> returns null.
                throw new Exception();
            }
        }

        /// <summary>
        /// Loads a JSON file and deserializes its contents into an intermediate type,
        /// which is transformed into the expected type.
        /// </summary>
        /// <param name="path">The path of the JSON file.</param>
        /// <typeparam name="T">The type to deserialize the file's contents into.</typeparam>
        /// <typeparam name="U">The intermediate type.</typeparam>
        /// <returns>The resulting value.</returns>
        public static T DeserializeJsonFromIntermediate<T, U>(string path)
            where U: IJsonHolder<T> =>
                DeserializeJson<U>(path).ToValue();

        /// <summary>
        /// Loads a JSON file which contains an array and deserializes its contents
        /// into an array of an intermediate type,
        /// which is transformed into an enumerable of the expected type.
        /// </summary>
        /// <param name="path">The path of the JSON file.</param>
        /// <typeparam name="T">The type of which the array into which to deserialize the file's contents is.</typeparam>
        /// <typeparam name="U">The intermediate type.</typeparam>
        /// <returns>The resulting value.</returns>
        public static IEnumerable<T> DeserializeJsonListFromIntermediate<T, U>(string path)
            where U : IJsonHolder<T> =>
                DeserializeJson<U[]>(path).Select(json => json.ToValue());

        /// <summary>
        /// Deserialize the bot's state from JSON from a directory.
        /// </summary>
        /// <param name="path">The directory's path.</param>
        public static void DeserializeAllFromJSON(string path)
        {
            Singleton<InvitationManager>.Instance.LoadInvitations<Library.HighLevel.Companies.CompanyInvitation, Library.HighLevel.Companies.JsonCompanyInvitation>(path, "company_invitations.json");
            Singleton<SessionManager>.Instance.LoadUserSessions(path);
            Singleton<CompanyManager>.Instance.LoadCompanies(path);
            Singleton<EntrepreneurManager>.Instance.LoadEntrepreneurs(path);
        }
    }
}
