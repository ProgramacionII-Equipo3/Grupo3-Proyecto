using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        private List<Entrepreneur> entrepeneurs = new List<Entrepreneur>();

        /// <summary>
        /// Gets the entrepreneur's users in the platform.
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
