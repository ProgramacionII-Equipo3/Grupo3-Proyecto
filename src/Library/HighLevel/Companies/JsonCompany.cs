using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using Library.Core;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Companies
{
    /// <summary>
    /// This class acts as a JSON data holder for companies.
    /// </summary>
    public class JsonCompany : IJsonHolder<Company>
    {
        /// <summary>
        /// Gets the company's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the company's contact information.
        /// </summary>
        public ContactInfo ContactInfo { get; set; }

        /// <summary>
        /// Gets the company's heading.
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Gets the company's location.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// The company's representants in the platform.
        /// </summary>
        [JsonInclude]
        public IList<string> Representants { get; set; }

        /// <summary>
        /// Gets a private list of the publications.
        /// </summary>
        [JsonInclude]
        public IList<JsonMaterialPublication> Publications { get; set; }

        /// <summary>
        /// Gets the list of material sales.
        /// </summary>
        [JsonInclude]
        public IList<JsonMaterialSalesLine> MaterialSales { get; set; }

        /// <inheritdoc />
        public void FromValue(Company value)
        {
            this.Name = value.Name;
            this.ContactInfo = value.ContactInfo;
            this.Heading = value.Heading;
            this.Location = value.Location;
            this.Representants = value.Representants;
            this.Publications = value.Publications.Select(p =>
            {
                var r = new JsonMaterialPublication();
                r.FromValue(p);
                return r;
            }).ToList();
            this.MaterialSales = value.MaterialSales.Select(s =>
            {
                var r = new JsonMaterialSalesLine();
                r.FromValue(s);
                return r;
            }).ToList();
        }

        /// <inheritdoc />
        public Company ToValue() =>
            new Company(
                this.Name,
                this.ContactInfo,
                this.Heading,
                this.Location,
                this.Representants,
                this.Publications.Select(json => json.ToValue()).ToList(),
                this.MaterialSales.Select(json => json.ToValue()).ToList());
    }
}