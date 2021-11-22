using Library.Core.Processing;
using Library.HighLevel.Companies;
using Library.InputHandlers;
using Library.InputHandlers.Abstractions;
using Library.Utils;
using Ucu.Poo.Locations.Client;

namespace Library.States.Companies
{
    public partial class IncompleteCompanyRepresentativeState
    {
        private class CreateNewCompanyState : FormProcessor<Company>
        {
            private IncompleteCompanyRepresentativeState parent;
            private string? heading;
            private Location? location;
            private int? phoneNumber;
            private string? email;
            private Company? result = null;

            /// <summary>
            /// Initializes an instance of <see cref="CreateNewCompanyState" />.
            /// </summary>
            /// <param name="parent">Its parent, from which it gets the company's name.</param>
            public CreateNewCompanyState(IncompleteCompanyRepresentativeState parent)
            {
                this.parent = parent;
                this.inputHandlers = new InputHandler[]
                {
                    ProcessorHandler.CreateInfallibleInstance<string>(
                        s => this.heading = s,
                        new BasicStringProcessor(() => "Por favor ingresa el rubro de la compañía.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<Location>(
                        l => this.location = l,
                        new LocationProcessor(() => "Por favor ingresa la ubicación de la compañía.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<int>(
                        n => this.phoneNumber = n,
                        new UnsignedInt32Processor(() => "Por favor ingresa el teléfono de la compañía.")
                    ),
                    ProcessorHandler.CreateInfallibleInstance<string>(
                        s => this.email = s,
                        new EmailProcessor(() => "Por favor ingresa el email de la compañía.")
                    )
                };
            }

            protected override Result<Company, string> getResult()
            {
                Company? result = Singleton<CompanyManager>.Instance.CreateCompany(
                    name: this.parent.name.Unwrap(),
                    contactInfo: new Library.Core.ContactInfo
                    {
                        Email = this.email.Unwrap(),
                        PhoneNumber = this.phoneNumber.Unwrap()
                    },
                    heading: this.heading.Unwrap(),
                    location: this.location.Unwrap());
                if (result == null) 
                {
                    return Result<Company, string>.Err("Ya existe una compañía con ese nombre.");
                }

                else
                {
                    return Result<Company, string>.Ok(result);
                }
            }
        }
    }
}
