using System;
using Library.Core;
using Library.States.Entrepreneurs;

namespace Library.States.Entrepreneurs
{
    /// <summary>
    /// This class represents the <see cref="Library.Core.State" /> of an entrepreneur in the initial menu.
    /// </summary>
    public class EntrepreneurMenuState : MultipleOptionState
    {
        /// <inheritdoc />
        public override bool IsComplete => true;

        /// <inheritdoc />
        public override State.Type UserType => State.Type.ENTREPRENEUR;

        private string userId;

        private string? initialResponse;

        private string keyword;
        /// <summary>
        /// Initializes an instance of <see cref="EntrepreneurMenuState" />
        /// </summary>
        public EntrepreneurMenuState(string? initialResponse = null)
        {
            this.initialResponse = initialResponse;
            this.commands = new (string, string, Func<(State, string)>)[]
            {
                ("/eregister", "Registers a Entrepreneur into the platform", this.registerEntrepreneur),
                ("/esearchFK", "Search a material with a keyword", this.searchByKeyword),
                ("/esearchFC", "Search a material by its category", this.searchByCategory),
                ("/esearchFZ", "Search a material by its zone", this.searchByZone)
            };
        }

        private (State, string) registerEntrepreneur()
        {
            return (new NewEntrepreneurState(userId), null);
        }

        private (State, string) searchByKeyword()
        {
            return (new EntrepreneurSearchByKeywordState(), null);
        }

        private (State, string) searchByCategory()
        {
            return (new EntrepreneurSearchByCategoryState(), null);
        }

        private (State, string) searchByZone()
        {
            return (new EntrepreneurSearchByZoneState(), null);
        }

        /// <inheritdoc />
        protected override string GetInitialResponse()
        {
            if(initialResponse is null) return "What do you want to do?";
            string response = initialResponse;
            initialResponse = null;
            return $"{response}\nWhat do you want to do?";
        }

        /// <inheritdoc />
        protected override string GetErrorString() =>
            "Invalid option.";
    }
}