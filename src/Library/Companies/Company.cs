using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Library.Core;

namespace Library.Companies
{
    public class Company : IPublisher<uint>, ISentMaterialReportCreator
    {

        public string Name { get; private set; }

        public ContactInfo contactInfo { get; private set; }

        public string Heading { get; private set; }

        private List<UserId> representants = new List<UserId>();

        public bool HasUser(UserId id) =>
            this.representants.Any(repId => repId == id);

        protected List<MaterialPublication> publications = new List<MaterialPublication>();

        public ReadOnlyCollection<MaterialPublication> Publications => this.publications.AsReadOnly();

        public bool PublishMaterial(Material material, Amount amount, Price price, Location location);

        public bool RemovePublication(TId id);
    }
}
