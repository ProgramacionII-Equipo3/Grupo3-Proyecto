using System;
using Library.Core.Processing;
using Library.Utils;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// This class represents an input processor which takes the result of another one and applies a transformation to it.
    /// </summary>
    /// <typeparam name="T">The type returned by the first processor.</typeparam>
    /// <typeparam name="U">The type of the resulting object.</typeparam>
    public class PipeProcessor<T, U> : InputProcessor<U>
    {
        private Func<T, Result<U, string>> func;
        private InputProcessor<T> processor;
        private U? result = default;

        /// <summary>
        /// Initializes an instance of <see cref="PipeProcessor{T, U}" />.
        /// </summary>
        /// <param name="func">The transformation function.</param>
        /// <param name="processor">The inner <see cref="InputProcessor{T}" />.</param>
        public PipeProcessor(Func<T, Result<U, string>> func, InputProcessor<T> processor)
        {
            this.func = func;
            this.processor = processor;
        }

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg) =>
            this.processor.GenerateFromInput(msg) is Result<T, string> middleResult
                ? middleResult.AndThen(
                    result => func(result).SwitchErr(
                        e =>
                        {
                            this.processor.Reset();
                            return $"{e}\n{this.processor.GetDefaultResponse()}";
                        }
                    ).AndThen(
                        result =>
                        {
                            this.result = result;
                            return Result<bool, string>.Ok(true);
                        }
                    )
                ) : Result<bool, string>.Ok(true);

        /// <inheritdoc />
        protected override Result<U, string> getResult() => Result<U, string>.Ok(this.result.Unwrap());

        /// <inheritdoc />
        public override string GetDefaultResponse() => this.processor.GetDefaultResponse();

        /// <inheritdoc />
        public override void Reset()
        {
            this.result = default;
            this.processor.Reset();
        }

        /// <summary>
        /// Creates a pipe processor whose piped process can't fail.
        /// </summary>
        /// <param name="func">The transformation function.</param>
        /// <param name="processor">The inner <see cref="InputProcessor{T}" />.</param>
        public static PipeProcessor<T, U> CreateInfallibleInstance(Func<T, U> func, InputProcessor<T> processor) =>
            new PipeProcessor<T, U>(
                v => Result<U, string>.Ok(func(v)),
                processor);
    }
}