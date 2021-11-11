using Library.HighLevel.Administers;
using Library.HighLevel.Companies;
using Library.Core.Invitations;

namespace ProgramTests.Utils
{
    ///
    public static class InvitationUtils
    {
        ///
        public static void CreateInvitation()
        {
            InvitationManager.CreateInvitation(
                Administer.GenerateInvitation(),
                code => new CompanyInvitation(code)
            );
        }
    }
}