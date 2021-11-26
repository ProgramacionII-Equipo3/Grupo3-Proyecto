using Library.Core;
using Library.Core.Distribution;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;

namespace Library.States.Admins
{
    /// <summary>
    /// This class represents the state of an admin who is removing an user.
    /// </summary>
    public class AdminRemoveUserState : InputHandlerState
    {
        /// <summary>
        /// Initializes an instance of <see cref="AdminRemoveUserState" />.
        /// </summary>
        public AdminRemoveUserState() : base(
            inputHandler: ProcessorHandler.CreateInstance<string>(
                userName =>
                {
                    if (Singleton<SessionManager>.Instance.GetByName(userName) is UserSession session)
                    {
                        if (session.UserData.UserType == UserData.Type.ADMIN)
                        {
                            return "No se puede eliminar un administrador.";
                        }
                        else if (Singleton<SessionManager>.Instance.RemoveUserByName(userName))
                        {
                            return null;
                        }
                    }
                    return "No existe un usuario con ese nombre.";
                },
                new BasicStringProcessor(() => "Por favor ingrese el nombre del usuario que quieres eliminar.")
            ),
            exitState: () => new AdminInitialMenuState(),
            nextState: () => new AdminInitialMenuState()
        )
        { }
    }
}
