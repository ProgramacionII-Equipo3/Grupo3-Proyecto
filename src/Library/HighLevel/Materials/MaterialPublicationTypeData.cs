using System;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This struct represents the three possible states in which a <see cref="MaterialPublication" /> can be.
    /// </summary>
    public readonly struct MaterialPublicationTypeData
    {
        private readonly DateTime dateTime;

        /// <summary>
        /// The date the material will be published if the publication is scheduled.
        /// </summary>
        public DateTime? DateTime => this.PublicationType == MaterialPublicationType.SCHEDULED ? this.dateTime : null;

        /// <summary>
        /// The type of the publication.
        /// </summary>
        public readonly MaterialPublicationType PublicationType;

        /// <summary>
        /// This enum represents the three possible states in which a <see cref="MaterialPublication" /> can be.
        /// </summary>
        public enum MaterialPublicationType
        {
            /// <summary>
            /// Represents a normal material publication.
            /// </summary>
            NORMAL,

            /// <summary>
            /// Represents a material publication which has a specific schedule.
            /// </summary>
            SCHEDULED,

            /// <summary>
            /// Represents a material publication whose material is constantly generated.
            /// </summary>
            CONTINUOUS
        }

        private MaterialPublicationTypeData(MaterialPublicationType publicationType, DateTime dateTime)
        {
            this.PublicationType = publicationType;
            this.dateTime = dateTime;
        }

        /// <summary>
        /// Creates the <see cref="MaterialPublicationTypeData" /> for a normal <see cref="MaterialPublication" />.
        /// </summary>
        /// <returns>The <see cref="MaterialPublicationTypeData" />.</returns>
        public static MaterialPublicationTypeData Normal() => new MaterialPublicationTypeData(MaterialPublicationType.NORMAL, default);

        /// <summary>
        /// Creates the <see cref="MaterialPublicationTypeData" /> for a scheduled <see cref="MaterialPublication" />.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime" /> in which the material will be published.</param>
        /// <returns>The <see cref="MaterialPublicationTypeData" />.</returns>
        public static MaterialPublicationTypeData Scheduled(DateTime dateTime) => new MaterialPublicationTypeData(MaterialPublicationType.SCHEDULED, dateTime);

        /// <summary>
        /// Creates the <see cref="MaterialPublicationTypeData" /> for a continuous <see cref="MaterialPublication" />.
        /// </summary>
        /// <returns>The <see cref="MaterialPublicationTypeData" />.</returns>
        public static MaterialPublicationTypeData Continuous() => new MaterialPublicationTypeData(MaterialPublicationType.CONTINUOUS, default);
        
    }
}
