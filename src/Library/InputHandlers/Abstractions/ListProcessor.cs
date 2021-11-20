using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Core.Processing;

namespace Library.InputHandlers.Abstractions
{
    /// <summary>
    /// This class represents a processor which processes several objects of the same type.
    /// </summary>
    /// <typeparam name="T">The type of the elements which are processed into a list.</typeparam>
    public partial class ListProcessor<T> : InputProcessor<T[]>
    {
        private List<T> list = new List<T>();

        private Func<string> initialResponseGetter;

        private ProcessorState state;

        private enum ProcessorState
        {
            INITIAL_MENU,
            REMOVE_ELEMENT,
            ADD_ELEMENT
        }

        private InputProcessor<T> processor;

        /// <summary>
        /// Initializes an instance of <see cref="ListProcessor{T}" />.
        /// </summary>
        /// <param name="initialResponseGetter">The default response for the initial menu.</param>
        /// <param name="processor">The processor which generates the elements of the list.</param>
        public ListProcessor(Func<string> initialResponseGetter, InputProcessor<T> processor)
        {
            this.state = ProcessorState.INITIAL_MENU;
            this.initialResponseGetter = initialResponseGetter;
            this.processor = processor;
        }

        /// <inheritdoc />
        public override Result<bool, string> ProcessInput(string msg)
        {
            switch(this.state)
            {
                case ProcessorState.INITIAL_MENU:
                    switch(msg.Trim())
                    {
                        case "/add":
                            this.state = ProcessorState.ADD_ELEMENT;
                            this.processor.Reset();
                            return Result<bool, string>.Err(this.GetDefaultResponse());
                        case "/remove":
                            this.state = ProcessorState.REMOVE_ELEMENT;
                            return Result<bool, string>.Err(this.GetDefaultResponse());
                        case "/finish":
                            return Result<bool, string>.Ok(true);
                        case "/back":
                            return Result<bool, string>.Ok(false);
                        default:
                            return Result<bool, string>.Err(this.GetDefaultResponse());
                    }
                case ProcessorState.ADD_ELEMENT:
                    if(this.processor.GenerateFromInput(msg) is Result<T, string> result)
                    {
                        return result.AndThen(
                            value =>
                            {
                                this.list.Add(value);
                                this.state = ProcessorState.INITIAL_MENU;
                                return Result<bool, string>.Err(this.GetDefaultResponse());
                            }
                        );
                    } else
                    {
                        this.state = ProcessorState.INITIAL_MENU;
                        return Result<bool, string>.Err(this.GetDefaultResponse());
                    }
                case ProcessorState.REMOVE_ELEMENT:
                    this.state = ProcessorState.INITIAL_MENU;
                    int index;
                    if(int.TryParse(msg.Trim(), out index))
                    {
                        if(index < 0) return Result<bool, string>.Err($"The given index is negative.\n{this.GetDefaultResponse()}");
                        if(index >= this.list.Count) return Result<bool, string>.Err($"The given index is higher than the length of the list.\n{this.GetDefaultResponse()}");
                        list.RemoveAt(index);
                        return Result<bool, string>.Err(this.GetDefaultResponse());
                    } else
                    {
                        return Result<bool, string>.Err($"A number was expected.\n{this.GetDefaultResponse()}");
                    }
                default: throw new Exception();
            }
        }

        /// <inheritdoc />
        protected override Result<T[], string> getResult() => Result<T[], string>.Ok(this.list.ToArray());

        /// <inheritdoc />
        public override string GetDefaultResponse()
        {
            switch(this.state)
            {
                case ProcessorState.INITIAL_MENU:
                    return (this.initialResponseGetter)()
                              + "\n        /add: Add an element"
                              + "\n        /remove: Remove an element"
                              + "\n        /finish: Finish"
                              + "\n        /back: Go back";
                case ProcessorState.ADD_ELEMENT:
                    return this.processor.GetDefaultResponse();
                case ProcessorState.REMOVE_ELEMENT:
                    return "Please insert the index of the element you want to remove." + string.Join(null, this.list.Select((el, i) => $"\n        {i}: {el}"));
                default: throw new Exception();
            }
        }

        /// <inheritdoc />
        public override void Reset()
        {
            this.list.Clear();
            this.processor.Reset();
        }
    }
}
