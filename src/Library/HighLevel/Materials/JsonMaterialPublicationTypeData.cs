using System;
using Library.Utils;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class holds the JSON information of a <see cref="MaterialPublicationTypeData" />.
    /// </summary>
    public class JsonMaterialPublicationTypeData : IJsonHolder<MaterialPublicationTypeData>
    {
        /// <summary>
        /// The date the material will be published if the publication is scheduled.
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// The type of the publication.
        /// </summary>
        public string PublicationType { get; set; } = string.Empty;

        /// <inheritdoc />
        public void FromValue(MaterialPublicationTypeData value)
        {
            switch(value.PublicationType)
            {
                case MaterialPublicationTypeData.MaterialPublicationType.NORMAL:
                    this.DateTime = null;
                    this.PublicationType = "NORMAL";
                    break;
                case MaterialPublicationTypeData.MaterialPublicationType.SCHEDULED:
                    this.DateTime = value.DateTime;
                    this.PublicationType = "SCHEDULED";
                    break;
                case MaterialPublicationTypeData.MaterialPublicationType.CONTINUOUS:
                    this.DateTime = null;
                    this.PublicationType = "CONTINUOUS";
                    break;
                default:
                    throw new Exception();
            }
        }

        /// <inheritdoc />
        public MaterialPublicationTypeData ToValue()
        {
            switch(this.PublicationType)
            {
                case "NORMAL":
                    return MaterialPublicationTypeData.Normal();
                case "SCHEDULED":
                    return MaterialPublicationTypeData.Scheduled(this.DateTime.Unwrap());
                case "CONTINUOUS":
                    return MaterialPublicationTypeData.Continuous();
                default:
                    throw new Exception("The material publication type must be either \"NORMAL\", \"SCHEDULED\", or \"CONTINUOUS\"");
            }
        }
    }
}
