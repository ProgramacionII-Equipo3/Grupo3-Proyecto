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
                ("/invitecompany", "Crea una invitación para un representante de una companía y obtiene su respectivo código.", this.inviteCompany),
                ("/removecompany", "Elimina una compañía y sus respectivos usuarios.",                                          this.removeCompany),
                ("/removeuser",    "Elimina un usuario.",                                                                       this.removeUser)
            };
        }

        private (State, string?) inviteCompany()
        {
            string code = Administer.CreateCompanyInvitation();
            return (this, $"El nuevo código de invitación es: {code}.");
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
            "¿Qué quieres hacer?";

        /// <inheritdoc />
        protected override string getErrorString() =>
            "Opción inválida.";
    }
}
