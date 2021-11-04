using System.Collections.Generic;
using Ucu.Poo.Locations.Client;
using System.Linq;
using Library.Core;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This class represents a company which can sell materials to entrepreneurs.
    /// We used the ISP principle, this class does not depend of types that don't 
    /// use.
    /// /// </summary>
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
        /// Tge company´s location.
        /// </summary>
        /// <value></value>
        public Location location { get; private set; }

        /// <summary>
        /// The company's representants in the platform.
        /// </summary>
        private List<UserId> representants = new List<UserId>();
        private List<string> companiesCreated = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contactInfo"></param>
        /// <param name="heading"></param>
        /// <param name="location"></param>
        public Company(string name, ContactInfo contactInfo, string heading, Location location)
        {
            this.Name = name;
            this.contactInfo = contactInfo;
            this.Heading = heading;
            this.location = location;
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
