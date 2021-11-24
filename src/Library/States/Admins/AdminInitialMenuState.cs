using System;
using Library.Core;
using Library.Core.Invitations;
using Library.HighLevel.Administers;
using Library.HighLevel.Companies;

namespace Library.States.Admins
{
    /// <summary>
    /// This class represents the <see cref="Library.Core.State" /> of an admin in the initial menu.
    /// </summary>
    public class AdminInitialMenuState : MultipleOptionState
    {
        /// <summary>
        /// Initializes an instance of <see cref="AdminInitialMenuState" />.
        /// </summary>
        public AdminInitialMenuState()
        {
            this.commands = new (string, string, Func<(State, string?)>)[]
            {
                ("/invitecompany", "Create a company invitation and get its code", this.inviteCompany),
                ("/removecompany", "Remove a company and its users",               this.removeCompany),
                ("/removeuser",    "Remove a user",                                this.removeUser)
            };
        }

        private (State, string?) inviteCompany()
        {
            string code = Administer.CreateCompanyInvitation();
            return (this, $"The new invitation's code is {code}.");
        }

        private (State, string?) removeCompany()
        {
            return (new AdminRemoveCompanyState(), null);
        }

        private (State, string?) removeUser()
        {
            return (new AdminRemoveUserState(), null);
        }

        /// <inheritdoc />
        protected override string getInitialResponse() =>
            "What do you want to do?";

        /// <inheritdoc />
        protected override string getErrorString() =>
            "Invalid option.";
    }
}
