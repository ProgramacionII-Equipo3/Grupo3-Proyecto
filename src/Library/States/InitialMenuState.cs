using Library.Core;
using Library.Core.States;

namespace Library.States
{
    /// <summary>
    /// This class represents a mock user state, in which the bot returns every message it receives.
    /// </summary>
    public class InitialMenuState : State
    {
        /// <inheritdoc />
        public override (State, string) ProcessMessage(UserId id, UserData data, string msg)
        {
            return (this, $"Message sent: {msg}");
        }

        /// <inheritdoc />
        public override string GetDefaultResponse()
        {
            return "";
        }
    }
}
