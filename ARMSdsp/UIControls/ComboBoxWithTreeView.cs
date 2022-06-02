using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace DataBinding.UIControls
{
    
    [TemplatePart(Name = "PART_Tree", Type = typeof(Selector))]
    public class ComboBoxWithTreeView : Control, INotifyPropertyChanged
    {
        public event TextChangedEventHandler TextChanged;

        #region const
        const string TreeViewItemsPropertyName = "TreeItems";
        const string SelectedItemPropertyName = "SelectedItem";
        const string WatermarkPropertyName = "Watermark";
        #endregion

        private Selector treeView;

        static ComboBoxWithTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ComboBoxWithTreeView), new FrameworkPropertyMetadata(typeof(ComboBoxWithTreeView)
                    ));
        }


        public ComboBoxWithTreeView()
        {
            Background = Brushes.WhiteSmoke;
        }

        #region properties and events

        public ItemCollection TreeItems
        {
            get { return (ItemCollection)GetValue(TreeViewItemsProperty); }
            set
            {
                SetValue(TreeViewItemsProperty, value);
            }
        }
        public static readonly DependencyProperty TreeViewItemsProperty =
                DependencyProperty.Register(TreeViewItemsPropertyName,
                                            typeof(string),
                                            typeof(ItemCollection),
                                            new UIPropertyMetadata(null));

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(SelectedItemPropertyName,
                                        typeof(string),
                                        typeof(object),
                                        new UIPropertyMetadata(null));

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set
            {
                SetValue(WatermarkProperty, value);
            }
        }
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register(WatermarkPropertyName,
                                        typeof(string),
                                        typeof(ComboBoxWithTreeView),
                                        new UIPropertyMetadata(string.Empty));

        #endregion


        private void Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trv = sender as TreeView;
            var trvItem = trv.SelectedItem as TreeViewItem;
            //if (trvItem.Items.Count != 0) return;
            //_header.Text = trvItem.Header.ToString();
            //_popup.IsOpen = false;
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Event raised when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event
        /// </summary>
        /// <param name="e">The arguments to pass</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion

        #region Overrides of control

        /// <summary>
        /// override to get the templated controls
        /// </summary>
        public override void OnApplyTemplate()
        {
            //Focus the popup if it exists in the Control template
            Popup popup = GetTemplateChild("Popup") as Popup;
            if (popup != null)
            {
                popup.Opened += delegate
                {
                    popup.Focus();
                };
            }

            treeView = GetTemplateChild("PART_Dates") as Selector;

        }

        #endregion

    }

    public class ShadowConverter : IMultiValueConverter
    {
        #region Implementation of IMultiValueConverter

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (string)values[0];
            return text == string.Empty
                       ? Visibility.Visible
                       : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[0];
        }

        #endregion
    }


    /// <summary>
    /// конвертер для получения строкового представления выбранного элемента в дереве
    /// </summary>
    public class TreeSelectedItemConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "выберите";

            TreeViewItem treeViewItem = (TreeViewItem)value;

            return treeViewItem.Header;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("This is a one-way value converter. ConvertBack method is not supported.");
        }

        #endregion
    }
}
