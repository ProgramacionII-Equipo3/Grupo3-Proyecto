using System.Collections.Generic;

namespace Library.Core.Company
{
    /// <summary>
    /// Stores data of a Company
    /// </summary>
    public class Company
    {
        /// <summary>
        /// The name of the Company
        /// </summary>
        public string Name;
        /// <summary>
        /// The forms of contact the Company
        /// </summary>
        public ContactInfo ContactInfo;
        public string Heading;
        /// <summary>
        /// Represents the list of representants of the Company
        /// </summary>
        private List<UserId> Representants;
        /// <summary>
        /// Company Constructor
        /// </summary>
        /// <param name= "name">Company's name</param>
        /// <param name= "info">Company contact info</param>
        /// <param name= "representants">List of representants of the Company</param>
        public Company(string name, ContactInfo info, List<UserId> representants)
        {
            this.Name = name;
            this.ContactInfo = info;
            this.Representants = representants;
        }

        /// <summary>
        /// This method verificates if a company has a specific user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool HasUser(UserId user)
        {
            if(this.Representants.Contains(user))
            {
                return true;
            }
            else
            {
                return false;
            } 
            
        }

    }
}