using Library.Core;

namespace Library.States
{
    public class WrapperState : State
    {
        private State innerState;

        public WrapperState(State innerState)
        {
            this.innerState = innerState;
        }

        /// <inheritdoc />
        public override string GetDefaultResponse() =>
            this.innerState.GetDefaultResponse();

        /// <inheritdoc />
        public override (State?, string?) ProcessMessage(string id, ref UserData data, string msg) =>
            this.innerState.ProcessMessage(id, ref data, msg);
    }
}
