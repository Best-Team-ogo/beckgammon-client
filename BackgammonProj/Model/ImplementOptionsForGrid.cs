using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ViewModel.Models
{
    public static class ImplementOptionsForGrid
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

        public static T GetChildOfType<T>(this DependencyObject depObj)
           where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}
