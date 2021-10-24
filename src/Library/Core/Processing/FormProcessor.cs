namespace Library.Core.Processing
{
    /// <summary>
    /// Represent a complex form-like set of data through which certain forms of data can be received from user input easily.
    /// </summary>
    public abstract class FormProcessor<T> : IInputProcessor<T> where T: class
    {
        /// <summary>
        /// The list of input handlers.
        /// </summary>
        protected IInputHandler[] inputHandlers;
        private int index = 0;
        private bool done = false;

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
        protected abstract (T, string) getResult();

        void IInputProcessor<T>.Reset()
        {
            foreach(IInputHandler handler in this.inputHandlers)
                handler.Reset();
            this.index = 0;
        }

        (T, string) IInputProcessor<T>.getResult() => this.getResult();
        
        /// <inheritdoc />
        (bool, string) IInputProcessor<T>.getInput(string msg)
        {
            var (ready, response) = this.currentHandler.GetInput(msg);
            if(response != null) return (default, response);

            if(!ready) return (false, null);

            this.index++;
            if(this.index >= this.inputHandlers.Length) return (true, null);
            else return (default, this.currentHandler.GetDefaultResponse());
        }
    }
}
