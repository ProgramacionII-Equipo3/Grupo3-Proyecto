using System.Linq;

namespace Library.InputHandlers.Abstractions
{
    public partial class ListProcessor<T>
    {
        /// <summary>
        /// This class represents the state of asking for an index to remove an element from the list.
        /// </summary>
        private class RemoveElementState : InnerProcessorState
        {
            public RemoveElementState(ListProcessor<T> parent) : base(parent)
            {
            }

            /// <inheritdoc />
            public override string GetDefaultResponse() =>
                "Please insert the index of the element you want to remove."
                + string.Join(null, this.parent.list.Select((el, i) => $"\n        {i}: {el}"));

            /// <inheritdoc />
            public override Result<bool, (ListProcessor<T>.InnerProcessorState, string?)> ProcessMessage(string msg) =>
                Result<bool, (ListProcessor<T>.InnerProcessorState, string?)>.Err((
                    new InitialMenuState(this.parent), processMessage(msg)));

            private string? processMessage(string msg)
            {
                int index;
                if (int.TryParse(msg.Trim(), out index))
                {
                    if (index < 0) return "The given index is negative.";
                    if (index >= this.parent.list.Count) return "The given index is higher than the length of the list.";
                    this.parent.list.RemoveAt(index);
                    return null;
                }
                else
                {
                    return "A number was expected.";
                }
            }
        }
    }
}
