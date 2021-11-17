using System.Collections.Generic;
using Library.Core;
using Library.HighLevel.Materials;
using Ucu.Poo.Locations.Client;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents an entrepreneur.
    /// We used the principle Creator to create this class, for
    /// example, the list of entrepreneur is created here.
    /// </summary>
    public class Entrepreneur
    {
        /// <summary>
        /// Gets or sets the entrepreneur's id.
        /// </summary>
        public UserId Id { get; set; }

        /// <summary>
        /// Gets the entrepeneur's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's age.
        /// </summary>
        public string Age { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's location.
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's heading.
        /// </summary>
        public string Heading { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's specialization.
        /// </summary>
        public List<string> Specialization { get; private set; }

        /// <summary>
        /// Gets the entrepreneur's users in the platform.
        /// </summary>
        public static List<UserId> EntrepeneurList = new List<UserId>();

        /// <summary>
        /// Gets the entrepreneur's habilitation needed to buy certain materials.
        /// </summary>
        public List<Habilitation> Habilitation = new List<Habilitation>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Entrepreneur"/> class.
        /// </summary>
        /// <param name="id">Entrepreneur´s id.</param>
        /// <param name="name">Entrepreneur´s name.</param>
        /// <param name="age">Entrepreneur´s age.</param>
        /// <param name="location">Entrepreneur´s location.</param>
        /// <param name="heading">Entrepreneur´s heading.</param>
        /// <param name="habilitations">Entrepreneur´s habilitation.</param>
        /// <param name="specializations">Entrepreneur´s specializations.</param>
        public Entrepreneur(UserId id, string name, string age, Location location, string heading, List<Habilitation> habilitations, List<string> specializations)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Location = location;
            this.Heading = heading;
            this.Habilitation = habilitations;
            this.Specialization = specializations;
        }
    }
}