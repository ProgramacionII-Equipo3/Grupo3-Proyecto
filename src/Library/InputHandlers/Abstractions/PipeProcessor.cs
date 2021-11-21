using System;
using Library.Core.Processing;
using Library.Utils;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// This class represents an input processor which takes the result of another one and applies a transformation to it.
    /// </summary>
    /// <typeparam name="T">The type of the resulting object.</typeparam>
    public class PipeProcessor<T> : InputProcessor<T>
    {
        private readonly Func<string> initialResponseGetter;
        private Func<string, Result<T, string>?> inputHandler;
        private Action resetter;
        private T? result = default;

        private PipeProcessor(Func<string> initialResponseGetter, Func<string, Result<T, string>?> inputHandler, Action resetter)
        {
            this.initialResponseGetter = initialResponseGetter;
            this.inputHandler = inputHandler;
            this.resetter = resetter;
        }

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg) =>
            (this.inputHandler)(msg) is Result<T, string> processResult
                ? processResult.AndThen(
                    result =>
                    {
                        this.result = result;
                        return Result<bool, string>.Ok(true);
                    })
                : Result<bool, string>.Ok(false);

        /// <inheritdoc />
        protected override Result<T, string> getResult() => Result<T, string>.Ok(this.result.Unwrap());

        /// <inheritdoc />
        public override string GetDefaultResponse() => (this.initialResponseGetter)();

        /// <inheritdoc />
        public override void Reset()
        {
            this.result = default;
            (this.resetter)();
        }

        /// <summary>
        /// Creates a pipe processor.
        /// </summary>
        /// <param name="func">The transformation function.</param>
        /// <param name="processor">The inner <see cref="InputProcessor{T}" />.</param>
        /// <typeparam name="U">The type of the objects the inner <see cref="InputProcessor{T}" /> returns.</typeparam>
        public static PipeProcessor<T> CreateInstance<U>(Func<U, Result<T, string>> func, InputProcessor<U> processor)
        {
            return new PipeProcessor<T>(
                initialResponseGetter: processor.GetDefaultResponse,
                inputHandler: s =>
                    processor.GenerateFromInput(s)?.AndThen<T>(
                        result => func(result).Switch(
                            v => v,
                            e =>
                            {
                                processor.Reset();
                                return $"{e}\n{processor.GetDefaultResponse()}";
                            })),
                resetter: processor.Reset);
        }
    }
}