using System.Collections.Generic;
using System.Linq;
using Library;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Materials;
using Library.HighLevel.Entrepreneurs;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class represents the state of an entrepreneur that is searching publications with a keyword.
    /// </summary>
    public class EntrepreneurSearchByKeywordState : InputHandlerState
    {
        /// <inheritdoc />
        public override bool IsComplete => true;

        /// <inheritdoc />
        public override State.Type UserType => State.Type.ENTREPRENEUR;

        private IList<MaterialPublication> publications;

        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurSearchByKeywordState" />.
        /// </summary>
        public EntrepreneurSearchByKeywordState(): base(
            inputHandler: ProcessorHandler.CreateInstance<string>(
                keyword => 
                Singleton<Searcher>.Instance.SearchByKeyword(keyword).Any()
                    ? null
                    : "There's no publication with that keyword.",
                new BasicStringProcessor(() => "Please insert the keyword you want to search.")
            ),
            exitState: () => new EntrepreneurMenuState(),
            nextState: () => new EntrepreneurMenuState()
        ) {}
    }
}