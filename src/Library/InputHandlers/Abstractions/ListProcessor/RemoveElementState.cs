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
                "Por favor, ingrese el índice del elemento que quiere remover."
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
                    if (index < 0) return "El índice es negativo.";
                    if (index >= this.parent.list.Count) return "El índice es más grande que el máximo permitido por la lista.";
                    this.parent.list.RemoveAt(index);
                    return null;
                }
                else
                {
                    return "Se esperaba un número.";
                }
            }
        }
    }
}
