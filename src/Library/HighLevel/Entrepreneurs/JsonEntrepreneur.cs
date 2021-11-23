using Ucu.Poo.Locations.Client;
using System.Collections.Generic;
using System.Linq;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class acts as a JSON data holder for entrepreneurs.
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
        public string Name { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's age.
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's location.
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's heading.
        /// </summary>
        public string Heading { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's habilitation needed to buy certain materials.
        /// </summary>
        public IList<JsonHabilitation> Habilitations { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's specialization.
        /// </summary>
        public IList<string> Specializations { get; private set; }

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
            });
            this.Specializations = value.Specializations;
        }

        Entrepreneur IJsonHolder<Entrepreneur>.ToValue() =>
            new Entrepreneur(this.Id, this.Name, this.Age, this.Location, this.Heading, this.Habilitations.Select(json => json.ToValue()), this.Specializations);
    }
}