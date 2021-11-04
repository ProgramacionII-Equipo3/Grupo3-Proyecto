using NUnit.Framework;
using Library.Core.Invitations;

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
            InvitationManager.CreateInvitation();
            InvitationManager.CreateInvitation();
            int expected = 3;
            int invitationsLength = InvitationManager.invitations.Count;
            Assert.AreEqual(invitationsLength, expected);
        }
    }
}