using ButtonGrid_Git.DTO;
using ButtonGrid_Git.Constants;
using System;
using System.Windows.Threading;
using System.Windows.Input;

namespace ButtonGrid_Git
{
    public static class KeyBoardHelper
    {

        public static void HandleKeyRight(GridButton currentGridButton, MainWindow mainWindow)
        {
            //Current Button plus one column = new Button

            GridButton nextGridButton;

            if (currentGridButton.gridPosition.ColumnNumber + 1 < VariableConstants.SQUARE_SIDE_LENGTH)
            {
                nextGridButton = mainWindow.gridButtonMultiArray[currentGridButton.gridPosition.RowNumber, currentGridButton.gridPosition.ColumnNumber + 1];

                mainWindow.TransitionPreviouslySelectedButton(nextGridButton);
            }
        }

        public static void HandleKeyLeft(GridButton currentGridButton, MainWindow mainWindow)
        {
            //Current Button minus one column = new Button

            GridButton nextGridButton;

            if (currentGridButton.gridPosition.ColumnNumber - 1 > VariableConstants.INVALID_ADJACENCY)
            {
                nextGridButton = mainWindow.gridButtonMultiArray[currentGridButton.gridPosition.RowNumber, currentGridButton.gridPosition.ColumnNumber - 1];

                mainWindow.TransitionPreviouslySelectedButton(nextGridButton);
            }
        }

        public static void HandleKeyDown(GridButton currentGridButton, MainWindow mainWindow)
        {
            //Current button plus one row = New Button

            GridButton nextGridButton;

            if (currentGridButton.gridPosition.RowNumber + 1 < VariableConstants.SQUARE_SIDE_LENGTH)
            {
                nextGridButton = mainWindow.gridButtonMultiArray[currentGridButton.gridPosition.RowNumber + 1, currentGridButton.gridPosition.ColumnNumber];

                mainWindow.TransitionPreviouslySelectedButton(nextGridButton);
            }
        }

        public static void HandleKeyUp(GridButton currentGridButton, MainWindow mainWindow)
        {
            //Current button minus one row = New button.

            GridButton nextGridButton;

            if (currentGridButton.gridPosition.RowNumber - 1 > VariableConstants.INVALID_ADJACENCY)
            {
                nextGridButton = mainWindow.gridButtonMultiArray[currentGridButton.gridPosition.RowNumber - 1, currentGridButton.gridPosition.ColumnNumber];

                mainWindow.TransitionPreviouslySelectedButton(nextGridButton);
            }
        }

        public static void SetKeyBoardFocusToMiddleGridButton(MainWindow mainWindow)
        {
            mainWindow.Dispatcher.BeginInvoke((Action)delegate
            {
                Keyboard.Focus(mainWindow.gridButtonMultiArray[Convert.ToInt32(mainWindow.StartingRow), Convert.ToInt32(mainWindow.StartingColumn)]);
            }, DispatcherPriority.Render);

            mainWindow.adjacencyInfoTextBlock.Text = mainWindow.BuildAdjacencyInfoDisplayString(mainWindow.gridButtonMultiArray[Convert.ToInt32(mainWindow.StartingRow), Convert.ToInt32(mainWindow.StartingColumn)]).ToString();
        }

    }
}
