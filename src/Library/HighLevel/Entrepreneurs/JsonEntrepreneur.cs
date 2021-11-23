using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Ucu.Poo.Locations.Client;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This struct holds the JSON information of an <see cref="Entrepreneur" />.
    /// </summary>
    public struct JsonEntrepreneur : IJsonHolder<Entrepreneur>
    {
        /// <summary>
        /// Gets the entrepreneur's id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the entrepeneur's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the entrepreneur's age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets the entrepreneur's location.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Gets the entrepreneur's heading.
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Gets the entrepreneur's habilitation needed to buy certain materials.
        /// </summary>
        [JsonInclude]
        public IList<JsonHabilitation> Habilitations { get; set; }

        /// <summary>
        /// Gets the entrepreneur's specialization.
        /// </summary>
        [JsonInclude]
        public IList<string> Specializations { get; set; }

        /// <summary>
        /// Gets the collection of bought materials.
        /// </summary>
        [JsonInclude]
        public IList<JsonBoughtMaterialLine> BoughtMaterials { get; private set; }


        void IJsonHolder<Entrepreneur>.FromValue(Entrepreneur value)
        {
            this.Id = value.Id;
            this.Name = value.Name;
            this.Age = value.Age;
            this.Location = value.Location;
            this.Heading = value.Heading;
            this.Habilitations = value.Habilitations.Select(h =>
            {
                var json = new JsonHabilitation();
                json.FromValue(h);
                return json;
            }).ToList();
            this.Specializations = value.Specializations;
        }

        Entrepreneur IJsonHolder<Entrepreneur>.ToValue() =>
            new Entrepreneur(
                this.Id,
                this.Name,
                this.Age,
                this.Location,
                this.Heading,
                this.Habilitations.Select(json => json.ToValue()).ToList(),
                this.Specializations,
                this.BoughtMaterials.Select(json => json.ToValue()).ToList());
    }
}