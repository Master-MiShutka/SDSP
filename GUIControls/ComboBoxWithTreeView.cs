using System;
using System.Windows;
using System.Windows.Controls;

namespace GUIControls.Tree
{
    [TemplatePart(Name = PART_Tree, Type = typeof (TreeViewWithRowSelect))]
    public class ComboBoxWithTreeView : DropDownButton
    {
        private const string PART_Tree = "PART_Tree";
        private TreeViewWithRowSelect _tree;

        #region Constructors

        static ComboBoxWithTreeView()
        {
            /*DefaultStyleKeyProperty.OverrideMetadata(typeof (ComboBoxWithTreeView),
                                                     new FrameworkPropertyMetadata(typeof (ComboBoxWithTreeView)));*/
        }

        #endregion //Constructors

        public TreeViewWithRowSelect Tree
        {
            get
            {
                return _tree;
            }
            set
            {
                if (_tree != null)
                    _tree.SelectedItemChanged -= Tree_SelectedItemChanged;

                _tree = value;

                if (_tree != null)
                    _tree.SelectedItemChanged += Tree_SelectedItemChanged;
            }
        }

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_tree != null)
                _tree.SelectedItemChanged -= Tree_SelectedItemChanged;

            Tree = GetTemplateChild(PART_Tree) as TreeViewWithRowSelect;
        }

        #endregion //Base Class Overrides

        #region Events

        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent("SelectedItemChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ComboBoxWithTreeView));
        public event RoutedEventHandler SelectedItemChanged
        {
            add
            {
                AddHandler(SelectedItemChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedItemChangedEvent, value);
            }
        }

        #endregion

        #region Event Handlers

        private void Tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            base.IsOpen = false;
            TreeViewWithRowSelect trv = sender as TreeViewWithRowSelect;
            TreeViewItem trvItem = (TreeViewItem) e.NewValue;
            
            if (trvItem == null) throw new ArgumentNullException();
            this.Text = trvItem.Header.ToString();
            //
            RaiseRoutedEvent(SelectedItemChangedEvent);
        }

        #endregion

        /// <summary>
        /// Raises routed events.
        /// </summary>
        private void RaiseRoutedEvent(RoutedEvent routedEvent)
        {
            RoutedEventArgs args = new RoutedEventArgs(routedEvent, this);
            RaiseEvent(args);
        }
    }

}
