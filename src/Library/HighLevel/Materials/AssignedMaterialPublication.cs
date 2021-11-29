using Library.HighLevel.Companies;
using Library.Utils;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This struct represents a <see cref="MaterialPublication" /> which is detached from its <see cref="Company" />.
    /// </summary>
    public readonly struct AssignedMaterialPublication
    {
        /// <summary>
        /// The publication.
        /// </summary>
        public readonly MaterialPublication Publication { get; }

        /// <summary>
        /// The company which owns the publication.
        /// </summary>
        public readonly Company Company { get; }

        /// <summary>
        /// Initializes an instance of <see cref="AssignedMaterialPublication" />.
        /// </summary>
        /// <param name="publication">The publication.</param>
        /// <param name="company">The company which owns the publication.</param>
        public AssignedMaterialPublication(MaterialPublication publication, Company company)
        {
            this.Publication = publication;
            this.Company = company;
        }

        /// <inheritdoc />
        public override string ToString() =>
            $"(De {this.Company.Name}) {this.Publication}";
    }
}
