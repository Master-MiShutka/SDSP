﻿#pragma checksum "..\..\..\..\UIControls\ComboBoxWithTreeView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "33A66793BE4237E9574C540F9ABFA87B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18034
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using DataBinding.UIControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DataBinding.UIControls {
    
    
    /// <summary>
    /// ComboBoxWithTreeView
    /// </summary>
    public partial class ComboBoxWithTreeView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 7 "..\..\..\..\UIControls\ComboBoxWithTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DataBinding.UIControls.ComboBoxWithTreeView Root;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DataBinding;component/uicontrols/comboboxwithtreeview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UIControls\ComboBoxWithTreeView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Root = ((DataBinding.UIControls.ComboBoxWithTreeView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            
            #line 35 "..\..\..\..\UIControls\ComboBoxWithTreeView.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Header_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\..\UIControls\ComboBoxWithTreeView.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.textBox_TextChanged);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 77 "..\..\..\..\UIControls\ComboBoxWithTreeView.xaml"
            ((System.Windows.Controls.Primitives.Popup)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Popup_MouseLeave);
            
            #line default
            #line hidden
            
            #line 78 "..\..\..\..\UIControls\ComboBoxWithTreeView.xaml"
            ((System.Windows.Controls.Primitives.Popup)(target)).Opened += new System.EventHandler(this.Popup_Opened);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 79 "..\..\..\..\UIControls\ComboBoxWithTreeView.xaml"
            ((System.Windows.Controls.TreeView)(target)).Initialized += new System.EventHandler(this.Tree_Initialized);
            
            #line default
            #line hidden
            
            #line 79 "..\..\..\..\UIControls\ComboBoxWithTreeView.xaml"
            ((System.Windows.Controls.TreeView)(target)).SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.Tree_SelectedItemChanged);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
