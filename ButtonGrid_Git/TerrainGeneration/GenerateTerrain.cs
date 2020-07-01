using System;
using System.Collections.Generic;
using System.Windows.Media;
using ButtonGrid_Git.DTO;
using ButtonGrid_Git.Enum;
using ButtonGrid_Git.Constants;
using System.Windows.Controls;

namespace ButtonGrid_Git.TerrainGeneration
{
    class GenerateTerrain
    {
        private Random _random = new Random();

        public GenerateTerrain(GridButton[,] gridButtonMultiArray)
        {
            TerrainTypeEnum.InitTerrainTypeDictionary();

            SetGridToWaterByDefault(gridButtonMultiArray);

            GenerateLandCheck(gridButtonMultiArray);
        }

        private void GenerateLandCheck(GridButton[,] gridButtonMultiArray)
        {
            //Randomly select a pre-determined number of tiles to swap to land and serve as the starting point for the terrain generation.
            List<GridButton> randomTileList = PickRandomStartingTiles(gridButtonMultiArray);

            //This list stores the GridButtons that, if valid, are adjacent to the randomTileList.
            List<GridButton> tileAdjacencyList = new List<GridButton>();

            //Swap the randomly selected tiles to Land.
            SwapTilesToLand(randomTileList);

            //The first iteration where the adjacent buttons have an 80% chance to be swapped to Land.
            CalculateAndSwapFirstIteration(gridButtonMultiArray, randomTileList, tileAdjacencyList);

            //Need to populate another adjacency list, from the tileAdjacencyList.
            //PopulateSecondTierAdjacencyList()

            //The second interation where the tiles within the tileAdjacencyList have 40% chance to be swapped to Land.
            //CalculateAndSwapSecondIteration(tileAdjacencyList);
        }

        private void CalculateAndSwapSecondIteration(List<GridButton> tileAdjacencyList)
        {
            foreach (GridButton adjacentTile in tileAdjacencyList)
            {
                CalculateSixtyPercentChanceAndSwitchToLand(adjacentTile);
            }
        }

        private void CalculateSixtyPercentChanceAndSwitchToLand(GridButton adjacentTile)
        {
            int randomOutcome;

            randomOutcome = _random.Next(1, VariableConstants.SELECT_RANDOM_TILES_NUMBER + 1);

            switch (randomOutcome)
            {
                //Case 1,2, and 3 is a 60% chance to swap to Land.
                case 1:
                case 2:
                case 3:
                    adjacentTile.GridButtonTerrainType = TerrainTypeEnum.TerrainType.Land;
                    adjacentTile.HasBeenSwappedToLand = true;
                    break;
                default:
                    break;
            }
        }

