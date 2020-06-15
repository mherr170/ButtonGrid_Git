using System;
using System.Collections;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using ButtonGrid_Git.DTO;
using ButtonGrid_Git.Adjacency;
using ButtonGrid_Git.Constants;
using System.Windows.Input;

using System.Windows.Threading;

namespace ButtonGrid_Git
{

    public partial class MainWindow : Window
    {
        GridButton[,] gridButtonMultiArray;

        private readonly TextBlock adjacencyInfoTextBlock;

        private GridButton previouslySelectedButton;

        private double startingRow;
        private double startingColumn;

        public MainWindow()
        {
            gridButtonMultiArray = new GridButton[Convert.ToInt32(VariableConstants.SQUARE_SIDE_LENGTH), Convert.ToInt32(VariableConstants.SQUARE_SIDE_LENGTH)];

            InitializeComponent();

            StackPanel mainStackPanel = Init_StackPanel();

            //Add a Text Block to display the adjacency information for any button that you click.
            adjacencyInfoTextBlock = Init_AdjacencyInfoTextBlock();
            
            Grid gridPanel = Init_GridPanel();

            HightlightMiddleOfGrid();

            mainStackPanel.Children.Add(adjacencyInfoTextBlock);
            mainStackPanel.Children.Add(gridPanel);

            SetKeyBoardFocusToMiddleGridButton();

            //Add the Stack Panel with all of our logic into the actual Content 
            Content = mainStackPanel;
        }

        private void SetKeyBoardFocusToMiddleGridButton()
        {
            this.Dispatcher.BeginInvoke((Action)delegate
            {
                Keyboard.Focus(gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(startingColumn)]);
            }, DispatcherPriority.Render);

            adjacencyInfoTextBlock.Text = BuildAdjacencyInfoDisplayString(gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(startingColumn)]).ToString();
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

        private Grid Init_GridPanel()
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

            //I'm not sure what order these get fired, but they appear to all work.
            newGridButton.KeyDown += NewGridButton_KeyDown;
            newGridButton.Click += NewGridButton_Click_DisplayAdjacencyInfo;
            newGridButton.Click += NewGridButton_Click;

            return newGridButton;
        }

        private void NewGridButton_KeyDown(object sender, KeyEventArgs e)
        {
            GridButton currentGridButton = e.Source as GridButton;

            switch (e.Key)
            {
                case Key.Up:
                    HandleKeyUp(currentGridButton);
                    break;
                case Key.Down:
                    HandleKeyDown(currentGridButton);
                    break;
                case Key.Left:
                    HandleKeyLeft(currentGridButton);
                    break;
                case Key.Right:
                    HandleKeyRight(currentGridButton);
                    break;
                default:
                    break;
            }

        }

        private void HandleKeyRight(GridButton currentGridButton)
        {
            GridButton nextGridButton;

            if (currentGridButton.gridPosition.ColumnNumber + 1 < VariableConstants.SQUARE_SIDE_LENGTH)
            {
                nextGridButton = gridButtonMultiArray[currentGridButton.gridPosition.RowNumber, currentGridButton.gridPosition.ColumnNumber + 1];

                TransitionPreviouslySelectedButton(nextGridButton);
            }
        }

        private void HandleKeyLeft(GridButton currentGridButton)
        {
            //Current Button - one column = new Button

            GridButton nextGridButton;

            if (currentGridButton.gridPosition.ColumnNumber - 1 > VariableConstants.INVALID_ADJACENCY)
            {
                nextGridButton = gridButtonMultiArray[currentGridButton.gridPosition.RowNumber, currentGridButton.gridPosition.ColumnNumber - 1];

                TransitionPreviouslySelectedButton(nextGridButton);
            }
        }

        private void HandleKeyDown(GridButton currentGridButton)
        {
            //Current button + one row = New Button

            GridButton nextGridButton;

            if (currentGridButton.gridPosition.RowNumber + 1 < VariableConstants.SQUARE_SIDE_LENGTH)
            {
                nextGridButton = gridButtonMultiArray[currentGridButton.gridPosition.RowNumber + 1, currentGridButton.gridPosition.ColumnNumber];

                TransitionPreviouslySelectedButton(nextGridButton);
            }
        }

        private void HandleKeyUp(GridButton currentGridButton)
        {
            //Current button minus one row = New button.

            GridButton nextGridButton;

            if (currentGridButton.gridPosition.RowNumber - 1 > VariableConstants.INVALID_ADJACENCY)
            {
                nextGridButton = gridButtonMultiArray[currentGridButton.gridPosition.RowNumber - 1, currentGridButton.gridPosition.ColumnNumber];

                TransitionPreviouslySelectedButton(nextGridButton);
            }
        }

        private void TransitionPreviouslySelectedButton(GridButton nextGridButton)
        {
            adjacencyInfoTextBlock.Text = BuildAdjacencyInfoDisplayString(nextGridButton).ToString();

            nextGridButton.Background = Brushes.LightBlue;
            
            previouslySelectedButton.Background = Brushes.Red;

            previouslySelectedButton = nextGridButton;
        }

        private void NewGridButton_Click(object sender, RoutedEventArgs e)
        {
            GridButton clickedGridButton = e.Source as GridButton;

            clickedGridButton.Background = Brushes.LightBlue;

            TransitionPreviouslySelectedButton(clickedGridButton);
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

        private void HightlightMiddleOfGrid()
        {
            FindAndAssignMiddleOfGrid();

            gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(startingColumn)].Background = Brushes.LightBlue;

            gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(startingColumn)].IsSelected = true;

            //Initialize the previously selected Grid Button.  The center starting Grid Button will always be the initial previously selected one.
            previouslySelectedButton = gridButtonMultiArray[Convert.ToInt32(startingRow), Convert.ToInt32(startingColumn)];
        
        }

        private void FindAndAssignMiddleOfGrid()
        {
            startingRow = Math.Floor(VariableConstants.SQUARE_SIDE_LENGTH / 2);

            startingColumn = Math.Floor(VariableConstants.SQUARE_SIDE_LENGTH / 2);
        }

    }
}
