using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Library.Utils;

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
        /// Gets the entrepreneur's users in the platform.
        /// </summary>
        public ReadOnlyCollection<Entrepreneur> Entrepreneurs => entrepreneurs.AsReadOnly();

        /// <summary>
        /// Adds a new entrepreneur into the list.
        /// </summary>
        /// <param name="entrepreneur">The new entrepreneur.</param>
        public void NewEntrepreneur(Entrepreneur entrepreneur)
        {
            entrepreneurs.Add(entrepreneur);
        }

        /// <summary>
        /// Gets the entrepreneur with a concrete id.
        /// </summary>
        /// <param name="id">The entrepreneur's id.</param>
        /// <returns>The entrepreneur, or null if there's no entrepreneur with the given id.</returns>
        public Entrepreneur? GetById(string id) =>
            this.entrepreneurs.Where(e => e.Id == id).FirstOrDefault();

        /// <summary>
        /// Loads all entrepreneurs from a JSON file.
        /// </summary>
        /// <param name="path">The main directory's file.</param>
        public void LoadEntrepreneurs(string path)
        {
            Entrepreneur[] entrepreneurs = SerializationUtils.DeserializeJSON<Entrepreneur[]>(path + "/entrepreneurs.json");
        }
    }
}
