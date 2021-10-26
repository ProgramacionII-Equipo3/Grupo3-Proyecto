using System;
using System.Globalization;
using Library.InputHandlers;
using Library.HighLevel.Companies;
using Library.Core.Processing;
using BoxedInt = Library.RefWrapper<int>;

namespace Library.States
{
    public partial class IncompleteCompanyRepresentativeState
    {
        private class CreateNewCompanyState : FormProcessor<Company>
        {

            private IncompleteCompanyRepresentativeState parent;
            private string heading;
            private string location;
            private int phoneNumber;
            private string email;

            private Company result = null;

            public CreateNewCompanyState(IncompleteCompanyRepresentativeState parent)
            {
                this.parent = parent;
                this.inputHandlers = new IInputHandler[]
                {
                    ProcessorHandler.CreateInstance<string>(
                        s => this.heading = s,
                        new BasicStringProcessor(() => "Please insert the company's heading.")
                    ),
                    ProcessorHandler.CreateInstance<string>(
                        s => this.location = s,
                        new BasicStringProcessor(() => "Please insert the company's location.")
                    ),
                    ProcessorHandler.CreateInstance<BoxedInt>(
                        n => this.phoneNumber = n.value,
                        new UnsignedInt32Processor(() => "Please insert the company's phone number.")
                    ),
                    ProcessorHandler.CreateInstance<string>(
                        s => this.email = s,
                        new EmailProcessor(() => "Please insert the company's email.")
                    )
                };
            }

            protected override (Company, string) getResult()
            {
                Company result = CompanyManager.CreateCompany(
                    name: this.parent.name,
                    contactInfo: new Library.Core.ContactInfo
                    {
                        Email = this.email,
                        PhoneNumber = this.phoneNumber
                    },
                    heading: this.heading
                );
                if(result == null) return (null, "There's already a company with the same name.");
                else return (result, null);
            }
        }
    }
}
