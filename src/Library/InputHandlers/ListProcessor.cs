using System;
using System.Collections.Generic;
using Library.Core.Processing;

namespace Library.InputHandlers
{
    /// <summary>
    /// This class represents a processor which processes several objects of the same type.
    /// </summary>
    /// <typeparam name="T">The type of the elements which are processed into a list.</typeparam>
    public class ListProcessor<T> : IInputProcessor<T[]>
    {
        private Func<string, bool> escapePredicate;
        private List<T> list = new List<T>();

        private Func<string> responseGetterForNextObjects;

        private IInputProcessor<T> processor;

        ///
        public ListProcessor(IInputProcessor<T> processor, Func<string, bool> escapePredicate, Func<string> responseGetterForNextObjects)
        {
            this.processor = processor;
            this.escapePredicate = escapePredicate;
            this.responseGetterForNextObjects = responseGetterForNextObjects;
        }

        Result<bool, string> IInputHandler.ProcessInput(string msg)
        {
            if ((this.escapePredicate)(msg)) return Result<bool, string>.Ok(true);
            return this.processor.GenerateFromInput(msg).Map(
                result => result.Map(
                    value =>
                    {
                        this.list.Add(value);
                        this.processor.Reset();
                        return Result<bool, string>.Err(this.GetDefaultResponse());
                    },
                    s => Result<bool, string>.Err(s)),
                () => Result<bool, string>.Ok(false));
        }

        Result<T[], string> IInputProcessor<T[]>.getResult() => Result<T[], string>.Ok(this.list.ToArray());

        /// <inheritdoc />
        public string GetDefaultResponse() =>
            this.list.Count == 0 ? this.processor.GetDefaultResponse() : (this.responseGetterForNextObjects)();

        void IInputHandler.Reset()
        {
            this.list.Clear();
            this.processor.Reset();
        }
    }
}
