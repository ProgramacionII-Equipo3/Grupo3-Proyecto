using System.Collections.Generic;
using System.Collections.ObjectModel;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This class represents the responsibility of managing material publications.
    /// </summary>
    /// <typeparam name="TId"> The type for the ids of the publications </typeparam>
    public interface IPublisher<TId>
    {
        /// <summary>
        /// A public read-only list of the publications.
        /// </summary>
        public ReadOnlyCollection<MaterialPublication> Publications { get; }

        /// <summary>
        /// Adds a publication to the list.
        /// </summary>
        /// <param name="material">The publication's material.</param>
        /// <param name="amount">The publication's amount of material.</param>
        /// <param name="price">The publication's price of the material.</param>
        /// <param name="location">The publication's pickup location of the material.</param>
        /// <returns>true if the publication was successful, false if not.</returns>
        public bool PublishMaterial(Material material, Amount amount, Price price, Location location);

        /// <summary>
        /// Removes a publication.
        /// </summary>
        /// <param name="id">The publication's id.</param>
        /// <returns>true if the removal was successful, false if not.</returns>
        public bool RemovePublication(TId id);
    }
}