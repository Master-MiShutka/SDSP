using System;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

using System.IO;

using OfficeOpenXml;

namespace EPPlusWrapper
{
    public class Wrapper
    {
        #region Variables

        ExcelPackage activePackage;
        ExcelWorkbook activeWorkBook;
        ExcelWorksheet activeSheet;
        ExcelRange activeRange;

        #endregion

        #region Constructor

        /// <summary>
        /// Конструктор
        /// </summary>
        public Wrapper()
        {
            activePackage = new ExcelPackage();

            activeWorkBook = activePackage.Workbook;
            activeSheet = activeWorkBook.Worksheets.Add("Лист 1");

            // свойства
            activeWorkBook.Properties.Author = "Инженер Трус Михаил Петрович";
            activeWorkBook.Properties.Title = "Просмотр данных СДСП";
            activeWorkBook.Properties.Category = "Просмотр данных СДСП";
            activeWorkBook.Properties.Keywords = "СДСП, ОЭС";
            activeWorkBook.Properties.Comments = "без комментариев";
            activeWorkBook.Properties.Company = "Отдел сбыта электроэнергии Ошмянских ЭС";

            activeSheet.View.ZoomScale = 75;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Сохранение текущего документа
        /// </summary>
        public void Save()
        {
            activePackage.Save();
        }

        /// <summary>
        /// Сохранение текущего документа
        /// </summary>
        public void SaveAs(string fileName)
        {
            activePackage.SaveAs(new FileInfo(fileName));
        }

        /// <summary>
        /// Установка форматирования по умолчанию
        /// </summary>
        public void SetDefaultStyles()
        {
        }

        /// <summary>
        /// Добавление листа
        /// </summary>
        /// <param name="name"></param>
        public void AddWorksheet(string name)
        {
            activeSheet = activePackage.Workbook.Worksheets.Add(name);
            activeRange = activeSheet.Cells["A1"];
        }

        /// <summary>
        /// Установка параметров страницы
        /// </summary>
        /// <param name="pageSettings"></param>
        public void SetPageSettings(WorkSheetPageSettings pageSettings)
        {
            activeSheet.PrinterSettings.HorizontalCentered = true;

            activeSheet.HeaderFooter.EvenHeader.RightAlignedText = ExcelHeaderFooter.CurrentDate + " " + ExcelHeaderFooter.CurrentTime;
            activeSheet.HeaderFooter.EvenFooter.LeftAlignedText = "инженер ОС ОЭС";            
            activeSheet.HeaderFooter.EvenFooter.CenteredText = "__________";
            activeSheet.HeaderFooter.EvenFooter.RightAlignedText = "Трус М.П.";


            activeSheet.PrinterSettings.Orientation = (eOrientation)pageSettings.Orientation;
            activeSheet.PrinterSettings.PaperSize = (ePaperSize)pageSettings.PaperSize;

            if (pageSettings.Zoom != 100)
            {
                activeSheet.PrinterSettings.FitToWidth = pageSettings.FitToPagesWide;
                activeSheet.PrinterSettings.FitToHeight = pageSettings.FitToPagesTall;
            }
            else
                activeSheet.PrinterSettings.Scale = pageSettings.Zoom;

            activeSheet.PrinterSettings.LeftMargin = (decimal)(pageSettings.LeftMarginMM * 25.4);
            activeSheet.PrinterSettings.RightMargin = (decimal)(pageSettings.RightMarginMM * 25.4);
            activeSheet.PrinterSettings.TopMargin = (decimal)(pageSettings.TopMarginMM * 25.4);
            activeSheet.PrinterSettings.BottomMargin = (decimal)(pageSettings.BottomMarginMM * 25.4);

            activeSheet.PrinterSettings.PrintArea = activeSheet.Cells[pageSettings.fromRow, pageSettings.fromColumn, pageSettings.toRow, pageSettings.toColumn];
        }

        /// <summary>
        /// Добавление стиля
        /// </summary>
        /// <param name="styleName">Название стиля</param>
        /// <param name="cellStyle">Стиль ячейки</param>
        public void SetCellStyle(CellStyle cellStyle, OfficeOpenXml.Style.ExcelStyle style)
        {
            style.WrapText = cellStyle.WrapText;
            style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)cellStyle.HorizontalAlignment;
            style.VerticalAlignment = (OfficeOpenXml.Style.ExcelVerticalAlignment)cellStyle.VerticalAlignment;

            style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(cellStyle.Background);

            style.Font.Color.SetColor(cellStyle.Foreground);

            style.Font.Name = cellStyle.FontStyle.FontFamily;
            style.Font.Size = (float)cellStyle.FontStyle.FontSize;
            style.Font.Italic = cellStyle.FontStyle.FontStyle == FontStyles.Italic;
            style.Font.Bold = cellStyle.FontStyle.FontWeight == FontWeights.Bold;

            //style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.None, Color.Black);
            style.Border.Diagonal.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
        }

