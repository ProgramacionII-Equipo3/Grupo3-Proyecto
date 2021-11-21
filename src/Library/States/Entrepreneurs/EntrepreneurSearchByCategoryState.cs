using Library;
using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Entrepreneurs;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using System.Linq;
using Library.HighLevel.Materials;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class represents the state of an entrepreneur that is searching publications with a keyword.
    /// </summary>
    public class EntrepreneurSearchByCategoryState : InputHandlerState
    {
        /// <inheritdoc />
        public override bool IsComplete => true;

        /// <inheritdoc />
        public override State.Type UserType => State.Type.ENTREPRENEUR;

        private MaterialCategory category;
        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurSearchByKeywordState" />.
        /// </summary>
        public EntrepreneurSearchByCategoryState(): base(
            inputHandler: ProcessorHandler.CreateInstance<string>(
                category => Singleton<Searcher>.Instance.SearchByCategory(category).Any()
                    ? null
                    : "There's no publication with that category",
                new BasicStringProcessor(() => "Please insert the category you want to search.")
            ),
            exitState: () => new EntrepreneurMenuState(),
            nextState: () => new EntrepreneurMenuState()
        ) {}
    }
}