using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Library.Core;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This class represents a company which can sell materials to entrepreneurs.
    /// </summary>
    public class Company : IPublisher<uint>, ISentMaterialReportCreator
    {
        /// <summary>
        /// The company's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The company's contact information.
        /// </summary>
        public ContactInfo contactInfo { get; private set; }

        /// <summary>
        /// The company's heading.
        /// </summary>
        public string Heading { get; private set; }

        /// <summary>
        /// The company's representants in the platform.
        /// </summary>
        private List<UserId> representants = new List<UserId>();

        /// <summary>
        /// Returns whether a user represents this company.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>Whether it belongs to the company.</returns>
        public bool HasUser(UserId id) =>
            this.representants.Any(repId => repId == id);

        /// <summary>
        /// A private list of the company's active publications in the platform.
        /// </summary>
        private List<MaterialPublication> publications { get; set; } = new List<MaterialPublication>();

        /// <summary>
        /// A public read-only list of the company's active publications in the platform.
        /// </summary>
        public ReadOnlyCollection<MaterialPublication> Publications => this.publications.AsReadOnly();

        public override bool PublishMaterial(Material material, Amount amount, Price price, Location location)
        {
//            this.publications.Add(MaterialPublication.)
        }

        public override bool RemovePublication(uint id)
        {

        }
    }
}
