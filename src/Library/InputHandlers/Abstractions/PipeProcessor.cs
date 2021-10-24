using System;
using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// This class represents an input processor which takes the result of another one and applies a transformation to it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PipeProcessor<T> : IInputProcessor<T> where T: class
    {
        private readonly Func<string> initialResponseGetter;
        private Func<string, (T, string)> inputHandler;
        private Action resetter;
        private T result = null;

        private PipeProcessor(Func<string> initialResponseGetter, Func<string, (T, string)> inputHandler, Action resetter)
        {
            this.initialResponseGetter = initialResponseGetter;
            this.inputHandler = inputHandler;
            this.resetter = resetter;
        }

        (bool, string) IInputProcessor<T>.getInput(string msg)
        {
            var (result, response) = (this.inputHandler)(msg);
            if(response != null) return (default, response);
            if(result == null) return (false, null);
            this.result = result;
            return (true, null);
        }

        (T, string) IInputProcessor<T>.getResult() => (this.result, null);

        string IInputProcessor<T>.GetDefaultResponse() => (this.initialResponseGetter)();

        void IInputProcessor<T>.Reset()
        {
            this.result = null;
            (this.resetter)();
        }

        /// <summary>
        /// Creates a pipe processor.
        /// </summary>
        /// <param name="func">The transformation function.</param>
        /// <param name="processor">The inner <see cref="IInputProcessor{T}" />.</param>
        /// <typeparam name="U">The type of the objects the inner <see cref="IInputProcessor{T}" /> returns.</typeparam>
        public static PipeProcessor<T> CreateInstance<U>(Func<U, (T, string)> func, IInputProcessor<U> processor) where U: class
        {
            return new PipeProcessor<T>(
                initialResponseGetter: processor.GetDefaultResponse,
                inputHandler: s =>
                {
                    var (midResult, response) = processor.ProcessInput(s);
                    if(response != null) return (default, response);
                    if(midResult == null) return (null, null);
                    var (result, response2) = func(midResult);
                    if(response2 != null)
                    {
                        processor.Reset();
                        return (default, $"{response2}\n{processor.GetDefaultResponse()}");
                    }
                    if(result == null) return (null, null);
                    return (result, null);
                },
                resetter: processor.Reset
            );
        }
    }
}