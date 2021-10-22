using System.Collections.Generic;
using System.Linq;
using Library.Core;

namespace Library.Companies
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
    }
}
