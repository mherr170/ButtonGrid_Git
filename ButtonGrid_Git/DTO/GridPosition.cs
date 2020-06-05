namespace ButtonGrid_Git.DTO
{
    class GridPosition
    {
        //The "set" on the X and Y Coordinate Properties are private because once the coordinates are set within the Constructor, they should be function as "readonly".
        //That is to say, once the coordinates are set, they will not (and should not) change.
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }

        public GridPosition(int _rowNumber, int _columnNumber)
        {
            RowNumber = _rowNumber;
            ColumnNumber = _columnNumber;
        }

        //This constructor gets used when initializing the Adjacency GridPositions
        public GridPosition()
        {

        }
    }
}
