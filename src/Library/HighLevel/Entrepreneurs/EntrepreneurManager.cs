using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents the highest level of encapsulation in handling entrepreneurs.
    /// </summary>
    public class EntrepreneurManager
    {
        private List<Entrepreneur> entrepeneurs = new List<Entrepreneur>();

        /// <summary>
        /// The entrepreneur's users in the platform.
        /// </summary>
        public ReadOnlyCollection<Entrepreneur> Entrepreneurs => entrepeneurs.AsReadOnly();

        /// <summary>
        /// Adds a new entrepreneur into the list.
        /// </summary>
        /// <param name="entrepreneur">The new entrepreneur.</param>
        public void NewEntrepreneur(Entrepreneur entrepreneur)
        {
            entrepeneurs.Add(entrepreneur);
        }
    }
}
