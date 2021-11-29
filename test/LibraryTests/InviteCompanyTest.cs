using Library;
using Library.Core.Invitations;
using Library.HighLevel.Administers;
using NUnit.Framework;

namespace ProgramTests
{
    /// <summary>
    /// Test if a Company can be invited to the platform.
    /// </summary>
    [TestFixture]
    public class InviteCompanyTest
    {
        /// <summary>
        /// Test SetUp.
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
            int invitationsLength = Singleton<InvitationManager>.Instance.InvitationCount;
            Administer.CreateCompanyInvitation();
            Assert.AreEqual(invitationsLength + 1, Singleton<InvitationManager>.Instance.InvitationCount);
        }
    }
}