using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Library.Core;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This class represents the highest level of encapsulation in handling companies.
    /// Created because of SRP and Don't talk to Strangers,
    /// that way the class CompanyManager is the one responsible of creating the list
    /// of companies and getting the companies and not Company by itself.
    /// </summary>
    public class CompanyManager
    {
        /// <summary>
        /// The list of companies.
        /// The class <see cref="List{T}" /> is used instead of the interface <see cref="IList{T}" />
        /// because the method <see cref="List{T}.AsReadOnly()" /> is neccesary for the property <see cref="CompanyManager.Companies" />.
        /// </summary>
        private List<Company> companies = new List<Company>();

        /// <summary>
        /// A public read-only list of the companies.
        /// </summary>
        public ReadOnlyCollection<Company> Companies => companies.AsReadOnly();

        /// <summary>
        /// Gets the company a concrete user represents.
        /// </summary>
        /// <param name="userId">The user's id.</param>
        /// <returns>A company, or null if the user doesn't represent a company.</returns>
        public Company GetCompanyOf(UserId userId) =>
            companies.Where(company => company.HasUser(userId)).FirstOrDefault();

        /// <summary>
        /// Gets an enumerable of companies whose names are similar to a given one.
        /// </summary>
        /// <param name="name">The company name to compare.</param>
        /// <returns>A list of companies.</returns>
        public IEnumerable<Company> GetCompaniesWithNamesSimilarTo(string name) =>
            companies.Where(company => Utils.AreSimilar(company.Name, name));

        /// <summary>
        /// Gets the <see cref="Company" /> with a concrete name.
        /// </summary>
        /// <param name="name">The company's name.</param>
        /// <returns>A company, or null if there is no company with that name.</returns>
        public Company GetByName(string name) =>
            companies.Where(company => company.Name == name).FirstOrDefault();

        /// <summary>
        /// Creates an instance of <see cref="Company" />, adding it to the list.
        /// </summary>
        /// <returns>The created instance, or null if there's already a company with the same name.</returns>
        /// <param name="name">The comany´s name.</param>
        /// <param name="contactInfo">The comany´s contact info.</param>
        /// <param name="heading">The company´s heading.</param>
        /// <param name="location">The company´s location.</param>
        public Company CreateCompany(string name, ContactInfo contactInfo, string heading, Location location)
        {
            if (GetByName(name) != null)
            {
                return null;
            }

            Company result = new Company(name, contactInfo, heading, location);
            companies.Add(result);
            return result;
        }
    }
}
