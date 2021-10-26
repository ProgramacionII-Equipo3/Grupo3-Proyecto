using System;
using System.Globalization;
using Library.Core.Processing;
using BoxedInt = Library.RefWrapper<int>;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represent an input processor with processes an integer from input.
    /// </summary>
    public class UnsignedInt32Processor : IInputProcessor<BoxedInt>
    {
        private BoxedInt result;

        private readonly Func<string> initialResponseGetter;

        ///
        public UnsignedInt32Processor(Func<string> initialResponseGetter)
        {
            this.initialResponseGetter = initialResponseGetter;
        }

        string IInputHandler.GetDefaultResponse() => (this.initialResponseGetter)();

        (bool, string) IInputHandler.ProcessInput(string msg)
        {
            if(string.IsNullOrWhiteSpace(msg)) return (default, "A number was expected.");
            int result;
            if(int.TryParse(msg.Trim(), NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out result))
            {
                this.result = new BoxedInt(result);
                return (true, null);
            }
            return (default, "The given input is not a valid number.");
        }

        (BoxedInt, string) IInputProcessor<BoxedInt>.getResult() => (this.result, null);

        void IInputHandler.Reset()
        {
            this.result = null;
        }
    }
}
