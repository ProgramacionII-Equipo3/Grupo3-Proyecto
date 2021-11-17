using System;
using System.Linq;
using Library.Core;
using Library.Utils;

namespace Library.States
{
    /// <summary>
    /// This class represents a <see cref="State" /> which handles the messages in a multiple-option fashion.
    /// </summary>
    public abstract class MultipleOptionState : State
    {
        private (string, Func<State>)[] commands { get; }

        /// <summary>
        /// Gets the string to send in order to notify the user that the data is invalid.
        /// </summary>
        /// <returns>A string.</returns>
        protected abstract string GetErrorString();

        /// <summary>
        /// Creates a <see cref="MultipleOptionState" /> with a given group of commands.
        /// </summary>
        /// <param name="commands">The group of commands.</param>
        protected MultipleOptionState(params (string, Func<State>)[] commands)
        {
            this.commands = commands;
        }

        /// <inheritdoc />
        public override (State, string) ProcessMessage(UserId id, UserData data, string msg) =>
            this.commands.Where((command) => command.Item1 == msg.Trim()).FirstOrNone().Map(
                command =>
                {
                    State newState = command.Item2();
                    return (newState, newState.GetDefaultResponse());
                },
                () => (this, $"{this.GetErrorString()}\n{this.GetDefaultResponse()}")
            );
    }
}
