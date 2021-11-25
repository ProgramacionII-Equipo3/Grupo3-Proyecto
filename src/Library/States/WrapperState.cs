using Library.Core;

namespace Library.States
{
    /// <summary>
    /// This class acts as a type wrapper for a <see cref="State" />.
    /// </summary>
    public class WrapperState : State
    {
        private State innerState;

        /// <summary>
        /// Initializes an instance of <see cref="WrapperState" />.
        /// </summary>
        /// <param name="innerState">The inner state.</param>
        public WrapperState(State innerState)
        {
            this.innerState = innerState;
        }

        /// <inheritdoc />
        public override string GetDefaultResponse() =>
            this.innerState.GetDefaultResponse();

        /// <inheritdoc />
        public override (State?, string?) ProcessMessage(string id, string msg) =>
            this.innerState.ProcessMessage(id, msg);
    }
}
