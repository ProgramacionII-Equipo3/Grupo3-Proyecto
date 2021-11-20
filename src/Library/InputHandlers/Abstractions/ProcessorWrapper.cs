using Library.Core.Processing;
using Library.Utils;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// Act as a type wrapper for a specific type of <see cref="InputProcessor{T}" />.
    /// </summary>
    /// <typeparam name="T">The type the inner processor returns.</typeparam>
    public abstract class ProcessorWrapper<T> : InputProcessor<T>
    {
        private InputProcessor<T> innerProcessor;
        private T? result = default;

        /// <summary>
        /// Initializes an instance of <see cref="ProcessorWrapper{T}" />.
        /// </summary>
        /// <param name="innerProcessor">The processor which receives the data and returns the object.</param>
        protected ProcessorWrapper(InputProcessor<T> innerProcessor)
        {
            this.innerProcessor = innerProcessor;
        }

        /// <inheritdoc />
        public override string GetDefaultResponse() => this.innerProcessor.GetDefaultResponse();

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg)
        {
            if(this.result != null) return Result<bool, string>.Ok(true);
            return this.innerProcessor.GenerateFromInput(msg) is Result<T, string> result
                ? result.SwitchOk(
                    v =>
                    {
                        this.result = v;
                        return true;
                    })
                : Result<bool, string>.Ok(false);
        }

        /// <inheritdoc />
        protected override Result<T, string> getResult() => Result<T, string>.Ok(this.result.Unwrap());

        /// <inheritdoc />
        public override void Reset()
        {
            this.result = default;
            this.innerProcessor.Reset();
        }
    }
}
