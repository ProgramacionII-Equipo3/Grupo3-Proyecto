using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This class represents a company which can sell materials to entrepreneurs.
    /// We used the ISP principle, this class does not depend of types it doesn't
    /// use. We also implemented Creator, this class is the one that creates the
    /// list of representants. And Expert, the method of checking if the
    /// company has a representant and add users.
    /// </summary>
    public class Company : IPublisher, ISentMaterialReportCreator
    {
        /// <summary>
        /// Gets the company's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the company's contact information.
        /// </summary>
        public ContactInfo ContactInfo { get; private set; }

        /// <summary>
        /// Gets the company's heading.
        /// </summary>
        public string Heading { get; private set; }

        /// <summary>
        /// Gets the company's location.
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// The company's representants in the platform.
        /// </summary>
        private List<UserId> representants = new List<UserId>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Company"/> class.
        /// </summary>
        /// <param name="name">The company´s name.</param>
        /// <param name="contactInfo">The company´s contact info.</param>
        /// <param name="heading">The company´s heading.</param>
        /// <param name="location">The company´s location.</param>
        public Company(string name, ContactInfo contactInfo, string heading, Location location)
        {
            this.Name = name;
            this.ContactInfo = contactInfo;
            this.Heading = heading;
            this.Location = location;
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

        /// <summary>
        /// Gets a list of Material Publications.
        /// </summary>
        List<MaterialPublication> IPublisher.Publications { get; } = new List<MaterialPublication>();

        /// <summary>
        /// Gets a list of Material Sales.
        /// </summary>
        List<MaterialSalesLine> ISentMaterialReportCreator.MaterialSales { get; } = new List<MaterialSalesLine>();
    }
}
