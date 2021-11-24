using Library.Core;
using Library.Core.Distribution;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;

namespace Library.States.Admins
{
    /// <summary>
    /// This class represents the state of an admin who is removing a user.
    /// </summary>
    public class AdminRemoveUserState : InputHandlerState
    {
        /// <summary>
        /// Initializes an instance of <see cref="AdminRemoveUserState" />.
        /// </summary>
        public AdminRemoveUserState(): base(
            inputHandler: ProcessorHandler.CreateInstance<string>(
                userName => Singleton<SessionManager>.Instance.RemoveUserByName(userName)
                    ? null
                    : "No existe un usuario con ese nombre.",
                new BasicStringProcessor(() => "Por favor ingrese el nombre del usuario que quieres eliminar.")
            ),
            exitState: () => new AdminInitialMenuState(),
            nextState: () => new AdminInitialMenuState()
        ) {}
    }
}