        private void CalculateAndSwapFirstIteration(GridButton[,] gridButtonMultiArray, List<GridButton> randomTileList, List<GridButton> tileAdjacencyList)
        {
            //Calculate percentage and swap to land if necessary for the five initially picked GridButtons.
            foreach (GridButton randomGridButton in randomTileList)
            {
                //Check validity of the Top Adjacent button.
                if (randomGridButton.TopAdjacency.RowNumber != VariableConstants.INVALID_ADJACENCY && randomGridButton.TopAdjacency.ColumnNumber != VariableConstants.INVALID_ADJACENCY)
                {
                    //If valid, assign button and calculate percent change to swap to land.
                    GridButton tempTopAdjacency = gridButtonMultiArray[randomGridButton.TopAdjacency.RowNumber, randomGridButton.TopAdjacency.ColumnNumber];
                    CalculateEightyPercentChanceAndSwitchToLand(tempTopAdjacency);

                    //Add the valid adjacent button to the tileAdjacencyList for calculating on the next iteration.
                    tileAdjacencyList.Add(gridButtonMultiArray[randomGridButton.TopAdjacency.RowNumber, randomGridButton.TopAdjacency.ColumnNumber]);
                }

                //Check validity of the Bottom (Down) Adjacent button.
                if (randomGridButton.DownAdjacency.RowNumber != VariableConstants.INVALID_ADJACENCY && randomGridButton.DownAdjacency.ColumnNumber != VariableConstants.INVALID_ADJACENCY)
                {
                    //If valid, assign button and calculate percent change to swap to land.
                    GridButton tempDownAdjacency = gridButtonMultiArray[randomGridButton.DownAdjacency.RowNumber, randomGridButton.DownAdjacency.ColumnNumber];
                    CalculateEightyPercentChanceAndSwitchToLand(tempDownAdjacency);

                    //Add the valid adjacent button to the tileAdjacencyList for calculating on the next iteration.
                    tileAdjacencyList.Add(gridButtonMultiArray[randomGridButton.DownAdjacency.RowNumber, randomGridButton.DownAdjacency.ColumnNumber]);
                }

                //Check validity of the Left Adjacent button.
                if (randomGridButton.LeftAdjacency.RowNumber != VariableConstants.INVALID_ADJACENCY && randomGridButton.LeftAdjacency.ColumnNumber != VariableConstants.INVALID_ADJACENCY)
                {
                    //If valid, assign button and calculate percent change to swap to land.
                    GridButton tempLeftAdjacency = gridButtonMultiArray[randomGridButton.LeftAdjacency.RowNumber, randomGridButton.LeftAdjacency.ColumnNumber];
                    CalculateEightyPercentChanceAndSwitchToLand(tempLeftAdjacency);

                    //Add the valid adjacent button to the tileAdjacencyList for calculating on the next iteration.
                    tileAdjacencyList.Add(gridButtonMultiArray[randomGridButton.LeftAdjacency.RowNumber, randomGridButton.LeftAdjacency.ColumnNumber]);
                }

                //Check validity of the Right Adjacent button.
                if (randomGridButton.RightAdjacency.RowNumber != VariableConstants.INVALID_ADJACENCY && randomGridButton.RightAdjacency.ColumnNumber != VariableConstants.INVALID_ADJACENCY)
                {
                    //If valid, assign button and calculate percent change to swap to land.
                    GridButton tempRightAdjacency = gridButtonMultiArray[randomGridButton.RightAdjacency.RowNumber, randomGridButton.RightAdjacency.ColumnNumber];
                    CalculateEightyPercentChanceAndSwitchToLand(tempRightAdjacency);

                    //Add the valid adjacent button to the tileAdjacencyList for calculating on the next iteration.
                    tileAdjacencyList.Add(gridButtonMultiArray[randomGridButton.RightAdjacency.RowNumber, randomGridButton.RightAdjacency.ColumnNumber]);
                }
            }
        }

        private void CalculateEightyPercentChanceAndSwitchToLand(GridButton gridButton)
        {
            int randomOutcome;

            randomOutcome = _random.Next(1, VariableConstants.SELECT_RANDOM_TILES_NUMBER + 1);

            //There is an 80% chance to switch the adjacent tiles from "Water" to "Land"
            switch (randomOutcome)
            {
                //If a "5" has been generated, this is the 20% to keep it Water.
                case 5:
                    //Do nothing actually, the tile is water by default.

                    break;
                //Else, a "1", "2", "3", or "4" was generated. 80% has been met.  Switch to Land.
                default:
                    gridButton.GridButtonTerrainType = TerrainTypeEnum.TerrainType.Land;
                    gridButton.HasBeenSwappedToLand = true;
                    break;
            }
        }

        private List<GridButton> PickRandomStartingTiles(GridButton[,] gridButtonMultiArray)
        {
            List<GridButton> randomTileList = new List<GridButton>();

            for (int i = 0; i < VariableConstants.SELECT_RANDOM_TILES_NUMBER; i++)
            {
                GridButton randomlySelectedGridButton = SelectRandomTile(gridButtonMultiArray);

                randomTileList.Add(randomlySelectedGridButton);
            }

            return randomTileList;
        }

        private GridButton SelectRandomTile(GridButton[,] gridButtonMultiArray)
        {
            int randomRow = _random.Next(0, Convert.ToInt32(VariableConstants.SQUARE_SIDE_LENGTH));
            int randomColumn = _random.Next(0, Convert.ToInt32(VariableConstants.SQUARE_SIDE_LENGTH));

            return gridButtonMultiArray[randomRow, randomColumn];
        }

        private void SwapTilesToLand(List<GridButton> randomTileList)
        {
            foreach (GridButton gridButton in randomTileList)
            {
                gridButton.GridButtonTerrainType = TerrainTypeEnum.TerrainType.Land;
                gridButton.HasBeenSwappedToLand = true;
            }
        }

        private void SetGridToWaterByDefault(GridButton[,] gridButtonMultiArray)
        {
            for (int rowNumber = 0; rowNumber < VariableConstants.SQUARE_SIDE_LENGTH; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < VariableConstants.SQUARE_SIDE_LENGTH; columnNumber++)
                {
                    gridButtonMultiArray[rowNumber, columnNumber].GridButtonTerrainType = TerrainTypeEnum.TerrainType.Water;
                }
            }
        }

    }
}
