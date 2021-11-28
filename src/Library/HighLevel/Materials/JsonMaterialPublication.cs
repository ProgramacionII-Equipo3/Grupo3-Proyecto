using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Linq;
using System.Text.Json.Serialization;
using Library.HighLevel.Accountability;
using Ucu.Poo.Locations.Client;
using Library.Utils;

namespace Library.HighLevel.Materials
{
    /// <summary>
    /// This class holds the JSON information of a <see cref="MaterialPublication" />.
    /// </summary>
    public class JsonMaterialPublication : IJsonHolder<MaterialPublication>
    {
        /// <summary>
        /// Gets the publication's material.
        /// </summary>
        public JsonMaterial Material { get; set; } = new JsonMaterial();

        /// <summary>
        /// Gets the publication's amount of material.
        /// </summary>
        public JsonAmount Amount { get; set; } = new JsonAmount();

        /// <summary>
        /// Gets the publication's price of the material.
        /// </summary>
        public JsonPrice Price { get; set; } = new JsonPrice();

        /// <summary>
        /// Gets the publication's pick-up location of material.
        /// </summary>
        public Location? PickupLocation { get; set; }

        /// <summary>
        /// Gets the type of the material publication.
        /// </summary>
        public JsonMaterialPublicationTypeData Type { get; set; } = new JsonMaterialPublicationTypeData();

        /// <summary>
        /// The list of keywords of the publication material.
        /// </summary>
        [JsonInclude]
        public IList<string>? Keywords { get; set; }

        /// <summary>
        /// The list of requirements of the material publication.
        /// </summary>
        [JsonInclude]
        public IList<string>? Requirements { get; set; }

        /// <inheritdoc />
        public void FromValue(MaterialPublication value)
        {
            this.Material = new JsonMaterial();
            this.Material.FromValue(value.Material);
            this.Amount = new JsonAmount();
            this.Amount.FromValue(value.Amount);
            this.Price = new JsonPrice();
            this.Price.FromValue(value.Price);
            this.PickupLocation = value.PickupLocation;
            this.Type = new JsonMaterialPublicationTypeData();
            this.Type.FromValue(value.Type);
            this.Keywords = value.Keywords.ToList();
            this.Requirements = value.Requirements.ToList();
        }

        /// <inheritdoc />
        public MaterialPublication ToValue() =>
            MaterialPublication.CreateInstance(
                this.Material.ToValue(),
                this.Amount.ToValue(),
                this.Price.ToValue(),
                this.PickupLocation!,
                this.Type.ToValue(),
                this.Keywords!,
                this.Requirements!).Unwrap();
    }
}
