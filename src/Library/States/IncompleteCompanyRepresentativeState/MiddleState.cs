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

            public abstract (Company, string) processMessage(string msg);
        }
    }
}
