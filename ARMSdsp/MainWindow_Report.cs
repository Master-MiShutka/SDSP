using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

using SDSPServiceInterface.Entities;

using Shared.Report;

namespace SDSP
{     
    /// <summary>
    /// Создание таблицы-отчета
    /// </summary>
    public partial class MainWindow : Window
    {
        private Report report;

        private void CreateReport(SdspInformation sdspInformation)
        {
            if (sdspInformation == null) throw new ArgumentNullException("SdspInformation is null");

            report = new Report();

            report.CreationDateTime = sdspInformation.ReportingDate;

            // название
            report.Header = String.Format("Отчет по результатам опроса объектов, оборудованных СДСП, в {0}\nна {1} г.", sdspInformation.Departament.Name, sdspInformation.ReportingDate.ToShortDateString());
            // описание таблицы
            const int _columnCount = 14;
            double[] col_w = new double[_columnCount] { 0.0, -1.0,  95.0,  85.0,
                                              80.0,  50.0,  0.0,  0.0,  60.0,
                                              90.0,   0.0,   0.0,   0.0,   0.0};
            // Создаем столбцы и добавляем их в таблицу
            for (int index = 0; index < _columnCount; index++)
            {
                if (col_w[index] < 0)
                {
                    report.ColumnDefinition.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }
                if (col_w[index] == 0)
                {
                    report.ColumnDefinition.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                }
                if (col_w[index] > 0)
                {
                    report.ColumnDefinition.Add(new ColumnDefinition() { Width = new GridLength(col_w[index], GridUnitType.Pixel) });
                }
            }

            Thickness border;
            int rowIndex = 1;

            // Строки: 1-я название таблицы; 2-5 шапка таблицы
            AddRowDefinitionToTable(5);
            report.RowDefinition.Add(new RowDefinition() { Height = new GridLength(18, GridUnitType.Pixel) });
            // создаем шапку таблицы
            // 1-я строка
            report.Items.Add(AddCellToReport("№\nп/п", rowIndex, 0, 4, 1, new Thickness(2.0, 2.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Расчетная точка", rowIndex, 1, 3, 1, new Thickness(1.0, 2.0, 0.0, 1.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Номер телефона модема", rowIndex, 2, 3, 1, new Thickness(1.0, 2.0, 0.0, 1.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Статус модема", rowIndex, 3, 4, 1, new Thickness(1.0, 2.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Последний сеанс", rowIndex, 4, 4, 1, new Thickness(1.0, 2.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Данные счетчика", rowIndex, 5, 1, 9, new Thickness(1.0, 2.0, 2.0, 1.0), ReportItemStyle.HeaderCellStyle));

            rowIndex++;
            // 2-я строка
            report.Items.Add(AddCellToReport("Присоединение", rowIndex, 5, 3, 1, new Thickness(1.0, 0.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Тип\nсчетчика", rowIndex, 6, 3, 1, new Thickness(1.0, 0.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Заводской\nномер", rowIndex, 7, 3, 1, new Thickness(1.0, 0.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Сетевой\nадрес", rowIndex, 8, 3, 1, new Thickness(1.0, 0.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Состояние", rowIndex, 9, 3, 1, new Thickness(1.0, 0.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("показания периода", rowIndex, 10, 1, 2, new Thickness(1.0, 0.0, 0.0, 0.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("Разность показаний", rowIndex, 12, 2, 2, new Thickness(1.0, 0.0, 2.0, 0.0), ReportItemStyle.HeaderCellStyle));

            rowIndex++;
            // 3-я строка
            report.Items.Add(AddCellToReport("начало", rowIndex, 10, 1, 1, new Thickness(1.0, 1.0, 0.0, 0.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("окончание", rowIndex, 11, 1, 1, new Thickness(1.0, 1.0, 0.0, 0.0), ReportItemStyle.HeaderCellStyle));

            rowIndex++;
            // 3-я строка
            report.Items.Add(AddCellToReport("ВЛ-10 кВ, номер ТП", rowIndex, 1, 1, 2, new Thickness(1.0, 0.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport(sdspInformation.StartDate.ToString("dd.MM.yyyy"), rowIndex, 10, 1, 1, new Thickness(1.0, 1.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport(sdspInformation.EndDate.ToString("dd.MM.yyyy"), rowIndex, 11, 1, 1, new Thickness(1.0, 1.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("отчетный", rowIndex, 12, 1, 1, new Thickness(1.0, 1.0, 0.0, 2.0), ReportItemStyle.HeaderCellStyle));
            report.Items.Add(AddCellToReport("предыдущий", rowIndex, 13, 1, 1, new Thickness(1.0, 1.0, 2.0, 2.0), ReportItemStyle.HeaderCellStyle));

            rowIndex++;
            // шестая строка номера колонок
            for (int index = 1; index <= _columnCount; index++)
            {
                Thickness borderThickness = new Thickness(1.0, 0.0, 0.0, 2.0);
                if (index == 1) borderThickness = new Thickness(2.0, 0.0, 0.0, 2.0);
                if (index == 14) borderThickness = new Thickness(1.0, 0.0, 2.0, 2.0);
                report.Items.Add(AddCellToReport(index.ToString(), rowIndex, index - 1, 1, 1, borderThickness, ReportItemStyle.HeaderNumbersCellStyle));
            }

            // содержимое таблицы
            // свод
            // добавляем строки
            AddRowDefinitionToTable(new float[4] { 18.0f, 18.0f, 18.0f, 20.0f });
            rowIndex++;
            border = new Thickness(2.0, 0.0, 0.0, 0.0);

            // данные
            report.Items.Add(AddCellToReport("Всего систем", rowIndex, 0, 1, 2, border, ReportItemStyle.StringSummaryCellStyle));
            report.Items.Add(AddCellToReport("Успешно опрошено систем", rowIndex + 1, 0, 1, 2, border, ReportItemStyle.StringSummaryCellStyle));
            report.Items.Add(AddCellToReport("Не опрошено систем", rowIndex + 2, 0, 1, 2, border, ReportItemStyle.StringSummaryCellStyle));
            report.Items.Add(AddCellToReport("Процент опроса", rowIndex + 3, 0, 1, 2, new Thickness(2.0, 0.0, 0.0, 2.0), ReportItemStyle.StringSummaryCellStyle));

            border = new Thickness(0.0, 0.0, 0.0, 0.0);

            report.Items.Add(AddCellToReport(String.Format(" {0} шт.", sdspInformation.ModemsCount), rowIndex, 2, 1, 1, border, ReportItemStyle.IntSummaryCellStyle));
            report.Items.Add(AddCellToReport(String.Format(" {0} шт.", sdspInformation.AnsweringModemsCount), rowIndex + 1, 2, 1, 1, border, ReportItemStyle.IntSummaryCellStyle));
            report.Items.Add(AddCellToReport(String.Format(" {0} шт.", sdspInformation.NotAnsweringModemsCount), rowIndex + 2, 2, 1, 1, border, ReportItemStyle.IntSummaryCellStyle));
            report.Items.Add(AddCellToReport(String.Format(" {0}%", 100 * sdspInformation.AnsweringModemsCount / sdspInformation.ModemsCount),
                                                    rowIndex + 3, 2, 1, 1, new Thickness(0.0, 0.0, 0.0, 2.0), ReportItemStyle.IntSummaryCellStyle));

            border = new Thickness(0.0, 0.0, 0.0, 0.0);

            report.Items.Add(AddCellToReport("Всего учётов", rowIndex, 3, 1, 3, border, ReportItemStyle.StringSummaryCellStyle));
            report.Items.Add(AddCellToReport("Успешно опрошено учётов", rowIndex + 1, 3, 1, 3, border, ReportItemStyle.StringSummaryCellStyle));
            report.Items.Add(AddCellToReport("Не опрошено учётов", rowIndex + 2, 3, 1, 3, border, ReportItemStyle.StringSummaryCellStyle));
            report.Items.Add(AddCellToReport("Процент опроса", rowIndex + 3, 3, 1, 3, new Thickness(0.0, 0.0, 0.0, 2.0), ReportItemStyle.StringSummaryCellStyle));

            border = new Thickness(0.0, 0.0, 2.0, 0.0);

            report.Items.Add(AddCellToReport(String.Format(" {0} шт.", sdspInformation.CountersCount), rowIndex, 6, 1, 8, border, ReportItemStyle.IntSummaryCellStyle));
            report.Items.Add(AddCellToReport(String.Format(" {0} шт.", sdspInformation.AnsweringCountersCount), rowIndex + 1, 6, 1, 8, border, ReportItemStyle.IntSummaryCellStyle));
            report.Items.Add(AddCellToReport(String.Format(" {0} шт.", sdspInformation.NotAnsweringCountersCount), rowIndex + 2, 6, 1, 8, border, ReportItemStyle.IntSummaryCellStyle));
            report.Items.Add(AddCellToReport(String.Format(" {0}%", 100 * sdspInformation.AnsweringCountersCount / sdspInformation.CountersCount),
                                                    rowIndex + 3, 6, 1, 8, new Thickness(0.0, 0.0, 2.0, 2.0), ReportItemStyle.IntSummaryCellStyle));

            // получаем список систем
            SdspContainer[] containers = (sdspInformation.SdspContainers as SdspContainer[]) ?? sdspInformation.SdspContainers.ToArray<SdspContainer>();
            // получаем список счётчиков
            IEnumerable<Counter> allCountersFromContainers = this.GetAllCountersFromContainers(containers);
            Counter[] counters = (allCountersFromContainers as Counter[]) ?? allCountersFromContainers.ToArray<Counter>();

            //
            string indications;
            ReportItemStyle style;


            rowIndex += 4;
            // цикл по всем шкафам - системам
            for (int index = 0; index < containers.Count(); index++)
            {
                // количество счетчиков в текущем шкафу-СДСП
                int counters_in_container = containers[index].Counters.Count<Counter>();

                // число строк для объединения для модема
                int rows_to_span = counters_in_container <= 2 ? 2 : counters_in_container;

                // добавляем строки
                AddRowDefinitionToTable(rows_to_span);

                border = new Thickness(1.0, 0.0, 0.0, 2.0);

                report.Items.Add(AddCellToReport(containers[index].OrderNumber.ToString(), rowIndex, 0, rows_to_span, 1, new Thickness(2.0, 0.0, 0.0, 2.0), ReportItemStyle.StringCellStyle));
                report.Items.Add(AddCellToReport(containers[index].Street, rowIndex, 1, rows_to_span - 1, 1, new Thickness(1.0, 0.0, 0.0, 1.0), ReportItemStyle.StringCellStyle));
                report.Items.Add(AddCellToReport(containers[index].TPName, rowIndex + rows_to_span - 1, 1, 1, 2, border, ReportItemStyle.StringCellStyle));
                report.Items.Add(AddCellToReport(containers[index].DialNumber, rowIndex, 2, rows_to_span - 1, 1, new Thickness(1.0, 0.0, 0.0, 1.0), ReportItemStyle.StringCellStyle));
                report.Items.Add(AddCellToReport(containers[index].Status, rowIndex, 3, rows_to_span, 1, border, ReportItemStyle.StringCellStyle));
                report.Items.Add(AddCellToReport(ReportHelper.DateToString(containers[index].LastSession), rowIndex, 4, rows_to_span, 1, border, ReportItemStyle.StringCellStyle));

                // счетчики
                // число строк для объединения, если счетчик один
                if (counters_in_container == 1)
                {
                    rows_to_span = 2;
                    border = new Thickness(1.0, 0.0, 0.0, 2.0);
                }
                else
                {
                    rows_to_span = 1;
                    border = new Thickness(1.0, 0.0, 0.0, 1.0);
                }
                // индекс счётчиков
                int counter_index = 0;
                foreach (Counter counter in containers[index].Counters)
                {
                    if (counter == null) continue;

                    //
                    if (counter_index + 1 == counters_in_container) border = new Thickness(1.0, 0.0, 0.0, 2.0);
                    //
                    border.Right = 0.0;
                    //
                    report.Items.Add(AddCellToReport(counter.Name, rowIndex, 5, rows_to_span, 1, border, ReportItemStyle.StringCellStyle));
                    report.Items.Add(AddCellToReport(counter.Type, rowIndex, 6, rows_to_span, 1, border, ReportItemStyle.StringCellStyle));
                    report.Items.Add(AddCellToReport(counter.SerialNumber, rowIndex, 7, rows_to_span, 1, border, ReportItemStyle.StringCellStyle));
                    report.Items.Add(AddCellToReport(counter.NetworkAdress, rowIndex, 8, rows_to_span, 1, border, ReportItemStyle.StringCellStyle));
                    report.Items.Add(AddCellToReport(counter.Status, rowIndex, 9, rows_to_span, 1, border, ReportItemStyle.StringCellStyle));

                    indications = ReportHelper.IndicationsToString(counter.StartIndications);
                    style = indications == "нет" ? ReportItemStyle.NullIntCellStyle : ReportItemStyle.IntCellStyle;
                    report.Items.Add(AddCellToReport(indications, rowIndex, 10, rows_to_span, 1, border, style));

                    indications = ReportHelper.IndicationsToString(counter.EndIndications);
                    style = indications == "нет" ? ReportItemStyle.NullIntCellStyle : ReportItemStyle.IntCellStyle;
                    report.Items.Add(AddCellToReport(indications, rowIndex, 11, rows_to_span, 1, border, style));

                    indications = ReportHelper.IndicationsToString(counter.IndicationsDifference);
                    style = indications == "нет" ? ReportItemStyle.NullIntCellStyle : ReportItemStyle.IntCellStyle;
                    report.Items.Add(AddCellToReport(indications, rowIndex, 12, rows_to_span, 1, border, style));

                    border.Right = 2.0;

                    indications = ReportHelper.IndicationsToString(counter.PreviousIndicationsDifference);
                    style = indications == "нет" ? ReportItemStyle.NullIntCellStyle : ReportItemStyle.IntCellStyle;
                    report.Items.Add(AddCellToReport(indications, rowIndex, 13, rows_to_span, 1, border, style));

                    // счетчик строк
                    rowIndex++;
                    counter_index++;
                }
                // корректируем счётчик строк
                if (rows_to_span == 2) rowIndex++;
            }

            ReportExporter.ReportToGrid(report, ref this.ReportGrid);            
        }

        private ReportItem AddCellToReport(string content,
                                    int row, int column,
                                    int row_span, int column_span,
                                    Thickness borderThickness,
                                    ReportItemStyle cell_style_name)
        {
            ReportItem item = new ReportItem();

            item.Row = row;
            item.Column = column;
            item.RowSpan = row_span;
            item.ColumnSpan = column_span;

            item.Caption = content;
            item.Style = cell_style_name;

            item.Border = borderThickness;

            return item;
        }

        private void AddRowDefinitionToTable(float[] rowHeight)
        {
            // добавляем строки
            foreach (float rh in rowHeight)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(rh);
                report.RowDefinition.Add(rowDefinition);
            }
        }

        private void AddRowDefinitionToTable(float rowHeight, int count)
        {
            for (int index = 1; index <= count; index++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(rowHeight);
                report.RowDefinition.Add(rowDefinition);
            }
        }

        private void AddRowDefinitionToTable(int count)
        {
            for (int index = 1; index <= count; index++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Auto);
                report.RowDefinition.Add(rowDefinition);
            }
        }

        private void AddRowDefinitionToTable(float rowHeight)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(rowHeight);
            report.RowDefinition.Add(rowDefinition);
        }

        private void AddTipWhenTableIsEmpty()
        {
            Grid grid = new Grid();
            grid.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            grid.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            for (int index = 1; index <= 4; index++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            }

            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            TextBlock line1 = new TextBlock(new Run("Данные для отображения отсутствуют"));
            line1.Style = (Style)this.FindResource("TextBlockTableHeaderStyle");

            Grid.SetRow(line1, 0);
            Grid.SetColumn(line1, 0);
            Grid.SetColumnSpan(line1, 3);
            grid.Children.Add(line1);

            TextBlock line2 = new TextBlock(new Run("Для получения данных:"));
            line2.Style = (Style)this.FindResource("TextBlockTextTipStyle");

            Grid.SetRow(line2, 1);
            Grid.SetColumn(line2, 0);
            Grid.SetColumnSpan(line2, 3);
            grid.Children.Add(line2);

            TextBlock line3 = new TextBlock(new Run("• нажмите кнопку «Соединиться»;\n• в появившемся слева списке выберите подразделение;\t\t\n"+
                "• при необходимости измените временной интервал."));
            line3.Style = (Style)this.FindResource("TextBlockBulletTextTipStyle");
            Grid.SetRow(line3, 2);
            Grid.SetColumn(line3, 0);
            Grid.SetColumnSpan(line3, 3);
            grid.Children.Add(line3);

            Button button = new Button();
            button.Content = "Соединиться";
            button.FontSize = 14 * (96.0 / 72.0);
            button.HorizontalAlignment = HorizontalAlignment.Stretch;
            button.Margin = new Thickness(0, 15, 0, 5);
            Grid.SetRow(button, 3);
            Grid.SetColumn(button, 1);
            grid.Children.Add(button);

            button.Command = CommandConnectToDb;

            Binding bindVisible = new Binding("IsNotConnected");
            bindVisible.Source = this;
            //bindVisible.Mode = System.Windows.Data.BindingMode.TwoWay;
            bindVisible.Converter = new BooleanToVisibilityConverter();
            //bindVisible.Path = new PropertyPath("IsNotConnected");

            button.SetBinding(UIElement.VisibilityProperty, bindVisible);

            Image img = new Image() { Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/Resources/smile.png") as ImageSource };
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(img, 4);
            Grid.SetColumn(img, 1);
            grid.Children.Add(img);

            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = new Thickness(2.0, 2.0, 2.0, 2.0);

            border.Child = grid;

            this.ReportGrid.ColumnDefinitions.Clear();
            this.ReportGrid.RowDefinitions.Clear();
            this.ReportGrid.Children.Clear();
            this.ReportGrid.Children.Add(border);
        }

    }
}
