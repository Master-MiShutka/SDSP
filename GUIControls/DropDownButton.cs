using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using GUIControls.Core.Utilities;

namespace GUIControls
{
    [TemplatePart(Name = PART_DropDownButton, Type = typeof(ToggleButton))]
    [TemplatePart(Name = PART_ContentPresenter, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = PART_Popup, Type = typeof(Popup))]
    public class DropDownButton : PopupControlBase
    {
        private const string PART_DropDownButton = "PART_DropDownButton";
        private const string PART_ContentPresenter = "PART_ContentPresenter";
        private const string PART_Popup = "PART_Popup";
        
        #region Constructors

        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }

        public DropDownButton() :base()
        {
            
        }

        #endregion //Constructors

        #region Members

        private ContentPresenter _contentPresenter;
        private Popup _popup;

        #endregion

        #region Properties

        private System.Windows.Controls.Primitives.ButtonBase _button;
        protected System.Windows.Controls.Primitives.ButtonBase Button
        {
            get
            {
                return _button;
            }
            set
            {
                if (_button != null)
                    _button.Click -= DropDownButton_Click;

                _button = value;

                if (_button != null)
                    _button.Click += DropDownButton_Click;
            }
        }

        #endregion //Properties

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Button = GetTemplateChild(PART_DropDownButton) as ToggleButton;

            _contentPresenter = GetTemplateChild(PART_ContentPresenter) as ContentPresenter;

            if (_popup != null)
                _popup.Opened -= Popup_Opened;

            _popup = GetTemplateChild(PART_Popup) as Popup;

            if (_popup != null)
                _popup.Opened += Popup_Opened;
        }

        #endregion //Base Class Overrides

        #region Event Handlers

        private void DropDownButton_Click(object sender, RoutedEventArgs e)
        {
            OnClick();
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            // Set the focus on the content of the ContentPresenter.
            if (_contentPresenter != null)
            {
                if (_contentPresenter.Content == null) return;
                DependencyObject o = _contentPresenter.Content as DependencyObject;
                while (o != null && (VisualTreeHelper.GetChildrenCount(o) > 0))
                {
                    if (o is UIElement)
                    {
                        if (((UIElement)o).Focusable)
                            break;
                    }
                    o = VisualTreeHelper.GetChild(o, 0);
                }

                ((UIElement)o).Focus();
            }
        }

        #endregion
    }
}
