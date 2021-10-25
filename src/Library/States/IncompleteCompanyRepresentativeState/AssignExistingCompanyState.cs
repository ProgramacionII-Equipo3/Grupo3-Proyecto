using Library.HighLevel.Companies;

namespace Library.States
{
    public partial class IncompleteCompanyRepresentativeState
    {
        private class AssignExistingCompanyState : MiddleState
        {
            private Company company;

            public AssignExistingCompanyState(IncompleteCompanyRepresentativeState parent, Company company): base(parent)
            {
                this.company = company;
            }

            public override (Company, string) processMessage(string msg)
            {
                msg = msg.Trim().ToLowerInvariant();
                if(msg == "yes" || msg == "y")
                    return (company, null);
                else if(msg == "no" || msg == "n")
                    return (null, null);
                
                return (null, "Please answer \"yes\" (\"y\") or \"no\" (\"n\").");
            }
        }
    }
}
