using NUnit.Framework;
using Library.Core;
using Library.HighLevel.Accountability;
using Library.HighLevel.Companies;

namespace ProgramTests
{
    /// <summary>
    /// This Test is for verificates if a Company can accept an invitation to the platform.
    /// </summary>
    public class AcceptInvitationTest
    {
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void AcceptInvitation()
        {
            ContactInfo contactInfo;
            contactInfo.Email = "companysa@gmail.com";
            contactInfo.PhoneNumber = 098765432;
            Location location = new Location();
            Company company = new Company("Company.SA", contactInfo, "Arroz", location);
        }
    }
}