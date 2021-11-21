using System;
using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// Represents an <see cref="InputProcessor{T}" /> which transforms a value into another of the same type.
    /// </summary>
    /// <typeparam name="T">The type of the value the object transforms.</typeparam>
    public class ProcessorModifier<T> : InputProcessor<T>
    {
        private Func<string, T, Result<T, string>?> inputHandler;
        private readonly Func<string> initialResponseGetter;
        private Action resetter;
        private readonly Func<T> initialValueGetter;
        private T result;

        private ProcessorModifier(Func<T> initialValueGetter, Func<string, T, Result<T, string>?> inputHandler, Func<string> initialResponseGetter, Action resetter)
        {
            this.initialValueGetter = initialValueGetter;
            this.result = initialValueGetter();
            this.inputHandler = inputHandler;
            this.initialResponseGetter = initialResponseGetter;
            this.resetter = resetter;
        }

        /// <summary>
        /// Generates a function which can be passed to the <see cref="BaseFormProcessor{T, U}" /> constructor to represent an <see cref="InputHandler" />.
        /// </summary>
        /// <param name="f">The function which takes the initial value and the generated data, and returns the resulting value.</param>
        /// <param name="processor">The processor which generates the data.</param>
        /// <typeparam name="U">The type of the generated data.</typeparam>
        /// <returns>A function which receives a value getter and returns a <see cref="ProcessorModifier{T}" />.</returns>
        public static Func<Func<T>, ProcessorModifier<T>> CreateInstanceGetter<U>(Func<T, U, Result<T, string>> f, InputProcessor<U> processor)
        {
            return initialValueGetter => new ProcessorModifier<T> (
                initialValueGetter: initialValueGetter,
                inputHandler: (s, initialValue) => processor.GenerateFromInput(s)?.AndThen(
                    v =>
                    {
                        Result<T, string> res = f(initialValue, v);
                        res.RunIfErr(e =>
                        {
                            processor.Reset();
                        });
                        return res;
                    }
                ),
                initialResponseGetter: processor.GetDefaultResponse,
                resetter: processor.Reset
            );
        }

        /// <summary>
        /// Generates a function which can be passed to the <see cref="BaseFormProcessor{T, U}" /> constructor to represent an <see cref="InputHandler" />.
        /// </summary>
        /// <param name="f">The function which takes the initial value and the generated data, and returns the resulting value.</param>
        /// <param name="processor">The processor which generates the data.</param>
        /// <typeparam name="U">The type of the generated data.</typeparam>
        /// <returns>A function which receives a value getter and returns a <see cref="ProcessorModifier{T}" />.</returns>
        public static Func<Func<T>, ProcessorModifier<T>> CreateInfallibleInstanceGetter<U>(Func<T, U, T> f, InputProcessor<U> processor)
        {
            return initialValueGetter => new ProcessorModifier<T> (
                initialValueGetter: initialValueGetter,
                inputHandler: (s, initialValue) => processor.GenerateFromInput(s)?.AndThen(
                    v => Result<T, string>.Ok(f(initialValue, v))
                ),
                initialResponseGetter: processor.GetDefaultResponse,
                resetter: processor.Reset
            );
        }

        /// <inheritdoc />
        public override string GetDefaultResponse() => (this.initialResponseGetter)();

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg) =>
            (this.inputHandler)(msg, this.initialValueGetter()) is Result<T, string> result
                ? result.Map(
                    value =>
                    {
                        this.result = value;
                        return Result<bool, string>.Ok(true);
                    },
                    s => Result<bool, string>.Err(s)
                )
                : Result<bool, string>.Ok(false);

        /// <inheritdoc />
        protected override Result<T, string> getResult() => Result<T, string>.Ok(this.result);

        /// <inheritdoc />
        public override void Reset()
        {
            (this.resetter)();
            this.result = (this.initialValueGetter)();
        }
    }
}