        /// <summary>
        /// Удаление листа с указанным индексом
        /// </summary>
        /// <param name="index">номер</param>
        public void DeleteWorksheet(int index)
        {
            if (index < 1)
                throw new Exception("Index out of bounds");
            if (index > activeWorkBook.Worksheets.Count)
                throw new Exception("Index out of bounds");

            activeWorkBook.Worksheets.Delete(index);
        }

        /// <summary>
        /// Удаление листа по имени
        /// </summary>
        /// <param name="workSheetName"></param>
        public void DeleteWorksheet(string workSheetName)
        {
            activeWorkBook.Worksheets.Delete(workSheetName);
        }

        /// <summary>
        /// Установка текущего листа по имени
        /// </summary>
        /// <param name="workSheetName">название</param>
        public void SetCurrentWorksheet(string workSheetName)
        {
            activeSheet = activeWorkBook.Worksheets[workSheetName];
        }

        /// <summary>
        /// Установка текущего листа по индексу
        /// </summary>
        /// <param name="index">номер</param>
        public void SetCurrentWorksheet(int index)
        {
            if (index < 1)
                throw new Exception("Index out of bounds");
            if (index > activeWorkBook.Worksheets.Count)
                throw new Exception("Index out of bounds");

            activeSheet = activeWorkBook.Worksheets[index];
        }

        /// <summary>
        /// Установка границ текущего диапазона
        /// </summary>
        /// <param name="borders">Границы</param>
        public void SetBorders(CellBorders borders)
        {
            if (borders.Left.Weight != BorderWeights.None)
            {
                activeRange.Style.Border.Left.Style = (OfficeOpenXml.Style.ExcelBorderStyle)borders.Left.LineStyle | (OfficeOpenXml.Style.ExcelBorderStyle)borders.Left.Weight;
                activeRange.Style.Border.Left.Color.SetColor(Color.Black);
            }

            if (borders.Right.Weight != BorderWeights.None)
                activeRange.Style.Border.Right.Style = (OfficeOpenXml.Style.ExcelBorderStyle)borders.Right.LineStyle | (OfficeOpenXml.Style.ExcelBorderStyle)borders.Right.Weight;

            if (borders.Top.Weight != BorderWeights.None)
                activeRange.Style.Border.Top.Style = (OfficeOpenXml.Style.ExcelBorderStyle)borders.Top.LineStyle | (OfficeOpenXml.Style.ExcelBorderStyle)borders.Top.Weight;

            if (borders.Bottom.Weight != BorderWeights.None)
                activeRange.Style.Border.Bottom.Style = (OfficeOpenXml.Style.ExcelBorderStyle)borders.Bottom.LineStyle | (OfficeOpenXml.Style.ExcelBorderStyle)borders.Bottom.Weight;

            activeRange.Style.Border.DiagonalDown = false;
            activeRange.Style.Border.DiagonalUp = false;
        }

        /// <summary>
        /// Объединение ячеек диапазона
        /// </summary>
        /// <param name="location">адрес</param>
        public void MergeRange(string location)
        {
            activeSheet.Cells[location].Merge = true;
        }

        /// <summary>
        /// Объединение ячеек диапазона
        /// </summary>
        /// <param name="fromR">начиная со строки</param>
        /// <param name="fromC">начиная со столбца</param>
        /// <param name="toR">заканчивая строкой</param>
        /// <param name="toC">заканчивая столбцом</param>
        public void MergeRangeR1C1(int fromR, int fromC, int toR, int toC)
        {
            activeSheet.Cells[fromR, fromC, toR, toC].Merge = true;
        }

        /// <summary>
        /// Установка текущего диапазона
        /// </summary>
        /// <param name="location">адрес</param>
        public void SetRange(string location)
        {
            activeRange = activeSheet.Cells[location];
        }

