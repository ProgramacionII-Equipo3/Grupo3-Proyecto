using NUnit.Framework;
using Library.Core.Invitations;
using ProgramTests.Utils;

namespace ProgramTests
{
    /// <summary>
    /// 
    /// </summary>
    public class InviteCompanyTest
    {
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {      
        }

        /// <summary>
        /// This test proves that as an admin I can create an invitation
        /// As we can't expect a certain invitation code because it's
        /// generated randomly, we check if the list of invitations has 
        /// the same number of invitations as expected.
        /// </summary>
        [Test]
        public void InviteCompany()
        {
            InvitationUtils.CreateInvitation();
            InvitationUtils.CreateInvitation();
            int expected = 3;
            int invitationsLength = InvitationManager.InvitationCount;
            Assert.AreEqual(invitationsLength, expected);
        }
    }
}