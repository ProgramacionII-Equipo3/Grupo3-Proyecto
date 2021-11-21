namespace Library.Core.Processing
{
    /// <summary>
    /// Represent a complex form-like set of data through which certain types of data can be received from user input easily.
    /// </summary>
    /// <typeparam name="T">The type of the resulting object.</typeparam>
    public abstract class FormProcessor<T> : InputProcessor<T>
    {
        /// <summary>
        /// The list of input handlers.
        /// </summary>
        protected InputHandler[] inputHandlers = new InputHandler[0];
        private int index;

        private InputHandler CurrentHandler => this.inputHandlers[this.index];

        /// <inheritdoc />
        public override string GetDefaultResponse() => this.CurrentHandler.GetDefaultResponse();

        /// <inheritdoc />
        public override void Reset()
        {
            foreach (InputHandler handler in this.inputHandlers)
            {
                handler.Reset();
            }
            
            this.index = 0;
        }

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg)
        {
            Result<bool, string> processResult = this.CurrentHandler.ProcessInput(msg);
            return processResult.AndThen(
                ready =>
                {
                    if (!ready)
                    {
                        return Result<bool, string>.Ok(false);
                    }

                    this.index++;
                    if (this.index >= this.inputHandlers.Length)
                    {
                        return Result<bool, string>.Ok(true);
                    }
                    else
                    {
                        return Result<bool, string>.Err(this.CurrentHandler.GetDefaultResponse());
                    }
                });
        }
    }
}
