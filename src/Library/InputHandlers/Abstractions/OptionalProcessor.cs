using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    public class OptionalProcessor<T> : InputProcessor<T?>
    {
        private InputProcessor<T> innerProcessor;
        private T? result;

        public OptionalProcessor(InputProcessor<T> innerProcessor)
        {
            this.innerProcessor = innerProcessor;
        }

        /// <inheritdoc />
        public override string GetDefaultResponse() =>
            $"{this.innerProcessor.GetDefaultResponse()}\nEscribe /esc para ingresar un valor nulo.";

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override void Reset()
        {
            this.innerProcessor.Reset();
        }

        /// <inheritdoc />
        protected override Result<T?, string> getResult() =>
            Result<T?, string>.Ok(this.result);
    }
}
