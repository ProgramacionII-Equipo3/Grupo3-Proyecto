using System;
using System.Linq;
using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// This class acts as a standard implementation of a <see cref="FormProcessor{T}" />.
    /// </summary>
    /// <typeparam name="T">The type of the resulting object of the input handling.</typeparam>
    /// <typeparam name="U">The type of the object which holds memory of the state of the processor.</typeparam>
    public class BaseFormProcessor<T, U> : FormProcessor<T>
    {
        private readonly Func<U, Result<T, string>> conversionFunction;

        private Func<U> initialStateGetter;

        private U state;

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            this.state = (this.initialStateGetter)();
        }

        /// <summary>
        /// Initializes an instance of <see cref="BaseFormProcessor{T, U}" />.
        /// </summary>
        /// <param name="initialStateGetter">The function which determines the initial state.</param>
        /// <param name="conversionFunction">The function which generates the value.</param>
        /// <param name="inputHandlers">The array of input handlers.</param>
        public BaseFormProcessor(Func<U> initialStateGetter, Func<U, Result<T, string>> conversionFunction, params Func<Func<U>, InputProcessor<U>>[] inputHandlers)
        {
            this.initialStateGetter = initialStateGetter;
            this.state = initialStateGetter();
            this.conversionFunction = conversionFunction;
            this.inputHandlers = inputHandlers.Select(handlerGetter =>
                ProcessorHandler.CreateInfallibleInstance<U>(
                    newState => this.state = newState,
                    handlerGetter(() => this.state))).ToArray();
        }

        /// <inheritdoc />
        protected override Result<T, string> getResult() => (this.conversionFunction)(state);
    }
}
