using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ViewModel.Models
{
    public class ImplementOptionsForGrid
    {
        public static void CreateCoulmnsForGrid(int num,Grid grid)
        {
            for (int i = 0; i < num; i++)
            {
                var rowDefinition = new ColumnDefinition();
                grid.ColumnDefinitions.Add(rowDefinition);
            }
        }

        public static void CreateRowsForGrid(int num, Grid grid, GridLength? rowHeight)
        {
            for (int i = 0; i < num; i++)
            {

                var rowDefinition = new RowDefinition();
                if(rowHeight.HasValue)
                rowDefinition.Height = rowHeight.Value;
                grid.RowDefinitions.Add(rowDefinition);
            }
        }

        public static void AddToGrid(UIElement element, int row, int col, Grid grid)
        {
            element.SetValue(Grid.RowProperty, row);
            element.SetValue(Grid.ColumnProperty, col);
            grid.Children.Add(element);
        }
    }
}
