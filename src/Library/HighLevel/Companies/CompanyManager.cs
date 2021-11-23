using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;
using System.Linq;
using Library.Core;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;
using Library.Utils;

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
        /// because the method <see cref="List{T}.AsReadOnly()" /> is necessary for the property <see cref="CompanyManager.Companies" />.
        /// </summary>
        private List<Company> companies = new List<Company>();

        /// <summary>
        /// Gets a public read-only list of the companies.
        /// </summary>
        public ReadOnlyCollection<Company> Companies => companies.AsReadOnly();

        /// <summary>
        /// Gets the company a concrete user represents.
        /// </summary>
        /// <param name="userId">The user's id.</param>
        /// <returns>A company, or null if the user doesn't represent a company.</returns>
        public Company? GetCompanyOf(string userId) =>
            companies.Where(company => company.HasUser(userId)).FirstOrDefault();

        /// <summary>
        /// Gets the <see cref="Company" /> with a concrete name.
        /// </summary>
        /// <param name="name">The company's name.</param>
        /// <returns>A company, or null if there is no company with that name.</returns>
        public Company? GetByName(string name) =>
            companies.Where(company => company.Name == name).FirstOrDefault();

        /// <summary>
        /// Creates an instance of <see cref="Company" />, adding it to the list.
        /// </summary>
        /// <returns>The created instance, or null if there's already a company with the same name.</returns>
        /// <param name="name">The company's name.</param>
        /// <param name="contactInfo">The company's contact info.</param>
        /// <param name="heading">The company's heading.</param>
        /// <param name="location">The company's location.</param>
        public Company? CreateCompany(string name, ContactInfo contactInfo, string heading, Location location)
        {
            name = name.Trim();
            heading = heading.Trim();
            if (GetByName(name) != null)
            {
                return null;
            }

            Company result = new Company(name, contactInfo, heading, location);
            companies.Add(result);
            return result;
        }

        /// <summary>
        /// The list of all publications made by all companies.
        /// </summary>
        public List<AssignedMaterialPublication> Publications =>
            this.Companies.SelectMany(company => company.AssignedPublications).ToList();

        /// <summary>
        /// Removes a company.
        /// </summary>
        /// <param name="name">The company's name.</param>
        /// <returns>Whether the company was successfully removed.</returns>
        public bool RemoveCompany(string name)
        {
            name = name.Trim();
            Company? company = this.companies.Where(company => company.Name == name).FirstOrDefault();
            if (company == null) return false;
            company.RemoveUsers();
            return true;
        }

        /// <summary>
        /// Loads all companies from a JSON file.
        /// </summary>
        /// <param name="path">The main directory's path.</param>
        public void LoadCompanies(string path)
        {
            List<Company> companies = SerializationUtils.DeserializeJsonListFromIntermediate<Company, JsonCompany>($"{path}/companies.json").ToList();
            this.companies = companies;
        }

//        Singleton<CompanyManager>.Instance.SaveCompanies(path);
        /// <summary>
        /// Saves all companies from a JSON file.
        /// </summary>
        /// <param name="path">The main directory's path.</param>
        public void SaveCompanies(string path)
        {
            Company[] companies = this.companies.ToArray();
            SerializationUtils.SerializeJsonListWithIntermediate<Company, JsonCompany>($"{path}/companies.json", companies);
        }
    }
}