        public void SetRange(int row, int column)
        {
            activeRange = activeSheet.Cells[row, column];
        }

        /// <summary>
        /// Установка текущего диапазона
        /// </summary>
        /// <param name="fromR">начиная со строки</param>
        /// <param name="fromC">начиная со столбца</param>
        /// <param name="toR">заканчивая строкой</param>
        /// <param name="toC">заканчивая столбцом</param>
        public void SetRange(int fromR, int fromC, int toR, int toC)
        {
            activeRange = activeSheet.Cells[fromR, fromC, toR, toC];
        }

        /// <summary>
        /// Установка значения диапазона
        /// </summary>
        /// <param name="value">значение</param>
        public void SetRangeValue(string value)
        {
            activeRange.Value = value;
        }

        /// <summary>
        /// Установка значения диапазона
        /// </summary>
        /// <param name="value">значение</param>
        public void SetRangeValue(string[,] value)
        {
            activeRange.Value = value;
        }

        /// <summary>
        /// Возвращает значение ячейки
        /// </summary>
        /// <param name="location">адрес</param>
        public object GetCellValue(string location)
        {
            return activeSheet.Cells[location].Value;
        }

        /// <summary>
        /// Возвращает значение диапазона в виде массива
        /// </summary>
        /// <param name="location">адрес</param>
        /// <returns>строковый массив</returns>
        public string[,] GetRangeValue(string location)
        {
            return (string[,])activeSheet.Cells[location].Value;
        }

        /// <summary>
        /// Установка значения и стиля ячейки
        /// </summary>
        /// <param name="location">адрес</param>
        /// <param name="value">значение</param>
        /// <param name="style">имя стиля</param>
        public void SetCellValue(string location, string value, CellStyle style)
        {
            SetRange(location);
            activeRange.Value = value;
            SetCellStyle(style, activeRange.Style);
        }
        /// <summary>
        /// Установка значения и стиля ячейки
        /// </summary>
        /// <param name="row">номер строки</param>
        /// <param name="column">номер столбца</param>
        /// <param name="value">значение</param>
        /// <param name="style">имя стиля</param>
        public void SetCellValue(int row, int column, string value, CellStyle style)
        {
            SetRange(row, column);
            activeRange.Value = value;
            SetCellStyle(style, activeRange.Style);
        }
        /// <summary>
        /// Установка значения и стиля ячейки и объединить диапазон
        /// </summary>
        /// <param name="row">номер строки</param>
        /// <param name="column">номер столбца</param>
        /// <param name="rowSpan">ширина дипазона</param>
        /// <param name="columnSpan">высота диапазона</param>
        /// <param name="value">значение</param>
        /// <param name="style">имя стиля</param>
        public void SetCellValue(int row, int column, int rowSpan, int columnSpan, string value, CellStyle style)
        {
            activeRange = activeSheet.Cells[row, column, row + rowSpan - 1, column + columnSpan - 1];

            activeRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
            activeRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
            activeRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
            activeRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

            activeRange.Style.Border.DiagonalUp = false;
            activeRange.Style.Border.DiagonalDown = false;

            activeRange.Value = value;

            SetCellStyle(style, activeRange.Style);

            activeRange.Merge = true;
        }

        /// <summary>
        /// Установка высоты строки
        /// </summary>
        /// <param name="rowIndex">номер строки</param>
        /// <param name="height">высота</param>
        public void SetRowHeight(int rowIndex, double height)
        {
            if (height != 0)
                activeSheet.Row(rowIndex).Height = height;
        }
        /// <summary>
        /// Установка высоты строк
        /// </summary>
        /// <param name="fromRowIndex">со строки</param>
        /// <param name="toRowIndex">по строку</param>
        /// <param name="height">высота</param>
        public void SetRowHeight(int fromRowIndex, int toRowIndex, double height)
        {
            if (height != 0)
                for (int index = fromRowIndex; index <= toRowIndex; index ++)
                    activeSheet.Row(index).Height = height;
        }

