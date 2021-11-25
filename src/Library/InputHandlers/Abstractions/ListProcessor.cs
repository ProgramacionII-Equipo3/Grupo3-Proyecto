using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// This class represents a processor which processes several objects of the same type.
    /// </summary>
    /// <typeparam name="T">The type of the elements which are processed into a list.</typeparam>
    public partial class ListProcessor<T> : InputProcessor<T[]>
    {
        private List<T> list = new List<T>();

        private Func<string> initialResponseGetter;

        private InnerProcessorState state;

        private InputProcessor<T> processor;

        /// <summary>
        /// Initializes an instance of <see cref="ListProcessor{T}" />.
        /// </summary>
        /// <param name="initialResponseGetter">The default response for the initial menu.</param>
        /// <param name="processor">The processor which generates the elements of the list.</param>
        public ListProcessor(Func<string> initialResponseGetter, InputProcessor<T> processor)
        {
            this.state = new InitialMenuState(this);
            this.initialResponseGetter = initialResponseGetter;
            this.processor = processor;
        }

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg) =>
            this.state.ProcessMessage(msg).SwitchErr(
                result =>
                {
                    var (newState, msg) = result;
                    this.state = newState;
                    return msg is null
                        ? this.GetDefaultResponse()
                        : msg;
                });

        /// <inheritdoc />
        protected override Result<T[], string> getResult() => Result<T[], string>.Ok(this.list.ToArray());

        /// <inheritdoc />
        public override string GetDefaultResponse() => this.state.GetDefaultResponse();

        /// <inheritdoc />
        public override void Reset()
        {
            this.list.Clear();
            this.processor.Reset();
        }
    }
}
