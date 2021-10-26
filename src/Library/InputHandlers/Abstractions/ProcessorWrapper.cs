using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// Act as a type wrapper for a specific type of <see cref="IInputProcessor{T}" />
    /// </summary>
    /// <typeparam name="T">The type the inner processor returns.</typeparam>
    public abstract class ProcessorWrapper<T> : IInputProcessor<T> where T: class
    {
        private IInputProcessor<T> innerProcessor;
        private T result;

        ///
        protected ProcessorWrapper(IInputProcessor<T> innerProcessor)
        {
            this.innerProcessor = innerProcessor;
        }

        string IInputHandler.GetDefaultResponse() => this.innerProcessor.GetDefaultResponse();

        (bool, string) IInputHandler.ProcessInput(string msg)
        {
            if(this.result != null) return (true, null);

            var (result, response) = this.innerProcessor.GenerateFromInput(msg);
            if(response != null) return (default, response);
            if(result == null) return (false, null);
            this.result = result;
            return (true, null);
        }

        (T, string) IInputProcessor<T>.getResult() => (this.result, null);

        void IInputHandler.Reset()
        {
            this.result = null;
            this.innerProcessor.Reset();
        }
    }
}
