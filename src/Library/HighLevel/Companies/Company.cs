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
    /// <summary>
    /// This class represents a company which can sell materials to entrepreneurs.
    /// We used the ISP principle, this class does not depend of types it doesn't
    /// use. We also implemented Creator, this class is the one that creates the
    /// list of representants. And Expert, the method of checking if the
    /// company has a representant and add users.
    /// </summary>
    public partial class Company
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
        /// It's a <see cref="List{T}"/> instead of an <see cref="IList{T}"/>
        /// so it's method <see cref="List{T}.RemoveAll(System.Predicate{T})"/> can be used.
        /// </summary>
        public List<string> Representants { get; private set; } = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Company"/> class.
        /// </summary>
        /// <param name="name">The company's name.</param>
        /// <param name="contactInfo">The company's contact info.</param>
        /// <param name="heading">The company's heading.</param>
        /// <param name="location">The company's location.</param>
        public Company(string name, ContactInfo contactInfo, string heading, Location location)
        {
            this.Name = name;
            this.ContactInfo = contactInfo;
            this.Heading = heading;
            this.Location = location;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Company"/> class from JSON data.
        /// </summary>
        /// <param name="name">The company's name.</param>
        /// <param name="contactInfo">The company's contact info.</param>
        /// <param name="heading">The company's heading.</param>
        /// <param name="location">The company's location.</param>
        /// <param name="representants">The company's representants' ids.</param>
        /// <param name="publications">The company's publications.</param>
        /// <param name="materialSales">The company's material sales.</param>
        [JsonConstructor]
        public Company(string name, ContactInfo contactInfo, string heading, Location location, List<string> representants, List<MaterialPublication> publications, IList<MaterialSalesLine> materialSales)
        {
            this.Name = name;
            this.ContactInfo = contactInfo;
            this.Heading = heading;
            this.Location = location;
            this.Representants = representants;
            this.Publications = publications;
            this.MaterialSales = materialSales;
        }

        /// <summary>
        /// Returns whether an user represents this company.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>Whether it belongs to the company.</returns>
        public bool HasUser(string id) =>
            this.Representants.Any(repId => repId.Equals(id));

        /// <summary>
        /// Adds an user into the list of representants.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>Whether the operation is successful.</returns>
        public bool AddUser(string id)
        {
            if (this.Representants.Contains(id)) return false;
            this.Representants.Add(id);
            return true;
        }

        /// <summary>
        /// Removes an user from the list of representants.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>Whether the operation is successful.</returns>
        public bool RemoveRepresentantFromList(string id) =>
            this.Representants.RemoveAll(userId => userId == id) > 0;

        /// <summary>
        /// Removes all users in a company.
        /// </summary>
        public void RemoveUsers()
        {
            foreach(string id in this.Representants.ToList())
            {
                Singleton<SessionManager>.Instance.RemoveUser(id);
            }
        }
    }
}