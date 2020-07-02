using ButtonGrid_Git.Constants;
using ButtonGrid_Git.Enum;
using System.Windows.Controls;

namespace ButtonGrid_Git.DTO
{
    public class GridButton : Button
    {

        public GridPosition TopAdjacency { get; set; }
        public GridPosition RightAdjacency { get; set; }
        public GridPosition DownAdjacency { get; set; }
        public GridPosition LeftAdjacency { get; set; }

        public bool IsTopAdjacencyValid 
        {
            get 
            {
                if (this.TopAdjacency.RowNumber != VariableConstants.INVALID_ADJACENCY && this.TopAdjacency.ColumnNumber != VariableConstants.INVALID_ADJACENCY)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsDownAdjacencyValid
        {
            get
            {
                if (this.DownAdjacency.RowNumber != VariableConstants.INVALID_ADJACENCY && this.DownAdjacency.ColumnNumber != VariableConstants.INVALID_ADJACENCY)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsLeftAdjacencyValid
        {
            get
            {
                if (this.LeftAdjacency.RowNumber != VariableConstants.INVALID_ADJACENCY && this.LeftAdjacency.ColumnNumber != VariableConstants.INVALID_ADJACENCY)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsRightAdjacencyValid
        {
            get
            {
                if (this.RightAdjacency.RowNumber != VariableConstants.INVALID_ADJACENCY && this.RightAdjacency.ColumnNumber != VariableConstants.INVALID_ADJACENCY)
                {
                    return true;
                }

                return false;
            }
        }

        public bool HasBeenSwappedToLand { get; set; }

        private TerrainTypeEnum.TerrainType _gridButtonTerrainType;

        public TerrainTypeEnum.TerrainType GridButtonTerrainType
        {
            get { return _gridButtonTerrainType; }
            set
            {
                _gridButtonTerrainType = value;
                this.Background = TerrainTypeEnum.terrainTypeDictionary[_gridButtonTerrainType];
            }
        }

        public bool IsSelected { get; set; }

        public readonly GridPosition gridPosition;

        public GridButton(int rowNumber, int columnNumber)
        {
            gridPosition = new GridPosition(rowNumber, columnNumber);

            TopAdjacency = new GridPosition();
            RightAdjacency = new GridPosition();
            DownAdjacency = new GridPosition();
            LeftAdjacency = new GridPosition();
        }
        
    }
}