        /// <summary>
        /// Установка ширины столбца
        /// </summary>
        /// <param name="columnIndex">номер столбца</param>
        /// <param name="width">ширина</param>
        public void SetColumnWidth(int columnIndex, double width)
        {
            if (width == 0)
            {
                ExcelColumn column = activeSheet.Column(columnIndex);
                column.AutoFit();
            }
            else
                activeSheet.Column(columnIndex).Width = Pixel2ColumnWidth(activeSheet, width);
        }
        /// <summary>
        /// Установка ширины столбцов
        /// </summary>
        /// <param name="fromColumnIndex">со столбца</param>
        /// <param name="toColumnIndex">по столбец</param>
        /// <param name="width">ширина</param>
        public void SetColumnWidth(int fromColumnIndex, int toColumnIndex, double width)
        {
            if (width == 0)
            {
                for (int index = fromColumnIndex; index < toColumnIndex; index++)
                    activeSheet.Column(index).AutoFit();
            }
            else
                for (int index = fromColumnIndex; index <= toColumnIndex; index++)
                    activeSheet.Column(index).Width = width;// Pixel2RowHeight(width);
        }

        //http://epplus.codeplex.com/discussions/229134
        public double Pixel2ColumnWidth(ExcelWorksheet ws, double pixels)
        {
            //The correct method to convert pixel to width is:
            //1. use the formula =Truncate(({pixels}-5)/{Maximum Digit Width} * 100+0.5)/100 
            //    to convert pixel to character number.
            //2. use the formula width = Truncate([{Number of Characters} * {Maximum Digit Width} + {5 pixel padding}]/{Maximum Digit Width}*256)/256 
            //    to convert the character number to width.

            //get the maximum digit width
            decimal mdw = ws.Workbook.MaxFontWidth;

            //convert pixel to character number
            decimal numChars = decimal.Truncate(decimal.Add((decimal)(pixels - 5) / mdw * 100, (decimal)0.5)) / 100;
            //convert the character number to width
            decimal excelColumnWidth = decimal.Truncate((decimal.Add(numChars * mdw, (decimal)5)) / mdw * 256) / 256;

            return Convert.ToDouble(excelColumnWidth);
        }
        //http://epplus.codeplex.com/discussions/229134
        public double Pixel2RowHeight(double pixels)
        {
            //convert height to pixel
            double excelRowHeight = pixels * 0.75d;

            return excelRowHeight;
        }

        #endregion
    }

    #region Enums

    /// <summary>
    /// Тип линий границ ячейки
    /// </summary>
    public enum BorderLineStyleType
    {
        Dash = OfficeOpenXml.Style.ExcelBorderStyle.Dashed,
        DashDot = OfficeOpenXml.Style.ExcelBorderStyle.DashDot,
        DashDotDot = OfficeOpenXml.Style.ExcelBorderStyle.DashDotDot,
        Dot = OfficeOpenXml.Style.ExcelBorderStyle.Dotted,
        SlantDashDot = OfficeOpenXml.Style.ExcelBorderStyle.MediumDashDot,
        None = OfficeOpenXml.Style.ExcelBorderStyle.None
    }

    /// <summary>
    /// Толщина линий границ ячейки
    /// </summary>
    public enum BorderWeights
    {
        Hairline = OfficeOpenXml.Style.ExcelBorderStyle.Hair,
        Medium = OfficeOpenXml.Style.ExcelBorderStyle.Medium,
        Thick = OfficeOpenXml.Style.ExcelBorderStyle.Thick,
        Thin = OfficeOpenXml.Style.ExcelBorderStyle.Thin,
        Double = OfficeOpenXml.Style.ExcelBorderStyle.Double,
        None = OfficeOpenXml.Style.ExcelBorderStyle.None
    }

    /// <summary>
    /// Граница
    /// </summary>
    public struct BorderType
    {
        public BorderWeights Weight;
        public BorderLineStyleType LineStyle;
    }

    /// <summary>
    /// Границы ячейки
    /// </summary>
    public struct CellBorders
    {
        public BorderType Left;
        public BorderType Right;
        public BorderType Top;
        public BorderType Bottom;

        /// <summary>
        /// Установка значений по умолчанию
        /// </summary>
        public void SetDefault()
        {
            Left = new BorderType() { Weight = BorderWeights.None, LineStyle = BorderLineStyleType.None };
            Right = new BorderType() { Weight = BorderWeights.None, LineStyle = BorderLineStyleType.None };
            Top = new BorderType() { Weight = BorderWeights.None, LineStyle = BorderLineStyleType.None };
            Bottom = new BorderType() { Weight = BorderWeights.None, LineStyle = BorderLineStyleType.None };
        }
    }

