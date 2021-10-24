using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Companies;

namespace Library.States
{
    public partial class IncompleteCompanyRepresentativeState
    {
        private class AssignExistingCompanyState : IInputProcessor<Company>
        {
            private Company company;

            public AssignExistingCompanyState(Company company)
            {
                this.company = company;
            }

            public string GetDefaultResponse() =>
                $"There's already a company called {this.company.Name}. Is this the company you want to assign to?";

            (Company, string) IInputProcessor<Company>.getResult() => (this.company, null);

            (bool, string) IInputProcessor<Company>.getInput(string msg)
            {
                msg = msg.Trim().ToLowerInvariant();
                if(msg == "yes" || msg == "y")
                    return (true, null);
                else if(msg == "no" || msg == "n")
                    return (false, null);
                
                return (default, "Please answer \"yes\" (\"y\") or \"no\" (\"n\").");
            }

            void IInputProcessor<Company>.Reset()
            {
            }
        }
    }
}
