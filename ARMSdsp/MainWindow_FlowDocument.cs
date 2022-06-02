using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml.Linq;
using SDSPPresenter.Presenters;
using SDSPPresenter.Views;
using SDSPServiceInterface.Entities;


namespace DataBinding
{
    /// <summary>
    /// Создание документа
    /// </summary>
    public partial class MainWindow : Window, ISdspView, IView<SdspPresenter>
    {
/*        // класс-помощник для создания структуры документа
        private Document document;

        private void Work(SdspInformation sdspInformation)
        {
            this.flow_document.Blocks.Clear();

            // Создаем таблицу
            Table table = new Table();

            // применяем стиль
            table.Style = document.GetStyleFromName("TableStyle");

            int[] col_w = new int[15] { 30, 200, 150,  80,  55,
                                         0,  40,  60,   0,  50,
                                        60,   0,   0,   0,   0};

            // Создаем столбцы и добавляем их в таблицу
            for (int index = 0; index <= 14; index++)
            {
                table.Columns.Add(new TableColumn() { Width = new GridLength(col_w[index]) });

                 // Устанавливаем альтернативный фон для чётных столбцов
                if (index % 2 == 0)
                    table.Columns[index].Background = Brushes.Beige;
                else
                    table.Columns[index].Background = Brushes.LightSteelBlue; 
            }

            // и добавляем ее в коллекцию блоков документа
            this.flow_document.Blocks.Add(table);

            document.AddStringsToParagraph(new string[] {
                    String.Format("Отчет по результатам опроса объектов, оборудованных СДСП {0}", "!!!!"), //sdspInformation.Departament.Name
                    String.Format("на {0}", DateTime.Now)}, //sdspInformation.ReportingDate
                    "ParagraphStyle");

            ParseRES(null);
            /*switch (sdspInformation.Departament.DepartamentType)
            {
                // если выбран филиал
                case DepartamentType.Fes:
                    ParseFES(sdspInformation.Departament);
                    break;
                // если выбран РЭС
                case DepartamentType.Res:
                    ParseRES(sdspInformation.Departament);
                    break;
            }
        } 

        private void AddHeaderToTable(Table table, DateTime start_date, DateTime end_date)
        {
            if (this.document == null) throw new System.NullReferenceException();
            if (table == null) throw new System.NullReferenceException();

            int columns_count = table.Columns.Count;
            // новая группа строк
            TableRowGroup row_group = new TableRowGroup();
            // шапка состоит из 4-х строк
            TableRow row1 = new TableRow();
            TableRow row2 = new TableRow();
            TableRow row3 = new TableRow();
            TableRow row4 = new TableRow();

            // 1-я строка
            row1.Cells.Add(document.AddHeaderCell("№ п/п", 4, 1, 20));
            row1.Cells.Add(document.AddHeaderCell("Потребитель", 4, 1, 25));
            row1.Cells.Add(document.AddHeaderCell("Расчетная точка", 3, 1, 20));
            row1.Cells.Add(document.AddHeaderCell("Номер телефона модема", 3, 1, 0));
            row1.Cells.Add(document.AddHeaderCell("Статус модема", 3, 1, 10));
            row1.Cells.Add(document.AddHeaderCell("Последний сеанс", 4, 1, 20));
            row1.Cells.Add(document.AddHeaderCell("Данные счетчика", 1, 9, 0));

            // 2-я строка
            row2.Cells.Add(document.AddHeaderCell("Присоединение", 3, 1, 0));
            row2.Cells.Add(document.AddHeaderCell("Тип счетчика", 3, 1, 10));
            row2.Cells.Add(document.AddHeaderCell("Заводской номер", 3, 1, 10));
            row2.Cells.Add(document.AddHeaderCell("Сетевой адрес", 3, 1, 10));
            row2.Cells.Add(document.AddHeaderCell("Состояние", 3, 1, 18));
            row2.Cells.Add(document.AddHeaderCell("показания периода", 1, 2, 0));
            row2.Cells.Add(document.AddHeaderCell("Разность показаний", 2, 2, 10));

            // 3-я строка
            row3.Cells.Add(document.AddHeaderCell("начало", 1, 1, 0));
            row3.Cells.Add(document.AddHeaderCell("окончание", 1, 1, 0));

            // 3-я строка
            row4.Cells.Add(document.AddHeaderCell("ВЛ-10 кВ, номер ТП", 1, 3, 0));
            row4.Cells.Add(document.AddHeaderCell(String.Format("dd.mm.yyyy", start_date), 1, 1, 0));
            row4.Cells.Add(document.AddHeaderCell(String.Format("dd.mm.yyyy", end_date), 1, 1, 0));
            row4.Cells.Add(document.AddHeaderCell("отчетный", 1, 1, 0));
            row4.Cells.Add(document.AddHeaderCell("предыдущий", 1, 1, 0));

            row_group.Rows.Add(row1);
            row_group.Rows.Add(row2);
            row_group.Rows.Add(row3);
            row_group.Rows.Add(row4);
            table.RowGroups.Add(row_group);
        }

        private void ParseFES(Departament fes)
        {
            if (fes.DepartamentType != DepartamentType.Fes) throw new ArgumentException();
            // создаем отчет для каждого РЭС
            foreach (Departament fes_item in fes.ChildDepartaments)
            {
                ParseRES(fes_item);
            }
        }

        private void ParseRES(Departament res)
        {
            //if (res.DepartamentType != DepartamentType.Res) throw new ArgumentException();
            //foreach (Departament res_item in res.ChildDepartaments)
            {
                // Название РЭС
                Paragraph p = new Paragraph();
                p.Inlines.Add(new Run("Ghbdtn")); //res.Name
                p.TextAlignment = TextAlignment.Center;
                p.FontStyle = FontStyles.Oblique;
                p.FontSize = 14 * (96.0 / 72.0);
                p.FontWeight = FontWeights.Bold;
                p.Margin = new Thickness(0, 0, 0, 0.5f);
                document.AddParagraphToDocument(p);
                // Создаем таблицу
                Table table = document.AddTableToDocument(15);
                // заголовки
                AddHeaderToTable(table, this.GetStartDate(), this.GetEndDate());
            }
        }
*/

    }
}
