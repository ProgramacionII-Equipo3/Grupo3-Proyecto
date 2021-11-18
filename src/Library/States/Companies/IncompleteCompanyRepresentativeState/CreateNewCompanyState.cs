using Library.Core.Processing;
using Library.HighLevel.Companies;
using Library.InputHandlers;
using Ucu.Poo.Locations.Client;

namespace Library.States.Companies
{
    public partial class IncompleteCompanyRepresentativeState
    {
        private class CreateNewCompanyState : FormProcessor<Company>
        {
            private IncompleteCompanyRepresentativeState parent;
            private string heading;
            private Location location;
            private int phoneNumber;
            private string email;
            private Company result = null;

            public CreateNewCompanyState(IncompleteCompanyRepresentativeState parent)
            {
                this.parent = parent;
                this.inputHandlers = new IInputHandler[]
                {
                    ProcessorHandler.CreateInfallibleInstance<string>(
                        s => this.heading = s,
                        new BasicStringProcessor(() => "Please insert the company's heading.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<Location>(
                        l => this.location = l,
                        new LocationProcessor(() => "Please insert the company's location")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<int>(
                        n => this.phoneNumber = n,
                        new UnsignedInt32Processor(() => "Please insert the company's phone number.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<string>(
                        s => this.email = s,
                        new EmailProcessor(() => "Please insert the company's email.")
                    )
                };
            }

            protected override Result<Company, string> getResult()
            {
                Company result = Singleton<CompanyManager>.Instance.CreateCompany(
                    name: this.parent.name,
                    contactInfo: new Library.Core.ContactInfo
                    {
                        Email = this.email,
                        PhoneNumber = this.phoneNumber
                    },
                    heading: this.heading,
                    location: this.location);
                if (result == null) 
                {
                    return Result<Company, string>.Err("There's already a company with the same name.");
                }

                else
                {
                    return Result<Company, string>.Ok(result);
                }
            }
        }
    }
}
