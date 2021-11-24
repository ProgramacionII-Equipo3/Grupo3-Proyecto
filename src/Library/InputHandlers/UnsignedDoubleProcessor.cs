using System;
using System.Globalization;
using Library.Core.Processing;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represent an input processor which processes a double floating point decimal from input.
    /// </summary>
    public class UnsignedDoubleProcessor : InputProcessor<double>
    {
        private double result = double.NaN;

        private readonly Func<string> initialResponseGetter;

        /// <summary>
        /// Initializes an instance of <see cref="UnsignedInt32Processor" />
        /// </summary>
        /// <param name="initialResponseGetter"></param>
        public UnsignedDoubleProcessor(Func<string> initialResponseGetter)
        {
            this.initialResponseGetter = initialResponseGetter;
        }

        /// <inheritdoc />
        public override string GetDefaultResponse() => (this.initialResponseGetter)();

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg)
        {
            if (msg == "\\") return Result<bool, string>.Ok(false);
            if (string.IsNullOrWhiteSpace(msg)) return Result<bool, string>.Err($"Se esperaba un número.\n{(this.initialResponseGetter)()}");
            double result;
            if (double.TryParse(msg.Trim(), NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out result))
            {
                this.result = result;
                return Result<bool, string>.Ok(true);
            }
            return Result<bool, string>.Err($"El input no corresponde a un número positivo válido.\n{(this.initialResponseGetter)()}");
        }

        /// <inheritdoc />
        protected override Result<double, string> getResult() => Result<double, string>.Ok(this.result);

        /// <inheritdoc />
        public override void Reset()
        {
            this.result = double.NaN;
        }
    }
}
