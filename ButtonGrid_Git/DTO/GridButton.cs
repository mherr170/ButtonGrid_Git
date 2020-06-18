using System.Windows.Controls;

namespace ButtonGrid_Git.DTO
{
    public class GridButton : Button
    {

        public GridPosition TopAdjacency { get; set; }
        public GridPosition RightAdjacency { get; set; }
        public GridPosition DownAdjacency { get; set; }
        public GridPosition LeftAdjacency { get; set; }

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
