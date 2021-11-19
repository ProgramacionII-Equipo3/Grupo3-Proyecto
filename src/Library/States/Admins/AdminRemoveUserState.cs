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
        /// <inheritdoc />
        public override bool IsComplete => true;

        /// <inheritdoc />
        public override State.Type UserType => State.Type.ADMIN;

        /// <summary>
        /// Initializes an instance of <see cref="AdminRemoveUserState" />.
        /// </summary>
        public AdminRemoveUserState(): base(
            inputHandler: ProcessorHandler.CreateInstance<string>(
                userName => Singleton<SessionManager>.Instance.RemoveUserByName(userName)
                    ? null
                    : "There's no user with that name.",
                new BasicStringProcessor(() => "Please insert the name of the user you want to remove.")
            ),
            exitState: () => new AdminInitialMenuState(),
            nextState: () => new AdminInitialMenuState()
        ) {}
    }
}
