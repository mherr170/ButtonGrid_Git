using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ButtonGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int SQUARE_SIDE_LENGTH = 3;

        public MainWindow()
        {
            InitializeComponent();

            StackPanel mainStackPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Grid gridPanel = Init_GridPanel();

            mainStackPanel.Children.Add(gridPanel);

            Content = mainStackPanel;
        }

        private Grid Init_GridPanel()
        {
            Grid gridPanel = new Grid();

            for (int rowNumber = 0; rowNumber < SQUARE_SIDE_LENGTH; rowNumber++)
            {
                gridPanel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                
                for (int columnNumber = 0; columnNumber < SQUARE_SIDE_LENGTH; columnNumber++)
                {
                    gridPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                    Button b = new Button
                    {
                        Background = Brushes.Red,
                        Content = "Test"

                    };

                    Grid.SetColumn(b, columnNumber);
                    Grid.SetRow(b, rowNumber);

                    gridPanel.Children.Add(b);
                }

            }
      
            return gridPanel;
        }
    }
}
