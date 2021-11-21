namespace Library.InputHandlers.Abstractions
{
    public partial class ListProcessor<T>
    {
        /// <summary>
        /// This class represents the inner state of the list processor.
        /// This class is based on the Polymorphism principle, through which
        /// the concrete subtype of this class to which the list's state belongs determines its behaviour.
        /// </summary>
        private abstract class InnerProcessorState
        {
            protected ListProcessor<T> parent;
            public InnerProcessorState(ListProcessor<T> parent)
            {
                this.parent = parent;
            }

            /// <summary>
            /// Processes a message.
            /// </summary>
            /// <param name="msg">The message to process.</param>
            /// <returns>
            /// Result.Err((newState, msg)), being newState the new state of the list and msg an optional response message (if it's null, the new state's default response is used in its place),<br />
            /// Result.Ok(true) for a success signal, or <br />
            /// Result.Ok(false) for an interrupt signal.
            /// </returns>
            public abstract Result<bool, (InnerProcessorState, string?)> ProcessMessage(string msg);

            /// <summary>
            /// Gets the <see cref="InnerProcessorState" />'s default response.
            /// </summary>
            /// <returns>A string.</returns>
            public abstract string GetDefaultResponse();
        }
    }
}
