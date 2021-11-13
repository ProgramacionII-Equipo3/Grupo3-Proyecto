namespace Library.Core.Processing
{
    /// <summary>
    /// Represent a complex form-like set of data through which certain types of data can be received from user input easily.
    /// </summary>
    /// <typeparam name="T">The type of the resulting object.</typeparam>
    public abstract class FormProcessor<T> : IInputProcessor<T>
    {
        /// <summary>
        /// The list of input handlers.
        /// </summary>
        protected IInputHandler[] inputHandlers;
        private int index;

        private IInputHandler CurrentHandler => this.inputHandlers[this.index];

        /// <inheritdoc />
        public string GetDefaultResponse() => this.CurrentHandler.GetDefaultResponse();

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

        /// <summary>
        /// Generates the resulting object with the obtained input.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///     This function should be called only after a call to <see cref="IInputHandler.ProcessInput(string)" /> returns: <br />
        ///         Result&lt;bool, string&gt;.Ok(true) <br />
        ///     which is a signal that the object's ready to produce the result. Doing so under other circumstances may result in undefined behaviour.
        ///     </para>
        /// </remarks>
        /// <returns>
        /// Result.Ok(result), being result the resulting object, or<br />
        /// Result.Err(error), being error an error string.
        /// </returns>
        protected abstract Result<T, string> getResult();
    }
}
