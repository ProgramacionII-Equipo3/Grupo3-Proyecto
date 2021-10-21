using Library.Core;
using Library.Core.Processing;

namespace Library.States
{
    public class InitialMenuState : IState
    {
        public (IState, string) ProcessMessage(UserId id, UserData data, string msg)
        {
            return (this, $"Message sent: {msg}");
        }
    }
}
