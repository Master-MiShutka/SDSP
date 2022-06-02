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
    public class DbSettings : PopupControlBase
    {
        #region Constructors

        static DbSettings()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DbSettings), new FrameworkPropertyMetadata(typeof(DbSettings)));
        }

        public DbSettings() :base()
        {

        }

        #endregion //Constructors

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ButtonBase btnOK = GetTemplateChild("btnOK") as ButtonBase;
            ButtonBase btnCancel = GetTemplateChild("btnCancel") as ButtonBase;

            if (btnOK != null)
                btnOK.Click += btnOK_Click;

            if (btnCancel != null)
                btnCancel.Click += btnCancel_Click;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            base.IsOpen = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            base.IsOpen = false;
        }
    }
}
