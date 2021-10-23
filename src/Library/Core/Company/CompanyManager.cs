using System.Collections.Generic;
namespace Library.Core.Company
{
    /// <summary>
    /// Store a list of all Companies and search the company of a specific user
    /// </summary>
    public static class CompanyManager
    {
        private static List<Company> Companies;
        
        /// <summary>
        /// Returns the company of a specific user
        /// </summary>
        /// <param name="user">User id to search his company</param>
        /// <returns></returns>
        public static Company GetCompany(UserId user)
        {
            Company result = null;
            foreach (var company in Companies)
            {
                if(company.HasUser(user) == true)
                {
                    result = company;
                }
            }
            return result;
        }
    }
}