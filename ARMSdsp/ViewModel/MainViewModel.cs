using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

using Utils.Shared.DataProviders;
using Utils.Shared.Commands;

namespace DataBinding.ViewModel
{
    public class MainViewModel
    {
        private ICommand _commandConnectToDB;

        private ICommand _commandExportData;

        private ReadOnlyCollection<string> _items;

        public ReadOnlyCollection<string> ProfilesList
        {
            get
            {
                if (_items == null)
                {
                    _items = AsyncDataProvider.GetItems();
                }

                return _items;
            }
        }

        public ICommand CommandConnectToDB
        {
            get
            {
                if (_commandConnectToDB == null)
                {
                    _commandConnectToDB = new RelayCommand(p => OnConnectToDB());
                }

                return _commandConnectToDB;
            }
        }

        public ICommand CommandExportData
        {
            get
            {
                if (_commandExportData == null)
                {
                    _commandExportData = new RelayCommand(p => OnExportData(p), p => CanExportData);
                }

                return _commandExportData;
            }
        }

        public string SelectedItem
        {
            get;
            set;
        }

        private void OnConnectToDB()
        {
            
        }

        private void OnExportData(object parameter)
        {
            MessageBox.Show(String.Format("Edtiting item: {0}",
                parameter != null ? parameter.ToString() : "Not selected"));
        }

        private bool CanExportData
        {
            get
            {
                return true;
            }
        }
    }
}
