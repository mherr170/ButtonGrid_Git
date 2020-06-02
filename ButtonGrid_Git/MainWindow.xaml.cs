using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ButtonGrid_Git
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly double SQUARE_SIDE_LENGTH = 11;

        private const int INVALID_ADJACENCY = -1;

        public MainWindow()
        {
            GridButton[,] gridButtonMultiArray = new GridButton[Convert.ToInt32(SQUARE_SIDE_LENGTH), Convert.ToInt32(SQUARE_SIDE_LENGTH)];

            InitializeComponent();

            StackPanel mainStackPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Grid gridPanel = Init_GridPanel(gridButtonMultiArray);



            double startingRow = Math.Floor(SQUARE_SIDE_LENGTH / 2);

            double staringColumn = Math.Floor(SQUARE_SIDE_LENGTH / 2);

            gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(staringColumn)].Background = Brushes.LightBlue;

            gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(staringColumn)].IsSelected = true;



            mainStackPanel.Children.Add(gridPanel);

            //Add the Stack Panel with all of our logic into the actual Content 
            Content = mainStackPanel;
        }

        private Grid Init_GridPanel(GridButton[,] gridButtonMultiArray)
        {
            Grid gridPanel = new Grid();

            for (int rowNumber = 0; rowNumber < SQUARE_SIDE_LENGTH; rowNumber++)
            {
                gridPanel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                
                for (int columnNumber = 0; columnNumber < SQUARE_SIDE_LENGTH; columnNumber++)
                {
                    gridPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                    GridButton newGridButton = new GridButton(rowNumber, columnNumber)
                    {
                        Background = Brushes.Red,                 
                    };

                    newGridButton.Content = newGridButton.gridPosition.RowNumber + ", " + newGridButton.gridPosition.ColumnNumber;

                    Grid.SetColumn(newGridButton, columnNumber);
                    Grid.SetRow(newGridButton, rowNumber);

                    gridPanel.Children.Add(newGridButton);

                    //Check Adjacendy of the new GridButton
                    CheckAdjacency(newGridButton, rowNumber, columnNumber);

                    //Add to 2D Array
                    gridButtonMultiArray[rowNumber, columnNumber] = newGridButton;
                }

            }

            //next - write a print function to verify that your adjacency values are correct.

            return gridPanel;
        }

        private void CheckAdjacency(GridButton newGridButton, int rowNumber, int ColumnNumber)
        {
            //Does it have valid top adjacency?

            if (rowNumber - 1 < 0)
            {
                newGridButton.TopAdjacency.RowNumber = INVALID_ADJACENCY;
                newGridButton.TopAdjacency.ColumnNumber = ColumnNumber;
            }
            else
            {
                newGridButton.TopAdjacency.RowNumber = rowNumber - 1;
                newGridButton.TopAdjacency.ColumnNumber = ColumnNumber;
            }


            //Does it have valid right adjacency?

            if (ColumnNumber + 1 > SQUARE_SIDE_LENGTH)
            {
                newGridButton.RightAdjacency.RowNumber = rowNumber;
                newGridButton.RightAdjacency.ColumnNumber = INVALID_ADJACENCY;
                
            }
            else
            {
                newGridButton.RightAdjacency.RowNumber = rowNumber;
                newGridButton.RightAdjacency.ColumnNumber = ColumnNumber + 1;
            }

            //Does it have valid down adjacency?

            if (rowNumber + 1 > SQUARE_SIDE_LENGTH)
            {
                newGridButton.DownAdjacency.RowNumber = INVALID_ADJACENCY;
                newGridButton.DownAdjacency.ColumnNumber = ColumnNumber;
            }
            else
            {
                newGridButton.DownAdjacency.RowNumber = rowNumber + 1;
                newGridButton.DownAdjacency.ColumnNumber = ColumnNumber;
            }


            //Does it have valid left adjacency?
            if (ColumnNumber - 1 < 0)
            {
                newGridButton.LeftAdjacency.RowNumber = rowNumber;
                newGridButton.LeftAdjacency.ColumnNumber = INVALID_ADJACENCY;
            }
            else
            {
                newGridButton.LeftAdjacency.RowNumber = rowNumber;
                newGridButton.LeftAdjacency.ColumnNumber = ColumnNumber - 1;
            }

        }
    }
}
