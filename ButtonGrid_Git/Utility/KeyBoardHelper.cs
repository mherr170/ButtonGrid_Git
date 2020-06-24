using ButtonGrid_Git.DTO;
using ButtonGrid_Git.Constants;
using System;
using System.Windows.Threading;

namespace ButtonGrid_Git.Utility.Keyboard
{
    public static class KeyBoardHelper
    {
        public static GridButton HandleKeyUp(GridButton currentGridButton, MainWindow mainWindow)
        {
            //Current button minus one row = New button.
            if (currentGridButton.gridPosition.RowNumber - 1 > VariableConstants.INVALID_ADJACENCY)
            {
               return mainWindow.gridButtonMultiArray[currentGridButton.gridPosition.RowNumber - 1, currentGridButton.gridPosition.ColumnNumber];
            }

            return null;
        }

        public static GridButton HandleKeyDown(GridButton currentGridButton, MainWindow mainWindow)
        {
            //Current button plus one row = New Button
            if (currentGridButton.gridPosition.RowNumber + 1 < VariableConstants.SQUARE_SIDE_LENGTH)
            {
                return mainWindow.gridButtonMultiArray[currentGridButton.gridPosition.RowNumber + 1, currentGridButton.gridPosition.ColumnNumber];
            }

            return null;
        }

        public static GridButton HandleKeyLeft(GridButton currentGridButton, MainWindow mainWindow)
        {
            //Current Button minus one column = new Button
            if (currentGridButton.gridPosition.ColumnNumber - 1 > VariableConstants.INVALID_ADJACENCY)
            {
                return mainWindow.gridButtonMultiArray[currentGridButton.gridPosition.RowNumber, currentGridButton.gridPosition.ColumnNumber - 1];
            }

            return null;
        }

        public static GridButton HandleKeyRight(GridButton currentGridButton, MainWindow mainWindow)
        {
            //Current Button plus one column = new Button
            if (currentGridButton.gridPosition.ColumnNumber + 1 < VariableConstants.SQUARE_SIDE_LENGTH)
            {
                return mainWindow.gridButtonMultiArray[currentGridButton.gridPosition.RowNumber, currentGridButton.gridPosition.ColumnNumber + 1];
            }

            return null;
        }

        public static void SetKeyBoardFocusToMiddleGridButton(MainWindow mainWindow)
        {
            mainWindow.Dispatcher.BeginInvoke((Action)delegate
            {
                System.Windows.Input.Keyboard.Focus(mainWindow.gridButtonMultiArray[Convert.ToInt32(mainWindow.StartingRow), Convert.ToInt32(mainWindow.StartingColumn)]);
            }, DispatcherPriority.Render);
        }

    }
}
