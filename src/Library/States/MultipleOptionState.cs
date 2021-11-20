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
        /// <summary>
        /// Gets the list of the commands supported by this <see cref="MultipleOptionState" />.
        /// </summary>
        protected (string, string, Func<(State, string?)>)[] commands = new (string, string, Func<(State, string?)>)[0];

        /// <summary>
        /// Returns the message the program sends before asking for an option.
        /// </summary>
        /// <returns>A string.</returns>
        protected abstract string GetInitialResponse();

        /// <inheritdoc />
        public override sealed string GetDefaultResponse() =>
            this.GetInitialResponse() + "\n        " + string.Join("\n        ", commands.Select(command => $"{command.Item1}: {command.Item2}"));

        /// <summary>
        /// Gets the string to send in order to notify the user that the data is invalid.
        /// </summary>
        /// <returns>A string.</returns>
        protected abstract string GetErrorString();

        /// <inheritdoc />
        public override (State, string?, UserData?) ProcessMessage(string id, UserData data, string msg)
        {
            (string, string, Func<(State, string?)>)? command = this.commands
                .Where((command) => command.Item1 == msg.Trim())
                .OfType<(string, string, Func<(State, string?)>)?>()
                .FirstOrDefault();
            
            if(command is (string, string, Func<(State, string?)>) c)
            {
                var (newState, res) = c.Item3();
                return (
                    newState,
                    res != null
                        ? $"{res}\n{newState.GetDefaultResponse()}"
                        : newState.GetDefaultResponse(),
                    null
                );
            } else {
                return (this, $"{this.GetErrorString()}\n{this.GetDefaultResponse()}", null);
            }
        }
    }
}
