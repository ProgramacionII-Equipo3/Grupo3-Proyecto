using System;
using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// Represents an <see cref="IInputProcessor{T}" /> which transforms a value into another of the same type.
    /// </summary>
    public class ProcessorModifier<T> : IInputProcessor<T>
    {
        private Func<string, T, Option<Result<T, string>>> inputHandler;
        private readonly Func<string> initialResponseGetter;
        private Action resetter;
        private readonly Func<T> initialValueGetter;
        private T result;

        ///
        private ProcessorModifier(Func<T> initialValueGetter, Func<string, T, Option<Result<T, string>>> inputHandler, Func<string> initialResponseGetter, Action resetter)
        {
            this.initialValueGetter = initialValueGetter;
            this.inputHandler = inputHandler;
            this.initialResponseGetter = initialResponseGetter;
            this.resetter = resetter;
        }

        public static Func<Func<T>, ProcessorModifier<T>> CreateInstanceGetter<U>(Func<T, U, Result<T, string>> f, IInputProcessor<U> processor)
        {
            return initialValueGetter => new ProcessorModifier<T> (
                initialValueGetter: initialValueGetter,
                inputHandler: (s, initialValue) => processor.GenerateFromInput(s).MapValue(
                    result => result.AndThen(
                        v =>
                        {
                            Result<T, string> res = f(initialValue, v);
                            res.RunIfErr(e =>
                            {
                                processor.Reset();
                            });
                            return res;
                        }
                    )
                ),
                initialResponseGetter: processor.GetDefaultResponse,
                resetter: processor.Reset
            );
        }

        string IInputHandler.GetDefaultResponse() => (this.initialResponseGetter)();

        Result<bool, string> IInputHandler.ProcessInput(string msg) =>
            (this.inputHandler)(msg, this.initialValueGetter()).Map(
                result => result.Map(
                    value =>
                    {
                        this.result = value;
                        return Result<bool, string>.Ok(true);
                    },
                    s => Result<bool, string>.Err(s)
                ),
                () => Result<bool, string>.Ok(false)
            );

        Result<T, string> IInputProcessor<T>.getResult() => Result<T, string>.Ok(this.result);

        void IInputHandler.Reset() => (this.resetter)();
    }
}
