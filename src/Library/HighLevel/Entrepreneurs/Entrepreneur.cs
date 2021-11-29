using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Library.Core;
using Library.HighLevel.Accountability;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents an entrepreneur.
    /// We used the principle Creator to create this class, for
    /// example, the list of entrepreneur is created here.
    /// </summary>
    public partial class Entrepreneur
    {
        /// <summary>
        /// Gets or sets the entrepreneur's id.
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
        public IList<Habilitation> Habilitations { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's specialization.
        /// </summary>
        public IList<string> Specializations { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's users in the platform.
        /// </summary>
        public static IList<string> EntrepeneurList = new List<string>();


        /// <summary>
        /// Initializes a new instance of the <see cref="Entrepreneur"/> class.
        /// </summary>
        /// <param name="id">Entrepreneur's id.</param>
        /// <param name="name">Entrepreneur's name.</param>
        /// <param name="age">Entrepreneur's age.</param>
        /// <param name="location">Entrepreneur's location.</param>
        /// <param name="heading">Entrepreneur's heading.</param>
        /// <param name="habilitations">Entrepreneur's habilitation.</param>
        /// <param name="specializations">Entrepreneur's specializations.</param>
        public Entrepreneur(string id, string name, int age, Location location, string heading, IList<Habilitation> habilitations, IList<string> specializations) : this(id, name, age, location, heading, habilitations, specializations, new List<BoughtMaterialLine>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entrepreneur"/> class from JSON data.
        /// </summary>
        /// <param name="id">Entrepreneur's id.</param>
        /// <param name="name">Entrepreneur's name.</param>
        /// <param name="age">Entrepreneur's age.</param>
        /// <param name="location">Entrepreneur's location.</param>
        /// <param name="heading">Entrepreneur's heading.</param>
        /// <param name="habilitations">Entrepreneur's habilitation.</param>
        /// <param name="specializations">Entrepreneur's specializations.</param>
        /// <param name="boughtMaterials">Entrepreneur's bought material lines.</param>
        public Entrepreneur(string id, string name, int age, Location location, string heading, IList<Habilitation> habilitations, IList<string> specializations, IList<BoughtMaterialLine> boughtMaterials)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Location = location;
            this.Heading = heading;
            this.Habilitations = habilitations;
            this.Specializations = specializations;
            this.BoughtMaterials = boughtMaterials.ToList();
        }

        public void RemoveBoughtMaterialLine(int saleId)
        {
            this.BoughtMaterials.RemoveAll(line => line.Id == saleId);
        }
    }
}