using System;
using System.Collections;
using System.Text;
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

        public TextBlock adjacencyInfoTextBlock;

        public MainWindow()
        {
            GridButton[,] gridButtonMultiArray = new GridButton[Convert.ToInt32(SQUARE_SIDE_LENGTH), Convert.ToInt32(SQUARE_SIDE_LENGTH)];

            InitializeComponent();

            StackPanel mainStackPanel = Init_StackPanel();

            //Add a Text Block to display the adjacency information for any button that you click.
            adjacencyInfoTextBlock = Init_AdjacencyInfoTextBlock();
            
            Grid gridPanel = Init_GridPanel(gridButtonMultiArray);

            HightlightMiddleOfGrid(gridButtonMultiArray);

            mainStackPanel.Children.Add(adjacencyInfoTextBlock);
            mainStackPanel.Children.Add(gridPanel);

            //Add the Stack Panel with all of our logic into the actual Content 
            Content = mainStackPanel;
        }

        private TextBlock Init_AdjacencyInfoTextBlock()
        {

            StringBuilder s = new StringBuilder();

            s.Append("My Position - Row: _ - Col: _");
            s.Append("\n");

            s.Append("Top - Row: _ - Col: _");
            s.Append("\n");

            s.Append("Right - Row: _ - Col: _");
            s.Append("\n");

            s.Append("Bottom - Row: _ - Col: _");
            s.Append("\n");

            s.Append("Left - Row: _ - Col: _");
            s.Append("\n");

            TextBlock _textBlock = new TextBlock
            {
                Text = s.ToString(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
             };

            return _textBlock;
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

                    GridButton newGridButton = Init_SingleGridButton(rowNumber, columnNumber);

                    Grid.SetColumn(newGridButton, columnNumber);
                    Grid.SetRow(newGridButton, rowNumber);

                    gridPanel.Children.Add(newGridButton);

                    //Check Adjacendy of the new GridButton
                    CheckAdjacency(newGridButton, rowNumber, columnNumber);

                    //Add to 2D Array
                    gridButtonMultiArray[rowNumber, columnNumber] = newGridButton;
                }

            }

            return gridPanel;
        }

        //If this is working, can definitely clean up some of the redundant assignments.
        private void CheckAdjacency(GridButton newGridButton, int rowNumber, int ColumnNumber)
        {
            //Does it have valid top adjacency?
            if (rowNumber - 1 < 0)
            {
                newGridButton.TopAdjacency.RowNumber = INVALID_ADJACENCY;
            }
            else
            {
                newGridButton.TopAdjacency.RowNumber = rowNumber - 1;
            }

            newGridButton.TopAdjacency.ColumnNumber = ColumnNumber;

            //Does it have valid right adjacency?
            if (ColumnNumber + 1 > SQUARE_SIDE_LENGTH)
            {
                newGridButton.RightAdjacency.ColumnNumber = INVALID_ADJACENCY;
            }
            else
            {
                newGridButton.RightAdjacency.ColumnNumber = ColumnNumber + 1;
            }

            newGridButton.RightAdjacency.RowNumber = rowNumber;

            //Does it have valid down adjacency?
            if (rowNumber + 1 > SQUARE_SIDE_LENGTH)
            {
                newGridButton.DownAdjacency.RowNumber = INVALID_ADJACENCY;
            }
            else
            {
                newGridButton.DownAdjacency.RowNumber = rowNumber + 1;
            }

            newGridButton.DownAdjacency.ColumnNumber = ColumnNumber;

            //Does it have valid left adjacency?
            if (ColumnNumber - 1 < 0)
            {
                newGridButton.LeftAdjacency.ColumnNumber = INVALID_ADJACENCY;
            }
            else
            {
                newGridButton.LeftAdjacency.ColumnNumber = ColumnNumber - 1;
            }

            newGridButton.LeftAdjacency.RowNumber = rowNumber;

        }

        private StackPanel Init_StackPanel()
        {
            StackPanel _stackPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            return _stackPanel;
        }

        private GridButton Init_SingleGridButton(int rowNumber, int columnNumber)
        {
            GridButton newGridButton = new GridButton(rowNumber, columnNumber)
            {
                Background = Brushes.Red,
            };

            newGridButton.Content = newGridButton.gridPosition.RowNumber + ", " + newGridButton.gridPosition.ColumnNumber;

            newGridButton.Click += NewGridButton_Click_DisplayAdjacencyInfo;

            return newGridButton;
        }

        private void NewGridButton_Click_DisplayAdjacencyInfo(object sender, RoutedEventArgs e)
        {
            GridButton tempGridButton = e.Source as GridButton;

            StringBuilder s = new StringBuilder();

            s.Append(String.Format("My Position - Row: {0} - Col: {1}", IsPositionValid(tempGridButton.gridPosition.RowNumber), IsPositionValid(tempGridButton.gridPosition.ColumnNumber)));
            s.Append("\n");

            s.Append(String.Format("Top - Row: {0} - Col: {1}", IsPositionValid(tempGridButton.TopAdjacency.RowNumber), IsPositionValid(tempGridButton.TopAdjacency.ColumnNumber)));
            s.Append("\n");

            s.Append(String.Format("Right - Row: {0} - Col: {1}", IsPositionValid(tempGridButton.RightAdjacency.RowNumber), IsPositionValid(tempGridButton.RightAdjacency.ColumnNumber)));
            s.Append("\n");

            s.Append(String.Format("Bottom - Row: {0} - Col: {1}", IsPositionValid(tempGridButton.DownAdjacency.RowNumber), IsPositionValid(tempGridButton.DownAdjacency.ColumnNumber)));
            s.Append("\n");

            s.Append(String.Format("Left - Row: {0} - Col: {1}", IsPositionValid(tempGridButton.LeftAdjacency.RowNumber), IsPositionValid(tempGridButton.LeftAdjacency.ColumnNumber)));
            s.Append("\n");

            adjacencyInfoTextBlock.Text = s.ToString();

        }

        private string IsPositionValid(int number)
        {
            if (number == INVALID_ADJACENCY || number >= SQUARE_SIDE_LENGTH)
            {
                return "Invalid Position";
            }    
            else
            {
                return number.ToString();
            }
        }

        private void HightlightMiddleOfGrid(GridButton[,] gridButtonMultiArray)
        {
            double startingRow = Math.Floor(SQUARE_SIDE_LENGTH / 2);

            double staringColumn = Math.Floor(SQUARE_SIDE_LENGTH / 2);

            gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(staringColumn)].Background = Brushes.LightBlue;

            gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(staringColumn)].IsSelected = true;
        }

    }
}
