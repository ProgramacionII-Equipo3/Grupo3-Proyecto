namespace Library.Core.Processing
{
    /// <summary>
    /// Represent a complex form-like set of data through which certain forms of data can be received from user input easily.
    /// </summary>
    /// <typeparam name="T">The type of the resulting object.</typeparam>
    public abstract class FormProcessor<T> : IInputProcessor<T>
    {
        /// <summary>
        /// The list of input handlers.
        /// </summary>
        protected IInputHandler[] inputHandlers;
        private int index = 0;

        private IInputHandler currentHandler => this.inputHandlers[this.index];


        /// <summary>
        /// Represents the functionality of handling one or more message input until realizing a certain operation successfully,
        /// or until the user indicates to stop trying.
        /// </summary>
        public string GetDefaultResponse() => this.currentHandler.GetDefaultResponse();

        /// <summary>
        /// Generates the resulting object with the obtained input.
        /// </summary>
        /// <returns>The resulting object.</returns>
        protected abstract Result<T, string> getResult();

        void IInputHandler.Reset()
        {
            foreach (IInputHandler handler in this.inputHandlers)
                handler.Reset();
            this.index = 0;
        }

        Result<T, string> IInputProcessor<T>.getResult() => this.getResult();

        /// <inheritdoc />
        Result<bool, string> IInputHandler.ProcessInput(string msg)
        {
            Result<bool, string> processResult = this.currentHandler.ProcessInput(msg);
            return processResult.Map(
                ready =>
                {
                    if (!ready) return Result<bool, string>.Ok(false);

                    this.index++;
                    if (this.index >= this.inputHandlers.Length) return Result<bool, string>.Ok(true);
                    else return Result<bool, string>.Err(this.currentHandler.GetDefaultResponse());
                },
                e => Result<bool, string>.Err(e)
            );
        }
    }
}
