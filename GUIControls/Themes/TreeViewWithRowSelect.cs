using System;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Controls;

namespace GUIControls.Tree
{
    public class TreeViewWithRowSelect : TreeView
    {
        public TreeViewWithRowSelect() :base()
        {

        }

   }

    public static class TreeViewItemExtensions
    {
        public static int GetDepth(this TreeViewItem item)
        {
            FrameworkElement elem = item;
            var parent = VisualTreeHelper.GetParent(item);
            var count = 0;
            while (parent != null && !(parent is TreeView))
            {
                var tvi = parent as TreeViewItem;
                if (parent is TreeViewItem)
                    count++;
                parent = VisualTreeHelper.GetParent(parent);
            }
            return count;
        }
    }

    public class LeftMarginMultiplierConverter : IValueConverter
    {
        public double Length { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return new Thickness(0);

            return new Thickness(Length * item.GetDepth(), 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
	}  	
}