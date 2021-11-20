using System;
using Library.Core.Processing;

namespace Library.InputHandlers
{
    /// <summary>
    /// Generates a string from a single input message, after trimming it.
    /// </summary>
    public class BasicStringProcessor : InputProcessor<string>
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
            this.result = string.Empty;
        }

        /// <inheritdoc />
        public override string GetDefaultResponse() => (this.initialResponseGetter)();

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg)
        {
            if(msg == "\\") return Result<bool, string>.Ok(false);
            if (string.IsNullOrWhiteSpace(msg)) return Result<bool, string>.Err("A valid string was expected.");
            this.result = msg.Trim();
            return Result<bool, string>.Ok(true);
        }

        /// <inheritdoc />
        protected override Result<string, string> getResult() => Result<string, string>.Ok(this.result);

        /// <inheritdoc />
        public override void Reset()
        {
            this.result = string.Empty;
        }
    }
}
