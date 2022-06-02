using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Globalization;

namespace DataBinding.UIControls
{
    /// <summary>
    /// Логика взаимодействия для CustomTreeView.xaml
    /// </summary>
    public partial class CustomTreeView : UserControl
    {
        public CustomTreeView()
        {
            InitializeComponent();
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

            return new Thickness(Length * 1, 0, 0, 0); //item.GetDepth()
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
