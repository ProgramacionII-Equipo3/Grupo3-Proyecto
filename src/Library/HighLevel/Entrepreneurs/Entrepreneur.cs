using System.Collections.Generic;
using System.Linq;
using Library.HighLevel.Materials;
using Library.HighLevel.Accountability;
using Library.Core;


namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// This class represents a entrepreneur.
    /// </summary>
    public class Entrepreneur 
    {

        /// <summary>
        /// The Id to identify the entrepreneur.
        /// </summary>
        /// <value></value>
        public UserId Id { get; private set; }
        
        /// <summary>
        /// The entrepeneur's name.
        /// </summary>
        /// <value></value>
        public string Name { get; private set; }
       
        /// <summary>
        /// The entrepreneur's age.
        /// </summary>
        /// <value></value>
        public string Age { get; private set; }

        /// <summary>
        /// The entrepreneur's location.
        /// </summary>
        /// <value></value>
        public Location Location { get; private set; }

        /// <summary>
        /// The entrepreneur's heading.
        /// </summary>
        /// <value></value>
        public string Heading { get; private set; }

        /// <summary>
        /// The entrepreneur's habilitation needed to buy certain materials.
        /// </summary>
        public  List<Habilitation> Habilitation = new List<Habilitation>();

        /// <summary>
        /// The entrepreneur's specialization.
        /// </summary>
        public List<Specialization> Specialization = new List<Specialization>();

        /// <summary>
        /// The entrepreneur's users in the platform.
        /// </summary>
        public static List<UserId> entrepeneurList = new List<UserId>();

        /// <summary>
        /// Entrepreneur's Constructor.
        /// </summary>
        public Entrepreneur(UserId id, string name, string age, string location, string heading,List<Habilitation> habilitations, List<Specialization> specializations)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Location = Location;
            this.Heading = heading;
            this.Habilitation= habilitations;
            this.Specialization = specializations;
        }
    }
}