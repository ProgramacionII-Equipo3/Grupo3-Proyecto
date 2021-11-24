namespace Library.InputHandlers.Abstractions
{
    public partial class ListProcessor<T>
    {
        /// <summary>
        /// This class represents the state of the initial menu.
        /// </summary>
        private class InitialMenuState : InnerProcessorState
        {
            public InitialMenuState(ListProcessor<T> parent) : base(parent)
            {
            }

            /// <inheritdoc />
            public override string GetDefaultResponse() =>
                (this.parent.initialResponseGetter)()
                  + "\n        /add: Añadir un elemento"
                  + "\n        /remove: Remover un elemento"
                  + "\n        /finish: Terminar"
                  + "\n        /back: Ir atrás";

            /// <inheritdoc />
            public override Result<bool, (ListProcessor<T>.InnerProcessorState, string?)> ProcessMessage(string msg)
            {
                switch (msg.Trim())
                {
                    case "/add":
                        this.parent.processor.Reset();
                        return Result<bool, (ListProcessor<T>.InnerProcessorState, string?)>.Err((
                            new AddElementState(this.parent), null));
                    case "/remove":
                        return Result<bool, (ListProcessor<T>.InnerProcessorState, string?)>.Err((
                            new RemoveElementState(this.parent), null));
                    case "/finish":
                        return Result<bool, (ListProcessor<T>.InnerProcessorState, string?)>.Ok(true);
                    case "/back":
                        return Result<bool, (ListProcessor<T>.InnerProcessorState, string?)>.Ok(false);
                    default:
                        return Result<bool, (ListProcessor<T>.InnerProcessorState, string?)>.Err((this, null));
                }
            }
        }
    }
}
