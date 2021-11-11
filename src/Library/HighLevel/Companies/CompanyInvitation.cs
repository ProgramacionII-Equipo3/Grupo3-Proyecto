using Library.Core;
using Library.Core.Distribution;
using Library.Core.Invitations;
using Library.States;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// Represents an invitation to the platform for a company representative.
    /// We used the DIP pattern, CompanyInvitation depends of an abstract class.
    /// </summary>
    public class CompanyInvitation : Invitation
    {

        ///
        public CompanyInvitation(string code): base(code) {}

        /// <inheritdoc />
        public override string Validate(UserId userId)
        {
            State newState = new IncompleteCompanyRepresentativeState();
            SessionManager.NewUser(
                id: userId,
                userData: new UserData(),
                state: newState
            );
            return newState.GetDefaultResponse();
        }
    }
}
