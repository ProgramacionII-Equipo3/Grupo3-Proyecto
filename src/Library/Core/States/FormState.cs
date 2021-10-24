namespace Library.Core.States
{
    /// <summary>
    /// Represent a complex form-like set of data through which certain forms of data can be received from user input easily.
    /// </summary>
    public abstract class FormState : State
    {
        /// <summary>
        /// The list of input handlers.
        /// </summary>
        private readonly IFromInput[] inputHandlers;
        private int index = 0;

        ///
        public FormState(IFromInput[] inputHandlers)
        {
            this.inputHandlers = inputHandlers;
        }

        /// <summary>
        /// Gathers the input obtained after several messages, realizes an operation with them, and moves on to the next state.
        /// </summary>
        /// <returns>The user's next state and the response string.</returns>
        public abstract (State, string) NextState();
        
        /// <inheritdoc />
        public override (State, string) ProcessMessage(UserId id, UserData data, string msg)
        {
            IFromInput handler = this.inputHandlers[this.index];
            if(handler.GetInput(msg) is string error) return (this, error);
            this.index += 1;
            if(this.index >= this.inputHandlers.Length) return this.NextState();
            else return (this, handler.GetDefaultString());
        }
    }
}
