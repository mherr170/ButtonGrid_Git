using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ButtonGrid_Git.DTO;
using ButtonGrid_Git.Utility.Adjacency;
using ButtonGrid_Git.Constants;
using System.Windows.Input;
using ButtonGrid_Git.TerrainGeneration;
using ButtonGrid_Git.Enum;

namespace ButtonGrid_Git
{

    public partial class MainWindow : Window
    {
        public GridButton[,] gridButtonMultiArray;

        public GridButton previouslySelectedButton;

        public double StartingRow { get; set; }
        public double StartingColumn { get; set; }

        public MainWindow()
        {
            gridButtonMultiArray = new GridButton[Convert.ToInt32(VariableConstants.SQUARE_SIDE_LENGTH), Convert.ToInt32(VariableConstants.SQUARE_SIDE_LENGTH)];

            InitializeComponent();
             
            Grid gridPanel = Init_GridPanel();

            //mainStackPanel.Children.Add(adjacencyInfoTextBlock);
            WPFButtonGrid.HorizontalAlignment = HorizontalAlignment.Center;
            WPFButtonGrid.VerticalAlignment = VerticalAlignment.Center;
            WPFButtonGrid.Children.Add(gridPanel);

            //Get hooks into the terrain generation
            InitTerrainGeneration(gridButtonMultiArray);

            HightlightMiddleOfGrid();
            Utility.Keyboard.KeyBoardHelper.SetKeyBoardFocusToMiddleGridButton(this);
        }

        private void InitTerrainGeneration(GridButton[,] gridButtonMultiArray)
        {
            GenerateTerrain gridTerrain = new GenerateTerrain(gridButtonMultiArray);
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

        private GridButton Init_SingleGridButton(int rowNumber, int columnNumber)
        {
            GridButton newGridButton = new GridButton(rowNumber, columnNumber)
            {
                Foreground = Brushes.PaleGoldenrod
            };

            newGridButton.Content = newGridButton.gridPosition.RowNumber + ", " + newGridButton.gridPosition.ColumnNumber;

            SetGridButtonEventHandlers(newGridButton);

            return newGridButton;
        }

        private void SetGridButtonEventHandlers(GridButton newGridButton)
        {
            //I'm not sure what order these get fired, but they appear to all work.
            newGridButton.KeyDown += NewGridButton_KeyDown;
            newGridButton.Click += NewGridButton_Click;
        }

        private void NewGridButton_KeyDown(object sender, KeyEventArgs e)
        {
            GridButton currentGridButton = e.Source as GridButton;
            GridButton nextGridButton = null;

            switch (e.Key)
            {
                case Key.Up:
                    
                    nextGridButton = Utility.Keyboard.KeyBoardHelper.HandleKeyUp(currentGridButton, this);

                    break;
                case Key.Down:

                    nextGridButton = Utility.Keyboard.KeyBoardHelper.HandleKeyDown(currentGridButton, this);

                    break;
                case Key.Left:

                    nextGridButton = Utility.Keyboard.KeyBoardHelper.HandleKeyLeft(currentGridButton, this);

                    break;
                case Key.Right:

                    nextGridButton = Utility.Keyboard.KeyBoardHelper.HandleKeyRight(currentGridButton, this);

                    break;
                default:
                    break;
            }

            if (nextGridButton != null)
            {
                TransitionPreviouslySelectedButton(nextGridButton);
            }

        }

        private void NewGridButton_Click(object sender, RoutedEventArgs e)
        {
            GridButton clickedGridButton = e.Source as GridButton;

            clickedGridButton.Background = Brushes.LightBlue;

            TransitionPreviouslySelectedButton(clickedGridButton);
        }

        private void HightlightMiddleOfGrid()
        {
            FindAndAssignMiddleOfGrid();

            gridButtonMultiArray[Convert.ToInt32(StartingRow), Convert.ToInt32(StartingColumn)].Background = Brushes.LightBlue;

            gridButtonMultiArray[Convert.ToInt32(StartingRow), Convert.ToInt32(StartingColumn)].IsSelected = true;

            //Initialize the previously selected Grid Button.  The center starting Grid Button will always be the initial previously selected one.
            previouslySelectedButton = gridButtonMultiArray[Convert.ToInt32(StartingRow), Convert.ToInt32(StartingColumn)];
        
        }

        public void TransitionPreviouslySelectedButton(GridButton nextGridButton)
        {
            nextGridButton.Background = Brushes.LightBlue;

            previouslySelectedButton.Background = TerrainTypeEnum.terrainTypeDictionary[previouslySelectedButton.GridButtonTerrainType];

            previouslySelectedButton = nextGridButton;
        }

        private void FindAndAssignMiddleOfGrid()
        {
            StartingRow = Math.Floor(VariableConstants.SQUARE_SIDE_LENGTH / 2);

            StartingColumn = Math.Floor(VariableConstants.SQUARE_SIDE_LENGTH / 2);
        }

    }
}
