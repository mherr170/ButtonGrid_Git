using System;
using System.Collections.Generic;
using ButtonGrid_Git.DTO;
using ButtonGrid_Git.Enum;
using ButtonGrid_Git.Constants;
using ButtonGrid_Git.Utility.RandomNumber;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Windows.Controls;

namespace ButtonGrid_Git.TerrainGeneration
{
    class GenerateTerrain
    {
        public GenerateTerrain(GridButton[,] gridButtonMultiArray)
        {
            TerrainTypeEnum.InitTerrainTypeDictionary();

            SetGridToWaterByDefault(gridButtonMultiArray);

            GenerateLandCheck(gridButtonMultiArray);
        }

        private void GenerateLandCheck(GridButton[,] gridButtonMultiArray)
        {
            #region Variable Declaration

            //Randomly select a pre-determined number of tiles to swap to land and serve as the starting point for the terrain generation.
            List<GridButton> startingPointRandomTileList = PickRandomStartingTiles(gridButtonMultiArray);

            //This list stores the GridButtons that are adjacent to the randomTileList.
            List<GridButton> tileAdjacencyListFirstIteration = new List<GridButton>();

            //The list to store GridButtons that are adjacent to the GridButtons within the tileAdjacencyList (first iteration)
            List<GridButton> tileAdjacencyListSecondIteration = new List<GridButton>();

            //The list to store GridButtons that are adjacent to the GridButtons within the tileAdjacencyListSecondIteration
            List<GridButton> tileAdjacencyListThirdIteration = new List<GridButton>();

            //The list to store GridButtons that are adjacent to the GridButtons within the tileAdjacencyListThirdIteration
            List<GridButton> tileAdjacencyListFourthIteration = new List<GridButton>();

            #endregion

            //Swap the initial randomly selected tiles to Land.
            SwapTilesToLand(startingPointRandomTileList);

            //Gather adjacent tiles, remove duplicates and land, calculate swap to land.
            FirstIteration(gridButtonMultiArray, startingPointRandomTileList, ref tileAdjacencyListFirstIteration);

            SecondIteration(gridButtonMultiArray, tileAdjacencyListFirstIteration, ref tileAdjacencyListSecondIteration);

            ThirdIteration(gridButtonMultiArray, tileAdjacencyListSecondIteration, ref tileAdjacencyListThirdIteration);

            FourthIteration(gridButtonMultiArray, tileAdjacencyListThirdIteration, ref tileAdjacencyListFourthIteration);
        }

        private void FirstIteration(GridButton[,] gridButtonMultiArray, List<GridButton> startingPointRandomTileList, ref List<GridButton> tileAdjacencyListFirstIteration)
        {
            //Gather the tiles that are adjacent to the startingPointRandomTileList
            GatherAdjacentTiles(gridButtonMultiArray, startingPointRandomTileList, tileAdjacencyListFirstIteration);

            //Remove any possible duplicate GridButtons within the list.
            tileAdjacencyListFirstIteration = tileAdjacencyListFirstIteration.Distinct().ToList();

            //Unlikely at this point in the process, but remove any tiles that have been swapped to land into a temp list.  Don't need to calculate for these.
            //Doing this into a temporary list as to keep the data intact within the original "tileAdjacencyListFirstIteration" list.
            List<GridButton> tempGridButtonList = RemoveTilesThatHaveBeenSwappedToLand(tileAdjacencyListFirstIteration);

            //For the First Iteration, the adjacent tiles have an 80% chance to be swapped from Water to Land.
            CalculateAndSwapFirstIteration(tempGridButtonList);
        }

        private void SecondIteration(GridButton[,] gridButtonMultiArray, List<GridButton> tileAdjacencyListFirstIteration, ref List<GridButton> tileAdjacencyListSecondIteration)
        {
            //Populate the tiles that are adjacent to those within the tileAdjacencyListFirstIteration.
            GatherAdjacentTiles(gridButtonMultiArray, tileAdjacencyListFirstIteration, tileAdjacencyListSecondIteration);

            //Remove any possible duplicate GridButtons within the grid.
            tileAdjacencyListSecondIteration = tileAdjacencyListSecondIteration.Distinct().ToList();

            //By this point, within tileAdjacencyListSecondIteration there are tiles being collected that have already been swapped to Land from previous iterations or from the starting point.
            //Remove them into a temporary list.  Not removing from the main list, because that would interfere with the next iteration's adjacent tile gathering.
            List<GridButton> tempGridButtonList = RemoveTilesThatHaveBeenSwappedToLand(tileAdjacencyListSecondIteration);

            //The second interation where the tiles within the tileAdjacencyListSecondIteration have 60% chance to be swapped to Land.
            CalculateAndSwapSecondIteration(tempGridButtonList);
        }

        private void ThirdIteration(GridButton[,] gridButtonMultiArray, List<GridButton> tileAdjacencyListSecondIteration, ref List<GridButton> tileAdjacencyListThirdIteration)
        {
            //Populate the tiles that are adjacent to those within the tileAdjacencyListSecondIteration
            GatherAdjacentTiles(gridButtonMultiArray, tileAdjacencyListSecondIteration, tileAdjacencyListThirdIteration);

            tileAdjacencyListThirdIteration = tileAdjacencyListThirdIteration.Distinct().ToList();

            //Remove the tiles that have been swapped to land again here from the tileAdjacencyListThirdIteration list.
            List<GridButton> tempGridButtonList = RemoveTilesThatHaveBeenSwappedToLand(tileAdjacencyListThirdIteration);

            //The third iteration where the tiles within the tileAdjacencyListThirdIteration have a 40% chance to be swapped to Land.
            CalculateAndSwapThirdIteration(tempGridButtonList);
        }

