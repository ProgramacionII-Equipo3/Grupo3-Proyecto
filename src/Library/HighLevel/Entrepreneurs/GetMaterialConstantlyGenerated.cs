using System.Collections.Generic;
using Library.HighLevel.Materials;

namespace Library.HighLevel.Entrepreneurs
{
    /// <summary>
    /// Class to find what materials are constantly generated.
    /// </summary>
    public static class GetMaterialConstantlyGenerated
    {
        /// <summary>
        /// This method returns all the materials that are constantly generated.
        /// </summary>
        public static List<Material> GetReport()
        {
            List<Material> result = new List<Material>();
            for (int i = 0; i < MaterialPublication.publications.Count; i++)
            {
                if (MaterialPublication.publications[i].Material.Name == MaterialPublication.publications[i+1].Material.Name && MaterialPublication.publications[i+1].Material.Name == MaterialPublication.publications[i+2].Material.Name)
                {
                    if (result.Contains(MaterialPublication.publications[i].Material) == false)
                    {
                        result.Add(MaterialPublication.publications[i].Material);
                    }
                }
            }
            return result;   
        }
    }
}