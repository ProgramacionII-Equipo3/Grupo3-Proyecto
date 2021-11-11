using System;
using Library.Core;
using Library.Core.Processing;

namespace Library.States
{
    /// <summary>
    /// This class represents a State which basically works with an <see cref="IInputHandler" />.
    /// </summary>
    public abstract class InputHandlerState : State
    {
        private IInputHandler inputHandler { get; }

        private Func<State> exitState { get; }

        private Func<State> nextState { get; }

        ///
        protected InputHandlerState(IInputHandler inputHandler, Func<State> exitState, Func<State> nextState)
        {
            this.inputHandler = inputHandler;
            this.exitState = exitState;
            this.nextState = nextState;
        }

        /// <inheritdoc />
        public override (State, string) ProcessMessage(UserId id, UserData data, string msg) =>
            inputHandler.ProcessInput(msg).Map<(State, string)>(
                success => ((success ? (this.nextState)() : (this.exitState)()), null),
                s => (this, s)
            );

        /// <inheritdoc />
        public override string GetDefaultResponse() => this.inputHandler.GetDefaultResponse();
    }
}
