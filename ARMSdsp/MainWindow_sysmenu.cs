using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using Shared.WinAPI;

namespace SDSP
{
    public class Theme
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
    }
    
    public partial class MainWindow : Window
    {
        #region Themes
            private Theme[] themesList = new Theme[6]
            {			    
                new Theme() { ShortName ="Aero", FullName = "/PresentationFramework.Aero, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, ProcessorArchitecture=MSIL;component/themes/aero.normalcolor.xaml"},
                new Theme() { ShortName ="AeroLite", FullName = "/PresentationFramework.AeroLite, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, ProcessorArchitecture=MSIL;component/themes/aerolite.normalcolor.xaml"},
                new Theme() { ShortName ="Luna normalcolor", FullName = "/PresentationFramework.Luna, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, ProcessorArchitecture=MSIL;component/themes/luna.normalcolor.xaml"},
                new Theme() { ShortName ="Luna homestead", FullName = "/PresentationFramework.Luna, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, ProcessorArchitecture=MSIL;component/themes/luna.homestead.xaml"},
                new Theme() { ShortName ="Luna metallic", FullName = "/PresentationFramework.Luna, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, ProcessorArchitecture=MSIL;component/themes/luna.metallic.xaml"},
                new Theme() { ShortName ="Royale", FullName = "/PresentationFramework.Royale, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, ProcessorArchitecture=MSIL;component/themes/royale.normalcolor.xaml"}
            };

            private Theme _selectTheme;
            public Theme SelectedTheme
            {
                get { return _selectTheme; }
                set
                {
                    _selectTheme = value;
                    OnChanged("SelectedTheme");
                    ChangeTheme(_selectTheme);
                }
            }

            private void ChangeTheme(Theme _SelectTheme)
            {
                this.LoadTheme(_SelectTheme.FullName);                
            }

            private ObservableCollection<Theme> _themes;
            public ObservableCollection<Theme> Themes
            {
                get { return _themes; }
                set { _themes = value; OnChanged("Themes"); }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnChanged(string name)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        #endregion

        // ID constants
        private const Int32 m_baseID = 1001;

        public void LoadTheme(string themePath)
        {
            // очищаем перед загрузкой темы
            this.Resources.MergedDictionaries.Clear();
            // загружаем необходимые для работы ресурсы
            //this.Resources.Source = new Uri("/GUIControls;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute);
            // загружаем тему
            try
            {
                this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(themePath, UriKind.RelativeOrAbsolute) });
                this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/GUIControls;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute) });
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось применить тему.", App.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void MakeMenu()
        {
            if (Themes == null)
                Themes = new ObservableCollection<Theme>();
            foreach (var themeItem in themesList)
                Themes.Add(themeItem);

            if (Directory.Exists("Themes"))
            {
                FileInfo[] localthemes = new System.IO.DirectoryInfo("Themes").GetFiles();
                foreach (var item in localthemes)
                {
                    Themes.Add(new Theme { ShortName = item.Name, FullName = item.FullName });
                }
            }

            //Create a new submenu structure
            IntPtr hMenu = SystemMenu.AddSysMenuSubMenu();
            if (hMenu != IntPtr.Zero)
            {
                // Build submenu items of hMenu
                uint index = 0;
                for (int i = 0; i < Themes.Count; i++)
                {
                    SystemMenu.AddSysMenuSubItem(Themes[i].ShortName, index, m_baseID + index, hMenu);
                    index++;
                }
                // Now add to main system menu (position 6)
                SystemMenu.AddSysMenuItem("Визуальная тема", 0, 6, hMenu);
                SystemMenu.AddSysMenuItem("-", 0, 7, IntPtr.Zero);
            }

            SelectedTheme = Themes[0];

            // Attach our WndProc handler to this Window
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            source.AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Check if a System Command has been executed
            if (msg == (int) SystemMenu.WindowMessages.wmSysCommand)
            {               
                int menuID = wParam.ToInt32();

                if (menuID <= (m_baseID + this.Themes.Count()))
                    if (menuID >= m_baseID)
                        this.SelectedTheme = Themes[menuID-m_baseID];
            }

            return IntPtr.Zero;
        }
    }
}
