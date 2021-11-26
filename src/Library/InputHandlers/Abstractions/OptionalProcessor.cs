using System;
using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// This class represents a processor which can generate a null value.
    /// </summary>
    /// <typeparam name="T">The type of which an object will be generated, if it isn't null.</typeparam>
    public class OptionalProcessor<T> : InputProcessor<T?> where T : class
    {
        private InputProcessor<T> innerProcessor;
        private T? result = null;

        /// <summary>
        /// Initializes an instance of <see cref="OptionalProcessor{T}" />.
        /// </summary>
        /// <param name="innerProcessor">The processor which generates the value.</param>
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
            if(msg == "/esc")
            {
                this.result = null;
                return Result<bool, string>.Ok(true);
            }

            if (this.innerProcessor.GenerateFromInput(msg) is Result<T, string> result)
            {
                return result.SwitchOk(v =>
                {
                    this.result = v;
                    return true;
                });
            }
            else
            {
                return Result<bool, string>.Ok(false);
            }
        }

        /// <inheritdoc />
        public override void Reset()
        {
            this.result = null;
            this.innerProcessor.Reset();
        }

        /// <inheritdoc />
        protected override Result<T?, string> getResult() =>
            Result<T?, string>.Ok(this.result);
    }
}
