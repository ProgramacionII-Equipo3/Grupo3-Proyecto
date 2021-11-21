using Library.Core;
using Library.Core.Processing;
using Library.HighLevel.Companies;

namespace Library.States.Companies
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

            Result<Company, string> IInputProcessor<Company>.getResult() => Result<Company, string>.Ok(this.company);

            Result<bool, string> IInputHandler.ProcessInput(string msg)
            {
                msg = msg.Trim().ToLowerInvariant();
                if(msg == "yes" || msg == "y")
                    return Result<bool, string>.Ok(true);
                else if(msg == "no" || msg == "n")
                    return Result<bool, string>.Ok(false);
                
                return Result<bool, string>.Err("Please answer \"yes\" (\"y\") or \"no\" (\"n\").");
            }

            void IInputHandler.Reset()
            {
            }
        }
    }
}
