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
