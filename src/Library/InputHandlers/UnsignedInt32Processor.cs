using System;
using System.Globalization;
using Library.Core.Processing;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represent an input processor which processes an integer from input.
    /// </summary>
    public class UnsignedInt32Processor : InputProcessor<int>
    {
        private int result = -1;

        private readonly Func<string> initialResponseGetter;

        /// <summary>
        /// Initializes an instance of <see cref="UnsignedInt32Processor" />
        /// </summary>
        /// <param name="initialResponseGetter"></param>
        public UnsignedInt32Processor(Func<string> initialResponseGetter)
        {
            this.initialResponseGetter = initialResponseGetter;
        }

        /// <inheritdoc />
        public override string GetDefaultResponse() => (this.initialResponseGetter)();

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg)
        {
            if(msg == "\\") return Result<bool, string>.Ok(false);
            if(string.IsNullOrWhiteSpace(msg)) return Result<bool, string>.Err($"A number was expected.\n{(this.initialResponseGetter)()}");
            int result;
            if(int.TryParse(msg.Trim(), NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out result))
            {
                this.result = result;
                return Result<bool, string>.Ok(true);
            }
            return Result<bool, string>.Err($"The given input is not a valid number.\n{(this.initialResponseGetter)()}");
        }

        /// <inheritdoc />
        protected override Result<int, string> getResult() => Result<int, string>.Ok(this.result);

        /// <inheritdoc />
        public override void Reset()
        {
            this.result = -1;
        }
    }
}
