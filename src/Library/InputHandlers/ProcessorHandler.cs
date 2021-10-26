using System;
using Library.Core.Processing;

namespace Library.InputHandlers
{
    /// <summary>
    /// Represents an <see cref="IInputHandler" /> which uses an input processor, and a function to be given its result.
    /// </summary>
    public class ProcessorHandler : IInputHandler
    {
        private Func<string, Result<bool, string>> inputHandler;
        private readonly Func<string> initialResponseGetter;
        private Action resetter;

        ///
        private ProcessorHandler(Func<string, Result<bool, string>> inputHandler, Func<string> initialResponseGetter, Action resetter)
        {
            this.inputHandler = inputHandler;
            this.initialResponseGetter = initialResponseGetter;
            this.resetter = resetter;
        }

        /// <summary>
        /// Creates an instance of <see cref="ProcessorHandler" />.
        /// </summary>
        /// <param name="action">The operation to do with the resulting input.</param>
        /// <param name="processor">The input processor.</param>
        /// <typeparam name="T">The type of the object the input processor returns, which is used by the "action" operation.</typeparam>
        public static ProcessorHandler CreateInstance<T>(Action<T> action, IInputProcessor<T> processor)
        {
            return new ProcessorHandler (
                inputHandler: s =>
                {
                    if(processor.GenerateFromInput(s) is Result<T, string> result)
                    {
                        return result.SwitchOk(
                            v =>
                            {
                                action(v);
                                return true;
                            }
                        );
                    } else
                    {
                        return Result<bool, string>.Ok(false);
                    }
                },
                initialResponseGetter: processor.GetDefaultResponse,
                resetter: processor.Reset
            );
        }

        string IInputHandler.GetDefaultResponse() => (this.initialResponseGetter)();

        Result<bool, string> IInputHandler.ProcessInput(string msg) => (this.inputHandler)(msg);

        void IInputHandler.Reset() => (this.resetter)();
    }
}
