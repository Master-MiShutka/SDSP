using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Shared.Report
{
    public partial class ReportExporter
    {
        public static void ReportToGrid(Report report, ref Grid grid)
        {
            if (report == null) throw new ArgumentNullException("Report is null");
            if (grid == null) throw new ArgumentNullException("Grid is null");
            
            // очищаем сетку
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            // задаем количество и ширину столбцов
            if (report.ColumnDefinition == null) throw new ArgumentNullException("Reports ColumnDefinition is null");

            foreach (ColumnDefinition colDef in report.ColumnDefinition)
            {
                grid.ColumnDefinitions.Add(colDef);
            }

            // задаем количество и высоту строк
            if (report.RowDefinition == null) throw new ArgumentNullException("Reports RowDefinition is null");

            foreach (RowDefinition rowDef in report.RowDefinition)
            {
                grid.RowDefinitions.Add(rowDef);
            }

            //
            LoadStyles();

            // заголовок
            AddCellToReport(ref grid, report.Header, 0, 0, 1, report.ColumnDefinition.Count, new Thickness(0), ReportItemStyle.TableHeaderCellStyle);

            // ячейки
            foreach (ReportItem item in report.Items)
            {
               AddCellToReport(ref grid, item.Caption, item.Row, item.Column, item.RowSpan, item.ColumnSpan, item.Border, item.Style);
            }
        }

        private static Uri uri = new Uri("/Shared;component/Report/Styles.xaml", UriKind.Relative);
        private static ResourceDictionary resDictStyles;

        private static void LoadStyles()
        {
            resDictStyles = Application.LoadComponent(uri) as ResourceDictionary;
        }

        #region Добавление строк в сетку

        private void AddRowDefinitionToTable(ref Grid grid, float[] rowHeight)
        {
            // добавляем строки
            foreach (float rh in rowHeight)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(rh);
                grid.RowDefinitions.Add(rowDefinition);
            }
        }

        private void AddRowDefinitionToTable(ref Grid grid, float rowHeight, int count)
        {
            for (int index = 1; index <= count; index++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(rowHeight);
                grid.RowDefinitions.Add(rowDefinition);
            }
        }

        private void AddRowDefinitionToTable(ref Grid grid, int count)
        {
            for (int index = 1; index <= count; index++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Auto);
                grid.RowDefinitions.Add(rowDefinition);
            }
        }

        private void AddRowDefinitionToTable(ref Grid grid, float rowHeight)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(rowHeight);
            grid.RowDefinitions.Add(rowDefinition);
        }

        #endregion

        private static void AddCellToReport(ref Grid grid,
                                    string content,
                                    int row, int column,
                                    int rowSpan, int columnSpan,
                                    Thickness borders,
                                    ReportItemStyle cellStyleName)
        {
            if (grid.RowDefinitions.Count < row) throw new ArgumentException("Указанный номер строки больше количества строк в сетке");
            if (grid.ColumnDefinitions.Count < column) throw new ArgumentException("Указанный номер столбца больше количества столбцов в сетке");

            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = borders;
            border.Margin = new Thickness(0);
            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            border.VerticalAlignment = VerticalAlignment.Stretch;

            //
            TextBox textBox = new TextBox()
            {
                Text = content,
                Style = cellStyleName.ToTextBoxStyle(resDictStyles),
                ToolTip = new TextBlock(new Run(content)) { FontSize = 11 * (96.0 / 72.0) },
                IsReadOnly = true,
                BorderThickness = new Thickness(0)
            };

            border.Background = textBox.Background;
            border.Child = textBox;

            Grid.SetRow(border, row);
            Grid.SetColumn(border, column);
            Grid.SetRowSpan(border, rowSpan);
            Grid.SetColumnSpan(border, columnSpan);
            //
            grid.Children.Add(border);
        }
    }

    public static class ReportItemStyleExtensions
    {
        public static Style ToTextBoxStyle(this ReportItemStyle me, ResourceDictionary resDict)
        {
            if (resDict == null) return new Style();
            return (Style)resDict[me.ToString()];
        }
    }
}