        private void FourthIteration(GridButton[,] gridButtonMultiArray, List<GridButton> tileAdjacencyListThirdIteration, ref List<GridButton> tileAdjacencyListFourthIteration)
        {
            GatherAdjacentTiles(gridButtonMultiArray, tileAdjacencyListThirdIteration, tileAdjacencyListFourthIteration);

            tileAdjacencyListFourthIteration = tileAdjacencyListFourthIteration.Distinct().ToList();

            List<GridButton> tempGridButtonList = RemoveTilesThatHaveBeenSwappedToLand(tileAdjacencyListFourthIteration);

            CalculateAndSwapFourthIteration(tempGridButtonList);
        }

        private List<GridButton> RemoveTilesThatHaveBeenSwappedToLand(List<GridButton> tileAdjacencyListFirstIteration)
        {
            List<GridButton> temporaryDuplicatesRemovalList = new List<GridButton>(tileAdjacencyListFirstIteration);

            temporaryDuplicatesRemovalList.RemoveAll(x => x.HasBeenSwappedToLand == true);

            return temporaryDuplicatesRemovalList;
        }

        private void CalculateAndSwapFourthIteration(List<GridButton> tileAdjacencyListFourthIteration)
        { 
            foreach (GridButton gridButton in tileAdjacencyListFourthIteration)
            {
                CalculateTwentyPercentChanceToSwitchToLand(gridButton);
            }
        }

        private void CalculateTwentyPercentChanceToSwitchToLand(GridButton gridButton)
        {
            //There is an 80% chance to switch the adjacent tiles from "Water" to "Land"
            switch (RandomNumberGenerator.GetNextRandomNumberBetween1and5())
            {
                //If a "1" has been generated, this is the 20% chance to swap to Land.
                case 1:
                    gridButton.GridButtonTerrainType = TerrainTypeEnum.TerrainType.Land;
                    gridButton.HasBeenSwappedToLand = true;
                    break;
                //Else, a "2", "3", "4", or "5" was generated.
                default:

                    break;
            }
        }

        private void CalculateAndSwapThirdIteration(List<GridButton> tileAdjacencyListThirdIteration)
        {
            foreach (GridButton gridButton in tileAdjacencyListThirdIteration)
            {
                if (!gridButton.HasBeenSwappedToLand)
                { 
                    CalculateFortyPercentChanceAndSwitchToLand(gridButton);
                }
            }            
        }

        private void CalculateFortyPercentChanceAndSwitchToLand(GridButton gridButton)
        {
            //There is an 80% chance to switch the adjacent tiles from "Water" to "Land"
            switch (RandomNumberGenerator.GetNextRandomNumberBetween1and5())
            {
                //If a "1" or "2" has been generated, this is the 40% to swap to Land.
                case 1:
                case 2:
                    gridButton.GridButtonTerrainType = TerrainTypeEnum.TerrainType.Land;
                    gridButton.HasBeenSwappedToLand = true;
                    break;
                //Else, a "3", "4", or "5" was generated.
                default:

                    break;
            }
        }

        private void GatherAdjacentTiles(GridButton[,] gridButtonMultiArray, List<GridButton> sourceList, List<GridButton> childList)
        {
            foreach (GridButton gridButton in sourceList)
            {
                if (gridButton.IsTopAdjacencyValid)
                {
                    childList.Add(gridButtonMultiArray[gridButton.TopAdjacency.RowNumber, gridButton.TopAdjacency.ColumnNumber]);
                }

                if (gridButton.IsDownAdjacencyValid)
                {
                    childList.Add(gridButtonMultiArray[gridButton.DownAdjacency.RowNumber, gridButton.DownAdjacency.ColumnNumber]);
                }

                if (gridButton.IsLeftAdjacencyValid)
                {
                    childList.Add(gridButtonMultiArray[gridButton.LeftAdjacency.RowNumber, gridButton.LeftAdjacency.ColumnNumber]);
                }

                if (gridButton.IsRightAdjacencyValid)
                {
                    childList.Add(gridButtonMultiArray[gridButton.RightAdjacency.RowNumber, gridButton.RightAdjacency.ColumnNumber]);
                }
            }
        }

        private void CalculateAndSwapSecondIteration(List<GridButton> tileAdjacencyListSecondIteration)
        {
            foreach (GridButton adjacentTile in tileAdjacencyListSecondIteration)
            {        
                CalculateSixtyPercentChanceAndSwitchToLand(adjacentTile);
            }
        }

        private void CalculateSixtyPercentChanceAndSwitchToLand(GridButton adjacentTile)
        {
            switch (RandomNumberGenerator.GetNextRandomNumberBetween1and5())
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

        private void CalculateAndSwapFirstIteration(List<GridButton> tileAdjacencyListFirstIteration)
        {
            foreach (GridButton gridButton in tileAdjacencyListFirstIteration)
            {
                CalculateEightyPercentChanceAndSwitchToLand(gridButton);
            }
        }

        private void CalculateEightyPercentChanceAndSwitchToLand(GridButton gridButton)
        {
            //There is an 80% chance to switch the adjacent tiles from "Water" to "Land"
            switch (RandomNumberGenerator.GetNextRandomNumberBetween1and5())
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
            int randomRow = RandomNumberGenerator.GetNextRandomNumberBetween0andSQUARE_SIDE_LENGTH();
            int randomColumn = RandomNumberGenerator.GetNextRandomNumberBetween0andSQUARE_SIDE_LENGTH();

            return gridButtonMultiArray[randomRow, randomColumn];
        }

        private void SwapTilesToLand(List<GridButton> startingPointRandomTileList)
        {
            foreach (GridButton gridButton in startingPointRandomTileList)
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

        private void RemoveDuplicateGridButtons()
        {

        }

    }
}
