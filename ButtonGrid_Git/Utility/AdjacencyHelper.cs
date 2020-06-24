using ButtonGrid_Git.DTO;
using ButtonGrid_Git.Constants;


namespace ButtonGrid_Git.Utility.Adjacency
{
    static class AdjacencyHelper
    {
        //If this is working, can definitely clean up some of the redundant assignments.
        public static void SetAdjacency(GridButton newGridButton, int rowNumber, int columnNumber)
        {
            setTopAdjacency(newGridButton, rowNumber, columnNumber);

            setRightAdjacency(newGridButton, rowNumber, columnNumber);

            setDownAdjacency(newGridButton, rowNumber, columnNumber);

            setLeftAdjacency(newGridButton, rowNumber, columnNumber);
        }

        private static void setTopAdjacency(GridButton newGridButton, int rowNumber, int columnNumber)
        {
            //Does it have valid top adjacency?
            if (rowNumber - 1 < 0)
            {
                newGridButton.TopAdjacency.RowNumber = VariableConstants.INVALID_ADJACENCY;
            }
            else
            {
                newGridButton.TopAdjacency.RowNumber = rowNumber - 1;
            }

            newGridButton.TopAdjacency.ColumnNumber = columnNumber;
        }

        private static void setRightAdjacency(GridButton newGridButton, int rowNumber, int columnNumber)
        {
            //Does it have valid right adjacency?
            if (columnNumber + 1 > VariableConstants.SQUARE_SIDE_LENGTH)
            {
                newGridButton.RightAdjacency.ColumnNumber = VariableConstants.INVALID_ADJACENCY;
            }
            else
            {
                newGridButton.RightAdjacency.ColumnNumber = columnNumber + 1;
            }

            newGridButton.RightAdjacency.RowNumber = rowNumber;
        }

        private static void setDownAdjacency(GridButton newGridButton, int rowNumber, int columnNumber)
        {
            //Does it have valid down adjacency?
            if (rowNumber + 1 > VariableConstants.SQUARE_SIDE_LENGTH)
            {
                newGridButton.DownAdjacency.RowNumber = VariableConstants.INVALID_ADJACENCY;
            }
            else
            {
                newGridButton.DownAdjacency.RowNumber = rowNumber + 1;
            }

            newGridButton.DownAdjacency.ColumnNumber = columnNumber;
        }

        private static void setLeftAdjacency(GridButton newGridButton, int rowNumber, int columnNumber)
        {
            //Does it have valid left adjacency?
            if (columnNumber - 1 < 0)
            {
                newGridButton.LeftAdjacency.ColumnNumber = VariableConstants.INVALID_ADJACENCY;
            }
            else
            {
                newGridButton.LeftAdjacency.ColumnNumber = columnNumber - 1;
            }

            newGridButton.LeftAdjacency.RowNumber = rowNumber;
        }

        public static string IsPositionValid(int number)
        {
            if (number == VariableConstants.INVALID_ADJACENCY || number >= VariableConstants.SQUARE_SIDE_LENGTH)
            {
                return "Invalid Position";
            }
            else
            {
                return number.ToString();
            }
        }
    }
}
