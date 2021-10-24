using System;
using Library.Core.Processing;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represents an <see cref="IInputHandler" /> which uses an input processor, and a function to be given its result.
    /// </summary>
    public class BasicInputHandler : IInputHandler
    {
        private Func<string, (bool, string)> inputHandler;
        private readonly Func<string> initialResponseGetter;
        private Action resetter;

        ///
        private BasicInputHandler(Func<string, (bool, string)> inputHandler, Func<string> initialResponseGetter, Action resetter)
        {
            this.inputHandler = inputHandler;
            this.initialResponseGetter = initialResponseGetter;
            this.resetter = resetter;
        }

        /// <summary>
        /// Creates a <see cref="BasicInputHandler" />.
        /// </summary>
        /// <param name="action">The operation to do with the resulting input.</param>
        /// <param name="processor">The input processor.</param>
        /// <typeparam name="T">The type of the object the input processor returns, which is used by the "action" operation.</typeparam>
        public static BasicInputHandler CreateInstance<T>(Action<T> action, IInputProcessor<T> processor) where T: class
        {
            return new BasicInputHandler (
                inputHandler: s =>
                {
                    var (result, response) = processor.ProcessInput(s);
                    if(result != null)
                    {
                        action(result);
                        return (true, null);
                    }
                    return (false, response);
                },
                initialResponseGetter: processor.GetDefaultResponse,
                resetter: processor.Reset
            );
        }

        string IInputHandler.GetDefaultResponse() => (this.initialResponseGetter)();

        (bool, string) IInputHandler.GetInput(string msg) => (this.inputHandler)(msg);

        void IInputHandler.Reset() => (this.resetter)();
    }
}
