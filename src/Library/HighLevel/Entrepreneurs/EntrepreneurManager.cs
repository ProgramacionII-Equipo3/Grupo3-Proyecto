using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents the highest level of encapsulation in handling entrepreneurs.
    /// </summary>
    public static class EntrepreneurManager
    {
        private static List<Entrepreneur> entrepeneurs = new List<Entrepreneur>();

        /// <summary>
        /// Gets the entrepreneur's users in the platform.
        /// </summary>
        public static ReadOnlyCollection<Entrepreneur> Entrepreneurs => entrepeneurs.AsReadOnly();

        /// <summary>
        /// Adds a new entrepreneur into the list.
        /// </summary>
        /// <param name="entrepreneur">The new entrepreneur.</param>
        public static void NewEntrepreneur(Entrepreneur entrepreneur)
        {
            entrepeneurs.Add(entrepreneur);
        }
    }
}
