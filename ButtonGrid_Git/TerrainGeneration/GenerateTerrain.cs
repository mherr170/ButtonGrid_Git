using System;
using System.Collections.Generic;
using System.Windows.Media;
using ButtonGrid_Git.DTO;
using ButtonGrid_Git.Enum;

namespace ButtonGrid_Git.TerrainGeneration
{
    class GenerateTerrain
    {
        public GenerateTerrain(GridButton[,] gridButtonMultiArray, double StartingRow, double StartingColumn)
        {
            TerrainTypeEnum.InitTerrainTypeDictionary();

            SetGridCenterToLand(gridButtonMultiArray, StartingRow, StartingColumn);
        }

        private void SetGridCenterToLand(GridButton[,] gridButtonMultiArray, double StartingRow, double StartingColumn)
        {
            gridButtonMultiArray[Convert.ToInt32(StartingRow), Convert.ToInt32(StartingColumn)].GridButtonTerrainType = TerrainTypeEnum.TerrainType.Land;
            gridButtonMultiArray[Convert.ToInt32(StartingRow), Convert.ToInt32(StartingColumn)].Background = TerrainTypeEnum.terrainTypeDictionary[TerrainTypeEnum.TerrainType.Land];
        }
    }
}
