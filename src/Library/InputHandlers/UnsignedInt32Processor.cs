using System;
using System.Globalization;
using Library.Core.Processing;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represent an input processor with processes an integer from input.
    /// </summary>
    public class UnsignedInt32Processor : IInputProcessor<int>
    {
        private int result;

        private readonly Func<string> initialResponseGetter;

        ///
        public UnsignedInt32Processor(Func<string> initialResponseGetter)
        {
            this.initialResponseGetter = initialResponseGetter;
        }

        string IInputHandler.GetDefaultResponse() => (this.initialResponseGetter)();

        Result<bool, string> IInputHandler.ProcessInput(string msg)
        {
            if(string.IsNullOrWhiteSpace(msg)) return Result<bool, string>.Err("A number was expected.");
            int result;
            if(int.TryParse(msg.Trim(), NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out result))
            {
                this.result = result;
                return Result<bool, string>.Ok(true);
            }
            return Result<bool, string>.Err("The given input is not a valid number.");
        }

        Result<int, string> IInputProcessor<int>.getResult() => Result<int, string>.Ok(this.result);

        void IInputHandler.Reset()
        {
            this.result = -1;
        }
    }
}
