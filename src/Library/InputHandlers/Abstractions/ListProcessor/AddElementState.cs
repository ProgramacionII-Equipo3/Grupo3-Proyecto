namespace Library.InputHandlers.Abstractions
{
    public partial class ListProcessor<T>
    {
        /// <summary>
        /// This class represents the state of adding a new element in the list,
        /// which is done via the inner processor.
        /// </summary>
        private class AddElementState : InnerProcessorState
        {
            public AddElementState(ListProcessor<T> parent) : base(parent)
            {
            }

            /// <inheritdoc />
            public override string GetDefaultResponse() => this.parent.processor.GetDefaultResponse();

            /// <inheritdoc />
            public override Result<bool, (ListProcessor<T>.InnerProcessorState, string?)> ProcessMessage(string msg)
            {
                if (this.parent.processor.GenerateFromInput(msg) is Result<T, string> result)
                {
                    return result.Map(
                        value =>
                        {
                            this.parent.list.Add(value);
                            return Result<bool, (ListProcessor<T>.InnerProcessorState, string?)>.Err((
                                new InitialMenuState(this.parent), null
                            ));
                        },
                        e => Result<bool, (ListProcessor<T>.InnerProcessorState, string?)>.Err((this, e))
                    );
                }
                else
                {
                    return Result<bool, (ListProcessor<T>.InnerProcessorState, string?)>.Err((
                        new InitialMenuState(this.parent), null
                    ));
                }
            }
        }
    }
}