using System.Collections.Generic;
using System.Windows.Media;

namespace ButtonGrid_Git.Enum
{
    public static class TerrainTypeEnum
    {
        //Get this out of the Enum class obviously...
        public static Dictionary<TerrainTypeEnum.TerrainType, SolidColorBrush> terrainTypeDictionary;

        public static void InitTerrainTypeDictionary()
        {
            terrainTypeDictionary = new Dictionary<TerrainType, SolidColorBrush>();

            terrainTypeDictionary.Add(TerrainTypeEnum.TerrainType.None, Brushes.Red);
            terrainTypeDictionary.Add(TerrainTypeEnum.TerrainType.Land, Brushes.Brown);
            terrainTypeDictionary.Add(TerrainTypeEnum.TerrainType.Water, Brushes.DarkBlue);
        }

        public enum TerrainType
        {
            None,
            Land,
            Water
        }
    }
}
