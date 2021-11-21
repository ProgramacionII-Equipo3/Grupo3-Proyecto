using System;
using Library.Core;
using Library.Core.Processing;
using Library.Utils;

namespace Library.States
{
    /// <summary>
    /// This class represents a State which basically works with an <see cref="InputProcessor{T}" />.
    /// </summary>
    public class InputProcessorState : State
    {
        private Func<string, (State?, string?)>? nextStateGetter;
        private Func<string>? initialResponseGetter;

        public InputProcessorState()
        {
        }

        public static InputProcessorState CreateInstance<T>(InputProcessor<T> processor, Func<T, (State?, string?)> nextState, Func<(State?, string?)> exitState)
        {
            InputProcessorState r = new InputProcessorState();
            r.nextStateGetter =
                msg => processor.GenerateFromInput(msg) is Result<T, string> result
                    ? result.Map(
                        v => nextState(v),
                        e => (r, e)
                    ) : exitState();
            r.initialResponseGetter = processor.GetDefaultResponse;
            return r;
        }

        /// <inheritdoc />
        public override (State?, string?) ProcessMessage(string id, ref UserData data, string msg) =>
            (this.nextStateGetter.Unwrap())(msg);

        /// <inheritdoc />
        public override string GetDefaultResponse() =>
            (this.initialResponseGetter.Unwrap())();
    }
}
