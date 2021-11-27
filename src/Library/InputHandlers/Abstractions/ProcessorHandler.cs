using System;
using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// Represents an <see cref="InputHandler" /> which uses an input processor, and a function to be given its result.
    /// </summary>
    /// <typeparam name="T">The type of the object the input processor returns, which is used by the "action" operation.</typeparam>
    public class ProcessorHandler<T> : InputHandler
    {
        private Func<T, string?> f;
        private InputProcessor<T> processor;

        /// <summary>
        /// Initializes an instance of <see cref="ProcessorHandler{T}" />.
        /// </summary>
        /// <param name="f">The operation to do with the resulting input, and returns a not-null string if there was an error.</param>
        /// <param name="processor">The input processor.</param>
        public ProcessorHandler(Func<T, string?> f, InputProcessor<T> processor)
        {
            this.f = f;
            this.processor = processor;
        }

        /// <summary>
        /// Creates an instance of <see cref="ProcessorHandler{T}" /> which can't send an error
        /// in the function which gets the generated value.
        /// </summary>
        /// <param name="action">The operation to do with the resulting input.</param>
        /// <param name="processor">The input processor.</param>
        public static ProcessorHandler<T> CreateInfallibleInstance(Action<T> action, InputProcessor<T> processor) =>
            new ProcessorHandler<T>(v => {
                action(v);
                return null;
            }, processor);

        /// <inheritdoc />
        public override string GetDefaultResponse() => this.processor.GetDefaultResponse();

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg) =>
            this.processor.GenerateFromInput(msg) is Result<T, string> result
                ? result.AndThen(
                    v =>
                    {
                        string? res = f(v);
                        if (res is null) return Result<bool, string>.Ok(true);
                        this.processor.Reset();
                        return Result<bool, string>.Err($"{res}\n{processor.GetDefaultResponse()}");
                    }
                )
                : Result<bool, string>.Ok(false);

        /// <inheritdoc />
        public override void Reset()
        {
            this.processor.Reset();
        }
    }
}
