using Library.HighLevel.Companies;

namespace Library.States
{
    public partial class IncompleteCompanyRepresentativeState
    {
        private abstract class MiddleState
        {
            protected readonly IncompleteCompanyRepresentativeState parent;

            public MiddleState(IncompleteCompanyRepresentativeState parent)
            {
                this.parent = parent;
            }

            /// <summary>
            /// Processes a message, returning a company, a response string, or an interrupt signal.
            /// </summary>
            /// <param name="msg">The message's text.</param>
            /// <returns>
            /// (company, null), being company the resulting company,<br/>
            /// (null, response), being response the response string, or<br/>
            /// (null, null) for an interrupt signal.
            /// </returns>
            public abstract (Company, string) processMessage(string msg);
        }
    }
}
