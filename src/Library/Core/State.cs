using System;

namespace Library.Core
{
    /// <summary>
    /// This class represents a state.
    /// We created this class because of DIP, that way the other classes depend of abractions.
    /// Also because of LSP, this class is used by a lot of classes without collateral damage.
    /// </summary>
    public abstract class State
    {

        /// <summary>
        /// Processes a received message, returning the next state and the response message.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="msg">The message's text.</param>
        /// <returns>The next state and the response message. If the response message is null, the new state's default message can be used instead.</returns>
        public abstract (State?, string?) ProcessMessage(string id, string msg);

        /// <summary>
        /// Returns the first message the object uses to indicate what kind of input it wants.
        /// </summary>
        /// <returns>A string.</returns>
        public abstract string GetDefaultResponse();

        /// <summary>
        /// Determines the <see cref="State" /> of an user after the program begins to run.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="userData">The user's data.</param>
        /// <returns>A <see cref="State" />.</returns>
        public static State FromUserData(string id, UserData userData)
        {
            switch((userData.IsComplete, userData.UserType))
            {
                case (true, UserData.Type.ADMIN):
                    return new Library.States.Admins.AdminInitialMenuState();
                case (false, UserData.Type.ADMIN):
                    return new Library.States.Admins.AdminInitialMenuState();
                case (true, UserData.Type.COMPANY):
                    return new Library.States.Companies.CompanyInitialMenuState(id);
                case (true, UserData.Type.ENTREPRENEUR):
                    return new Library.States.Entrepreneurs.EntrepreneurInitialMenuState(id);
                case (false, UserData.Type.COMPANY):
                    return new Library.States.Companies.IncompleteCompanyRepresentativeState();
                case (false, UserData.Type.ENTREPRENEUR):
                    return new Library.States.Entrepreneurs.NewEntrepreneurState(id);
                default:
                    throw new Exception();
            }
        }
    }
}