using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// Act as a type wrapper for a specific type of <see cref="IInputProcessor{T}" />
    /// </summary>
    /// <typeparam name="T">The type the inner processor returns.</typeparam>
    public abstract class ProcessorWrapper<T> : IInputProcessor<T>
    {
        private IInputProcessor<T> innerProcessor;
        private T result = default;

        ///
        protected ProcessorWrapper(IInputProcessor<T> innerProcessor)
        {
            this.innerProcessor = innerProcessor;
        }

        string IInputHandler.GetDefaultResponse() => this.innerProcessor.GetDefaultResponse();

        Result<bool, string> IInputHandler.ProcessInput(string msg)
        {
            if(this.result != null) return Result<bool, string>.Ok(true);
            if(this.innerProcessor.GenerateFromInput(msg) is Result<T, string> result)
            {
                return result.SwitchOk(
                    v => 
                    {
                        this.result = v;
                        return true;
                    }
                );
            } else
            {
                return Result<bool, string>.Ok(false);
            }
        }

        Result<T, string> IInputProcessor<T>.getResult() => Result<T, string>.Ok(this.result);

        void IInputHandler.Reset()
        {
            this.result = default;
            this.innerProcessor.Reset();
        }
    }
}
