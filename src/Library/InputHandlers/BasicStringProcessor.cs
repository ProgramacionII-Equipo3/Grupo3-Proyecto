using System;
using Library.Core.Processing;

namespace Library.InputHandlers
{
    /// <summary>
    /// Generates a string from a single input message, after trimming it.
    /// </summary>
    public class BasicStringProcessor : IInputProcessor<string>
    {
        private readonly Func<string> initialResponseGetter;
        private string result;

        /// <summary>
        /// Initializes an instance of <see cref="BasicStringProcessor" /> with the given default response getter.
        /// </summary>
        /// <param name="initialResponseGetter">The default response getter.</param>
        public BasicStringProcessor(Func<string> initialResponseGetter)
        {
            this.initialResponseGetter = initialResponseGetter;
        }

        string IInputHandler.GetDefaultResponse() => (this.initialResponseGetter)();

        Result<bool, string> IInputHandler.ProcessInput(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return Result<bool, string>.Err("A valid string was expected.");
            this.result = msg.Trim();
            return Result<bool, string>.Ok(true);
        }

        Result<string, string> IInputProcessor<string>.getResult() => Result<string, string>.Ok(this.result);

        void IInputHandler.Reset()
        {
            this.result = null;
        }
    }
}