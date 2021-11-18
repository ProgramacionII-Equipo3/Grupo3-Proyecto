using System;
using Library.Core;
using Library.Core.Invitations;
using Library.HighLevel.Administers;
using Library.HighLevel.Companies;

namespace Library.States.Admins
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminInitialMenuState : MultipleOptionState
    {
        /// <inheritdoc />
        public override bool IsComplete => true;

        /// <inheritdoc />
        public override State.Type UserType => State.Type.ADMIN;


        /// <summary>
        /// 
        /// </summary>
        public AdminInitialMenuState()
        {
            this.commands = new (string, string, Func<(State, string)>)[]
            {
                ("/invitecompany", "Create a company invitation and get its code", this.inviteCompany),
                ("/removecompany", "Remove a company and its users",               this.removeCompany),
                ("/removeuser",    "Remove a user",                                this.removeUser)
            };
        }

        private (State, string) inviteCompany()
        {
            string code = Administer.CreateCompanyInvitation();
            return (this, $"The new invitation's code is {code}.");
        }

        private (State, string) removeCompany()
        {
            throw new NotImplementedException();
        }

        private (State, string) removeUser()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override string GetInitialResponse() =>
            "What do you want to do?";

        /// <inheritdoc />
        protected override string GetErrorString() =>
            "Invalid option.";
    }
}
