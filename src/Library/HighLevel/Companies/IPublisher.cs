using System.Collections.Generic;
using System.Collections.ObjectModel;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This interface represents the responsibility of managing material publications.
    /// We created this interface because of DIP, that way the classes depend of an abstraction.
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// A private list of the publications.
        /// The class <see cref="List{T}" /> is used instead of the interface <see cref="IList{T}" />
        /// because the method <see cref="List{T}.AsReadOnly()" /> is neccesary for the property <see cref="IPublisher.Publications" />.
        /// </summary>
        protected List<MaterialPublication> publications { get; }

        /// <summary>
        /// A public read-only list of the publications.
        /// </summary>
        public ReadOnlyCollection<MaterialPublication> Publications => this.publications.AsReadOnly();

        /// <summary>
        /// Publishes a material.
        /// </summary>
        /// <param name="material">The material to publish.</param>
        /// <param name="amount">The amount of material.</param>
        /// <param name="price">The price of the material.</param>
        /// <param name="location">The pick-up location of the material.</param>
        /// <param name="type">The type of the material publication.</param>
        /// <param name="keywords">The keywords of the material.</param>
        /// <returns>Whether the operation was successful.</returns>
        public bool PublishMaterial(Material material, Amount amount, Price price, Location location, MaterialPublicationTypeData type, IList<string> keywords)
        {
            if (MaterialPublication.CreateInstance(material, amount, price, location, type, keywords) is MaterialPublication publication)
            {
                this.publications.Add(publication);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes a material publication.
        /// </summary>
        /// <param name="index">The index of the publication.</param>
        /// <returns>Whether the removal was successful.</returns>
        public bool RemovePublication(int index)
        {
            if (index < 0 || index >= this.publications.Count)
            {
                return false;
            }

            this.publications.RemoveAt(index);
            return true;
        }
    }
}