    /// <summary>
    /// Вертикальное выравнивание
    /// </summary>
    public enum VAlign
    {
        Bottom = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom,
        Center = OfficeOpenXml.Style.ExcelVerticalAlignment.Center,
        Distributed = OfficeOpenXml.Style.ExcelVerticalAlignment.Distributed,
        Justify = OfficeOpenXml.Style.ExcelVerticalAlignment.Justify,
        Top = OfficeOpenXml.Style.ExcelVerticalAlignment.Top
    }

    /// <summary>
    /// Горизонтальное выравнивание
    /// </summary>
    public enum HAlign
    {
        Right = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right,
        Left = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left,
        Justify = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify,
        Distributed = OfficeOpenXml.Style.ExcelHorizontalAlignment.Distributed,
        Center = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center,
        None = OfficeOpenXml.Style.ExcelHorizontalAlignment.General,
        Fill = OfficeOpenXml.Style.ExcelHorizontalAlignment.Fill,
        CenterAcrossSelection = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous
    }

    /// <summary>
    /// Шрифт ячейки
    /// </summary>
    public struct CellFontStyle
    {
        public string FontFamily;
        public double FontSize;
        public FontWeight FontWeight;
        public System.Windows.FontStyle FontStyle;
    }

    /// <summary>
    /// Стиль ячейки
    /// </summary>
    public struct CellStyle
    {
        public bool WrapText;

        public CellFontStyle FontStyle;
        public HAlign HorizontalAlignment;
        public VAlign VerticalAlignment;
        public Color Foreground;
        public Color Background;

        /// <summary>
        /// Установка значений по умолчанию
        /// </summary>
        public void SetDefaultCellStyle()
        {
            WrapText = false;
            FontStyle = new CellFontStyle()
            {
                FontFamily = "Calibri",
                FontSize = 12.0d,
                FontStyle = System.Windows.FontStyles.Normal,
                FontWeight = FontWeights.Normal
            };
            HorizontalAlignment = HAlign.Center;
            VerticalAlignment = VAlign.Center;
            Foreground = Color.Black;
            Background = Color.White;
        }
    }

    /// <summary>
    /// Ориентация страницы листа
    /// </summary>
    public enum PageOrientation
    {
        /// <summary>
        /// Ландшафтная
        /// </summary>
        Landskape = OfficeOpenXml.eOrientation.Landscape,
        /// <summary>
        /// Портрет
        /// </summary>
        Portrait = OfficeOpenXml.eOrientation.Portrait
    }

    /// <summary>
    /// Размер бумаги
    /// </summary>
    public enum PaperSize
    {
        /// <summary>
        /// Бумага формата А4
        /// </summary>
        PaperA4 = OfficeOpenXml.ePaperSize.A4,
        /// <summary>
        /// Бумага формата А3
        /// </summary>
        PaperA3 = OfficeOpenXml.ePaperSize.A3
    }

    /// <summary>
    /// Параметры страницы листа
    /// </summary>
    public struct WorkSheetPageSettings
    {
        /// <summary>
        /// Оринтация
        /// </summary>
        public PageOrientation Orientation;
        /// <summary>
        /// размер
        /// </summary>
        public PaperSize PaperSize;
        /// <summary>
        /// Вписать содержимое на указанное число листов в ширину
        /// </summary>
        public int FitToPagesWide;
        /// <summary>
        /// Вписать содержимое на указанное число листов в высоту
        /// </summary>
        public int FitToPagesTall;
        /// <summary>
        /// Левый отступ, мм
        /// </summary>
        public double LeftMarginMM;
        /// <summary>
        /// Правый отступ, мм
        /// </summary>
        public double RightMarginMM;
        /// <summary>
        /// Отступ сверху, мм
        /// </summary>
        public double TopMarginMM;
        /// <summary>
        /// Отступ снизу, мм
        /// </summary>
        public double BottomMarginMM;
        /// <summary>
        /// Область печати, начиная со строки
        /// </summary>
        public int fromRow;
        /// <summary>
        /// Область печати, начиная со столбца
        /// </summary>
        public int fromColumn;
        /// <summary>
        /// Область печати, до строки
        /// </summary>
        public int toRow;
        /// <summary>
        /// Область печати, до столбца
        /// </summary>
        public int toColumn;
        /// <summary>
        /// Масштаб
        /// </summary>
        public int Zoom;
    }

    #endregion
}
