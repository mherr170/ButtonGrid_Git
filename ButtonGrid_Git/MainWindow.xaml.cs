using System;
using System.Collections;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using ButtonGrid_Git.DTO;
using ButtonGrid_Git.Adjacency;
using ButtonGrid_Git.Constants;

namespace ButtonGrid_Git
{

    public partial class MainWindow : Window
    {
        private readonly TextBlock adjacencyInfoTextBlock;

        private GridButton previouslySelectedButton;

        public MainWindow()
        {
            GridButton[,] gridButtonMultiArray = new GridButton[Convert.ToInt32(VariableConstants.SQUARE_SIDE_LENGTH), Convert.ToInt32(VariableConstants.SQUARE_SIDE_LENGTH)];

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
            TextBlock _textBlock = new TextBlock
            {
                Text = BuildDefaultAdjacencyInfoString().ToString(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
             };

            return _textBlock;
        }

        private StringBuilder BuildDefaultAdjacencyInfoString()
        {
            StringBuilder defaultAdjacencyInfoString = new StringBuilder();

            defaultAdjacencyInfoString.Append("My Position - Row: _ - Col: _");
            defaultAdjacencyInfoString.Append("\n");

            defaultAdjacencyInfoString.Append("Top - Row: _ - Col: _");
            defaultAdjacencyInfoString.Append("\n");

            defaultAdjacencyInfoString.Append("Right - Row: _ - Col: _");
            defaultAdjacencyInfoString.Append("\n");

            defaultAdjacencyInfoString.Append("Bottom - Row: _ - Col: _");
            defaultAdjacencyInfoString.Append("\n");

            defaultAdjacencyInfoString.Append("Left - Row: _ - Col: _");
            defaultAdjacencyInfoString.Append("\n");

            return defaultAdjacencyInfoString;
        }

        private Grid Init_GridPanel(GridButton[,] gridButtonMultiArray)
        {
            Grid gridPanel = new Grid();

            for (int rowNumber = 0; rowNumber < VariableConstants.SQUARE_SIDE_LENGTH; rowNumber++)
            {
                gridPanel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                
                for (int columnNumber = 0; columnNumber < VariableConstants.SQUARE_SIDE_LENGTH; columnNumber++)
                {
                    gridPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                    GridButton newGridButton = Init_SingleGridButton(rowNumber, columnNumber);

                    Grid.SetColumn(newGridButton, columnNumber);
                    Grid.SetRow(newGridButton, rowNumber);

                    gridPanel.Children.Add(newGridButton);

                    //Check Adjacendy of the new GridButton
                    AdjacencyHelper.SetAdjacency(newGridButton, rowNumber, columnNumber);

                    //Add to 2D Array
                    gridButtonMultiArray[rowNumber, columnNumber] = newGridButton;
                }
            }

            return gridPanel;
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

            //I'm not sure what order these get fired, but they both appear to work.
            newGridButton.Click += NewGridButton_Click_DisplayAdjacencyInfo;
            newGridButton.Click += NewGridButton_Click;

            return newGridButton;
        }

        private void NewGridButton_Click(object sender, RoutedEventArgs e)
        {
            GridButton tempGridButton = e.Source as GridButton;

            tempGridButton.Background = Brushes.LightBlue;

            //Change the previously selected button color back to Red, and assign the currently selected as the next previously selected.
            previouslySelectedButton.Background = Brushes.Red;

            previouslySelectedButton = tempGridButton;
        }

        private void NewGridButton_Click_DisplayAdjacencyInfo(object sender, RoutedEventArgs e)
        {
            GridButton tempGridButton = e.Source as GridButton;

            adjacencyInfoTextBlock.Text = BuildAdjacencyInfoDisplayString(tempGridButton).ToString();
        }

        private StringBuilder BuildAdjacencyInfoDisplayString(GridButton tempGridButton)
        {
            StringBuilder adjacencyInfoString = new StringBuilder();

            adjacencyInfoString.Append(String.Format("My Position - Row: {0} - Col: {1}", AdjacencyHelper.IsPositionValid(tempGridButton.gridPosition.RowNumber), AdjacencyHelper.IsPositionValid(tempGridButton.gridPosition.ColumnNumber)));
            adjacencyInfoString.Append("\n");

            adjacencyInfoString.Append(String.Format("Top - Row: {0} - Col: {1}", AdjacencyHelper.IsPositionValid(tempGridButton.TopAdjacency.RowNumber), AdjacencyHelper.IsPositionValid(tempGridButton.TopAdjacency.ColumnNumber)));
            adjacencyInfoString.Append("\n");

            adjacencyInfoString.Append(String.Format("Right - Row: {0} - Col: {1}", AdjacencyHelper.IsPositionValid(tempGridButton.RightAdjacency.RowNumber), AdjacencyHelper.IsPositionValid(tempGridButton.RightAdjacency.ColumnNumber)));
            adjacencyInfoString.Append("\n");

            adjacencyInfoString.Append(String.Format("Bottom - Row: {0} - Col: {1}", AdjacencyHelper.IsPositionValid(tempGridButton.DownAdjacency.RowNumber), AdjacencyHelper.IsPositionValid(tempGridButton.DownAdjacency.ColumnNumber)));
            adjacencyInfoString.Append("\n");

            adjacencyInfoString.Append(String.Format("Left - Row: {0} - Col: {1}", AdjacencyHelper.IsPositionValid(tempGridButton.LeftAdjacency.RowNumber), AdjacencyHelper.IsPositionValid(tempGridButton.LeftAdjacency.ColumnNumber)));
            adjacencyInfoString.Append("\n");

            return adjacencyInfoString;
        }

        private void HightlightMiddleOfGrid(GridButton[,] gridButtonMultiArray)
        {
            double startingRow = Math.Floor(VariableConstants.SQUARE_SIDE_LENGTH / 2);

            double staringColumn = Math.Floor(VariableConstants.SQUARE_SIDE_LENGTH / 2);

            gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(staringColumn)].Background = Brushes.LightBlue;

            gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(staringColumn)].IsSelected = true;

            //Initialize the previously selected Grid Button.  The center starting Grid Button will always be the initial previously selected one.
            previouslySelectedButton = gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(staringColumn)];
        }

    }
}
