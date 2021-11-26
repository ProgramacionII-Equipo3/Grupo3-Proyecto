using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Library.Core;
using Library.Core.Distribution;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Companies
{
    public partial class Company
    {
        /// <summary>
        /// Gets a private list of the publications.
        /// </summary>
        public List<MaterialPublication> Publications { get; private set; } = new List<MaterialPublication>();

        /// <summary>
        /// Gets the list of publications, dinamically assigned to the company.
        /// </summary>
        public IList<AssignedMaterialPublication> AssignedPublications =>
            this.Publications.Select(pub => new AssignedMaterialPublication(
                company: this,
                publication: pub
            )).ToList();

        /// <summary>
        /// Publishes a material.
        /// </summary>
        /// <param name="material">The material to publish.</param>
        /// <param name="amount">The amount of material.</param>
        /// <param name="price">The price of the material.</param>
        /// <param name="location">The pick-up location of the material.</param>
        /// <param name="type">The type of the material publication.</param>
        /// <param name="keywords">The keywords of the material.</param>
        /// <param name="requirements">The requirements of the material.</param>
        /// <returns>Whether the operation was successful.</returns>
        public bool PublishMaterial(Material material, Amount amount, Price price, Location location, MaterialPublicationTypeData type, IList<string> keywords, IList<string> requirements)
        {
            if (MaterialPublication.CreateInstance(material, amount, price, location, type, keywords, requirements) is MaterialPublication publication)
            {
                if(this.Publications.Any(p => p.Material.Name == material.Name))
                    return false;
                this.Publications.Add(publication);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a material publication.
        /// </summary>
        /// <param name="name">The name of the publication.</param>
        /// <returns>Whether the removal was successful.</returns>
        public bool RemovePublication(string name)
        {
            return this.Publications.RemoveAll(m => m.Material.Name == name) >= 1;
        }
    }
}
