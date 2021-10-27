using System.Collections.Generic;
namespace Library.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// List of all the publications of materials
        /// </summary>
        public List<MaterialPublication> Publications;

        /// <summary>
        /// List for Search a specific publication
        /// </summary>
        public ReadOnlyList<MaterialPublication> Publications;

        /// <summary>
        /// Method for the company to publish a material.
        /// </summary>
        /// <param name="material">Specific material to publish</param>
        /// <param name="amount">Amount of the material</param>
        /// <param name="price">Price of the material</param>
        /// <param name="location">Location of the material</param>
        /// <returns></returns>
        public bool PublishMaterial(Material material, Amount amount, Price price, Location location);
        
        /// <summary>
        /// Method for the company to remove a specific publication of an material.
        /// </summary>
        /// <param name="publication"></param>
        /// <returns></returns>
        public bool RemovePublication(MaterialPublication publication);
    }
}