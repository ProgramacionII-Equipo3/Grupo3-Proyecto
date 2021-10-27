using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Library.Core;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This class represents a company which can sell materials to entrepreneurs.
    /// </summary>
    public class Company : IPublisher, ISentMaterialReportCreator
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

        ///
        public Company(string name, ContactInfo contactInfo, string heading)
        {
            this.Name = name;
            this.contactInfo = contactInfo;
            this.Heading = heading;
        }

        /// <summary>
        /// Returns whether a user represents this company.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>Whether it belongs to the company.</returns>
        public bool HasUser(UserId id) =>
            this.representants.Any(repId => repId.Equals(id));

        /// <summary>
        /// Adds a user into the list of representants.
        /// </summary>
        /// <param name="id">The user's id.</param>
        public void AddUser(UserId id) =>
            this.representants.Add(id);

        List<MaterialPublication> IPublisher.publications { get; } = new List<MaterialPublication>();

        List<MaterialSalesLine> ISentMaterialReportCreator.materialSales { get; } = new List<MaterialSalesLine>();
    }
}
