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
        /// <summary>
        /// Creates instances of CompanyInvitation.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public CompanyInvitation(string code): base(code)
        {
        }

        /// <inheritdoc />
        public override string Validate(UserId userId)
        {
            SessionManager.NewUser(
                id: userId,
                userData: new UserData(),
                state: new IncompleteCompanyRepresentativeState());
            return "Please insert the company's name.";
        }
    }
}
