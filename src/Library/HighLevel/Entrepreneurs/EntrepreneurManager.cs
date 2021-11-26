using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Library.Core.Distribution;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Library.Utils;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents the highest level of encapsulation in handling entrepreneurs.
    /// </summary>
    public class EntrepreneurManager
    {
        /// <summary>
        /// The list of entrepreneurs.
        /// The class <see cref="List{T}" /> is used instead of the interface <see cref="IList{T}" />
        /// because the method <see cref="List{T}.AsReadOnly()" /> is necessary for the property <see cref="EntrepreneurManager.Entrepreneurs" />.
        /// </summary>
        private List<Entrepreneur> entrepreneurs = new List<Entrepreneur>();

        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurManager"/>.
        /// </summary>
        public EntrepreneurManager()
        {
            Singleton<SessionManager>.Instance.AddRemover(userId => this.RemoveUserAsEntrepreneurById(userId));
        }

        /// <summary>
        /// Gets the entrepreneur's users in the platform.
        /// </summary>
        public ReadOnlyCollection<Entrepreneur> Entrepreneurs => entrepreneurs.AsReadOnly();

        /// <summary>
        /// Adds a new entrepreneur into the list, if there isn't already an entrepreneur with the same name.
        /// </summary>
        /// <param name="id">The entrepreneur's id.</param>
        /// <param name="name">The entrepreneur's name.</param>
        /// <param name="age">The entrepreneur's age.</param>
        /// <param name="location">The entrepreneur's location.</param>
        /// <param name="heading">The entrepreneur's heading.</param>
        /// <param name="habilitations">The entrepreneur's habilitations.</param>
        /// <param name="specializations">The entrepreneur's specializations.</param>
        /// <returns>Whether the operation was successful.</returns>
        public bool NewEntrepreneur(string id, string name, int age, Location location, string heading, IList<Habilitation> habilitations, IList<string> specializations)
        {
            if(this.entrepreneurs.Any(e => e.Name == name)) return false;
            entrepreneurs.Add(new Entrepreneur(id, name, age, location, heading, habilitations, specializations));
            return true;
        }

        /// <summary>
        /// Removes an entrepreneur with a concrete id.
        /// </summary>
        /// <param name="id">The entrepreneur's id.</param>
        /// <returns>Whether there was an entrepreneur with the given id.</returns>
        public bool RemoveUserAsEntrepreneurById(string id) =>
            Singleton<SessionManager>.Instance.GetById(id) is UserSession session
                ? RemoveUserAsEntrepreneurByName(session.UserData.Name)
                : false;

        /// <summary>
        /// Removes an entrepreneur with a concrete name.
        /// </summary>
        /// <param name="name">The entrepreneur's name.</param>
        /// <returns>Whether there was an entrepreneur with the given name.</returns>
        public bool RemoveUserAsEntrepreneurByName(string name)
        {
            if(this.entrepreneurs.RemoveAll(e => e.Name == name) == 0)
            {
                return false;
            }

            Singleton<SessionManager>.Instance.RemoveUserByName(name);
            return true;
        }

        /// <summary>
        /// Gets the entrepreneur with a concrete id.
        /// </summary>
        /// <param name="id">The entrepreneur's id.</param>
        /// <returns>The entrepreneur, or null if there's no entrepreneur with the given id.</returns>
        public Entrepreneur? GetById(string id) =>
            this.entrepreneurs.Where(e => e.Id == id).FirstOrDefault();

        /// <summary>
        /// Gets the entrepreneur with a concrete name.
        /// </summary>
        /// <param name="name">The entrepreneur's name.</param>
        /// <returns>The entrepreneur, or null if there's no entrepreneur with the given name.</returns>
        public Entrepreneur? GetByName(string name) =>
            this.entrepreneurs.Where(e => e.Name == name).FirstOrDefault();

        /// <summary>
        /// Loads all entrepreneurs from a JSON file.
        /// </summary>
        /// <param name="path">The main directory's file.</param>
        public void LoadEntrepreneurs(string path)
        {
            List<Entrepreneur> entrepreneurs = SerializationUtils.DeserializeJsonListFromIntermediate<Entrepreneur, JsonEntrepreneur>($"{path}/entrepreneurs.json").ToList();
            this.entrepreneurs = entrepreneurs;
        }

        /// <summary>
        /// Saves all entrepreneurs from a JSON file.
        /// </summary>
        /// <param name="path">The main directory's file.</param>
        public void SaveEntrepreneurs(string path)
        {
            SerializationUtils.SerializeJsonListWithIntermediate<Entrepreneur, JsonEntrepreneur>($"{path}/entrepreneurs.json", this.entrepreneurs);
        }
    }
}
