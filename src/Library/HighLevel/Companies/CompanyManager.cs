using System.Collections.Generic;
using System.Linq;
using Library;
using Library.Core;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This class represents the highest level of encapsulation in handling companies.
    /// </summary>
    public static class CompanyManager
    {
        private static List<Company> companies = new List<Company>();

        /// <summary>
        /// Gets the company a concrete user represents.
        /// </summary>
        /// <param name="userId">The user's id.</param>
        /// <returns>A company, or null if the user doesn't represent a company.</returns>
        public static Company GetCompanyOf(UserId userId) =>
            companies.Where(company => company.HasUser(userId)).FirstOrDefault();

        /// <summary>
        /// Gets an enumerable of companies whose names are similar to a given one.
        /// </summary>
        /// <param name="name">The company name to compare.</param>
        /// <returns>A list of companies.</returns>
        public static IEnumerable<Company> GetCompaniesWithNamesSimilarTo(string name) =>
            companies.Where(company => Utils.AreSimilar(company.Name, name));

        /// <summary>
        /// Gets the <see cref="Company" /> with a concrete name.
        /// </summary>
        /// <param name="name">The company's name.</param>
        /// <returns>A company, or null if there is no company with that name.</returns>
        public static Company GetByName(string name) =>
            companies.Where(company => company.Name == name).FirstOrDefault();

        /// <summary>
        /// Creates an instance of <see cref="Company" />, adding it to the list.
        /// </summary>
        /// <returns>The created instance, or null if there's already a company with the same name.</returns>
        public static Company CreateCompany(string name, ContactInfo contactInfo, string heading)
        {
            if(GetByName(name) != null) return null;

            Company result = new Company(name, contactInfo, heading);
            companies.Add(result);
            return result;
        }
    }
}
