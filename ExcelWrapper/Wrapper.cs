using System;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;

namespace MSExcelWrapper
{
   
    /// <summary>
    /// 
    /// </summary>
    public class Wrapper
    {
        #region Variables
        private dynamic excelApp;
        private Excel.Workbook workbook;
        private Excel.Sheets sheets;
        private Excel.Worksheet worksheet;
        private object missing = System.Reflection.Missing.Value;
        private Excel.Range range;
        #endregion

        #region Constructor

        public Wrapper() { }
        #endregion

        #region Methods
        /// <summary>
        /// Создание нового документа
        /// </summary>
        public bool Create(bool visible)
        {
            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                worksheet = new Microsoft.Office.Interop.Excel.Worksheet();
                workbook = (Excel.Workbook)(excelApp.Workbooks.Add(missing));
                workbook.DoNotPromptForConvert = true;
                worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                sheets = workbook.Worksheets;
                excelApp.Visible = visible;

                SetDefaultStyles();
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                CleanUp();
                return false;
            }

            return true;
        }

        public void SetDefaultStyles()
        {
            worksheet.Cells.HorizontalAlignment = HAlign.Center;
            worksheet.Cells.VerticalAlignment = VAlign.Center;
            worksheet.Cells.Font.Name = "SegoeUI";
        }

        /// <summary>
        /// Открытие существующего документа
        /// </summary>
        public bool Open(string path, bool visible)
        {
            if (!path.EndsWith("xls") && !path.EndsWith("xlsx"))
            {
                Console.WriteLine("Invalid file format");
                return false;
            }
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
                return false;
            }

            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                worksheet = new Microsoft.Office.Interop.Excel.Worksheet();
                workbook = excelApp.Workbooks.Open(path, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                sheets = workbook.Worksheets;
                excelApp.Visible = visible;
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                CleanUp();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Закрытие приложения без сохранения изменений
        /// </summary>
        public void Close()
        {
            CleanUp();
        }

        /// <summary>
        /// Сохранение текущего документа
        /// </summary>
        public void Save()
        {
            workbook.DoNotPromptForConvert = true;
            workbook.Save();
        }

        public void SetPageSettings(WorkSheetPageSettings pageSettings)
        {
            worksheet.PageSetup.RightHeader = @"&""Arial""&""-,курсив""&10&K01+034&D - &T";
            worksheet.PageSetup.LeftFooter = @"&""-,курсив""&10инженер ОС ОЭС";
            worksheet.PageSetup.CenterFooter = "__________";
            worksheet.PageSetup.RightFooter = @"&""-,курсив""&10Трус М.П.";
            worksheet.PageSetup.CenterHorizontally = true;

            if (pageSettings.FitToPagesWide != 0)
            {
                worksheet.PageSetup.FitToPagesWide = pageSettings.FitToPagesWide;
                worksheet.PageSetup.Zoom = false;
            }
            if (pageSettings.FitToPagesTall != 0)
            {
                worksheet.PageSetup.FitToPagesTall = pageSettings.FitToPagesTall;
                worksheet.PageSetup.Zoom = false;
            }

            worksheet.PageSetup.LeftMargin = excelApp.InchesToPoints(pageSettings.LeftMarginMM / 25.4);
            worksheet.PageSetup.TopMargin = excelApp.InchesToPoints(pageSettings.TopMarginMM / 25.4);
            worksheet.PageSetup.RightMargin = excelApp.InchesToPoints(pageSettings.RightMarginMM / 25.4);
            worksheet.PageSetup.BottomMargin = excelApp.InchesToPoints(pageSettings.BottomMarginMM / 25.4);

            worksheet.PageSetup.Orientation = (Excel.XlPageOrientation)pageSettings.Orientation;
            worksheet.PageSetup.PaperSize = (Excel.XlPaperSize)pageSettings.PaperSize;

            Excel.Range range = worksheet.get_Range(worksheet.Cells[pageSettings.fromRow, pageSettings.fromColumn],
                                                    worksheet.Cells[pageSettings.toRow, pageSettings.toColumn]);

            worksheet.PageSetup.PrintArea = range.get_AddressLocal(range.Rows.Count, range.Columns.Count, Excel.XlReferenceStyle.xlA1, null, worksheet.UsedRange).Replace("$", "");;
        }

        public void AddStyle(string styleName, CellStyle cellStyle)
        {
            Excel.Style style = workbook.Styles.Add(styleName, missing);

            style.WrapText = cellStyle.WrapText;
            style.HorizontalAlignment = (Excel.XlHAlign)cellStyle.HorizontalAlignment;
            style.VerticalAlignment = (Excel.XlVAlign)cellStyle.VerticalAlignment;
            style.IncludeAlignment = true;            

            style.Interior.Color = ColorTranslator.ToOle(cellStyle.Background);
            style.Font.Color = ColorTranslator.ToOle(cellStyle.Foreground);

            style.Font.Name = cellStyle.FontStyle.FontFamily;
            style.Font.Size = cellStyle.FontStyle.FontSize;
            style.Font.Italic = cellStyle.FontStyle.FontStyle == FontStyles.Italic;
            style.Font.Bold = cellStyle.FontStyle.FontWeight == FontWeights.Bold;
            style.IncludeFont = true;

            style.IncludeBorder = true;

            style.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            style.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            style.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            style.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            style.Borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            style.Borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            style.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            style.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
        }


        /// <summary>
        /// Добавление листа
        /// </summary>
        /// <param name="name"></param>
        public void AddWorksheet(string name)
        {
            workbook.Worksheets.Add(missing, missing, missing, missing);
            Excel.Worksheet worksheetName = (Excel.Worksheet)sheets.get_Item(1);
            worksheetName.Name = name;
        }

        /// <summary>
        /// Удаление листа с указанным индексом
        /// </summary>
        /// <param name="index"></param>
        public void DeleteWorksheet(int index)
        {
            bool deleteCurrentSheet = false;

            if (index < 1)
                throw new Exception("Index out of bounds");
            if (index > workbook.Sheets.Count)
                throw new Exception("Index out of bounds");

            //Check if we are deleting the worksheet that is being used. If so point it to another worksheet after deletion
            if (worksheet.Name.Equals(((Excel.Worksheet)excelApp.Application.ActiveWorkbook.Sheets[index]).Name))
                deleteCurrentSheet = true;

            ((Excel.Worksheet)excelApp.Application.ActiveWorkbook.Sheets[index]).Delete();

            if (deleteCurrentSheet)
                worksheet = (Excel.Worksheet)sheets.get_Item(1);

        }

        /// <summary>
        /// Удаление листа по имени
        /// </summary>
        /// <param name="name"></param>
        public void DeleteWorksheet(string name)
        {
            for (int i = 0; i < workbook.Sheets.Count; i++)
            {
                Excel.Worksheet worksheetDelete = (Excel.Worksheet)sheets.get_Item(i + 1);
                if (name.Equals(worksheetDelete.Name))
                {
                    DeleteWorksheet(i + 1);
                }
            }
        }

        /// <summary>
        /// Установка текущего листа по имени
        /// </summary>
        /// <param name="name"></param>
        public void SetCurrentWorksheet(string name)
        {
            for (int i = 0; i < workbook.Sheets.Count; i++)
            {
                Excel.Worksheet worksheetCurrent = (Excel.Worksheet)sheets.get_Item(i + 1);
                if (name.Equals(worksheetCurrent.Name))
                {
                    worksheet = (Excel.Worksheet)sheets.get_Item(i + 1);
                }
            }
        }

        /// <summary>
        /// Установка текущего листа по индексу
        /// </summary>
        /// <param name="index"></param>
        public void SetCurrentWorksheet(int index)
        {
            if (index < 1)
                throw new Exception("Index out of bounds");
            if (index > workbook.Sheets.Count)
                throw new Exception("Index out of bounds");

            worksheet = (Excel.Worksheet)sheets.get_Item(index);
        }

        /// <summary>
        /// Сохранение как excel 2007
        /// </summary>
        /// <param name="fullPath">Full path with .xlsx extension</param>
        public void SaveAs2007(string fullPath)
        {
            workbook.SaveAs(fullPath, 52,
                    missing, missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                    missing, missing, missing, missing, missing);
        }

        /// <summary>
        /// Сохранение как excel 2003
        /// </summary>
        /// <param name="fullPath">Full path with .xls extension</param>
        public void SaveAs2003(string fullPath)
        {
            workbook.DoNotPromptForConvert = true;
            workbook.SaveAs(fullPath, Excel.XlFileFormat.xlWorkbookNormal,
                missing, missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                false, false, missing, missing, missing);
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>        
        private void CleanUp()
        {
            try
            {
                if (workbook != null)
                {
                    workbook.Close(false, null, null);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    excelApp.Workbooks.Close();
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    excelApp.Workbooks.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }

                excelApp = null;
                sheets = null;
                worksheet = null;
                workbook = null;

            }
            catch (Exception) { }

            finally
            {
                GC.Collect();
            }
        }

        public void SetBorders(CellBorders borders)
        {
            if (borders.Left.Weight != BorderWeights.None)
            {
                range.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = (Excel.XlLineStyle)borders.Left.LineStyle;
                range.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = borders.Left.Weight;
            }

            if (borders.Right.Weight != BorderWeights.None)
            {
                range.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = (Excel.XlLineStyle)borders.Right.LineStyle;
                range.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = borders.Right.Weight;
            }

            if (borders.Top.Weight != BorderWeights.None)
            {
                range.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = (Excel.XlLineStyle)borders.Top.LineStyle;
                range.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = borders.Top.Weight;
            }

            if (borders.Bottom.Weight != BorderWeights.None)
            {
                range.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = (Excel.XlLineStyle)borders.Bottom.LineStyle;
                range.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = borders.Bottom.Weight;
            }

            range.Borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            range.Borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
        }

        public void MergeRange(string from, string to)
        {
            worksheet.get_Range(from, to).Merge();
        }

        public void MergeRangeR1C1(int fromR, int fromC, int toR, int toC)
        {
            worksheet.get_Range(worksheet.Cells[fromR, fromC], worksheet.Cells[toR, toC]).Merge();
        }

        /// <summary>
        /// Set the column ranges e.g A1 B30 or A1 A1 if you want.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void SetRange(string from, string to)
        {
            range = worksheet.get_Range(from, to);
        }
        public void SetRange(int fromR, int fromC, int toR, int toC)
        {
            range = worksheet.get_Range(worksheet.Cells[fromR, fromC], worksheet.Cells[toR, toC]);
        }

        /// <summary>
        /// Установка значения диапазона
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void SetRangeValue(string value)
        {
            range.Value2 = value;
        }

        /// <summary>
        /// Установка значения диапазона
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void SetRangeValue(string[,] value)
        {
            range.Value2 = value;
        }

        public void ExportToExcel(object[,] data)
        {
            /*dynamic excel = Microsoft.VisualBasic.Interaction.CreateObject("Excel.Application", string.Empty);

            excel.ScreenUpdating = false;
            dynamic workbook = excel.workbooks;
            workbook.Add();

            dynamic worksheet = excel.ActiveSheet;

            const int left = 1;
            const int top = 1;
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            int bottom = top + height - 1;
            int right = left + width - 1;

            if (height == 0 || width == 0)
                return;

            dynamic rg = worksheet.Range[worksheet.Cells[top, left], worksheet.Cells[bottom, right]];

            rg.Value = data;

            // Set borders
            for (int i = 1; i <= 4; i++)
                rg.Borders[i].LineStyle = 1;

            // Set auto columns width
            rg.EntireColumn.AutoFit();

            // Set header view
            dynamic rgHeader = worksheet.Range[worksheet.Cells[top, left], worksheet.Cells[top, right]];
            rgHeader.Font.Bold = true;
            rgHeader.Interior.Color = 189 * (int)Math.Pow(16, 4) + 129 * (int)Math.Pow(16, 2) + 78; // #4E81BD

            // Show excel app
            excel.ScreenUpdating = true;
            excel.Visible = true;

            rg = null;
            rgHeader = null;
            worksheet = null;
            workbook = null;
            excel = null;
            GC.Collect();*/
        }

        /// <summary>
        /// Returns a multidimensional array of the range
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string[,] GetRangeValue(string value)
        {
            return (string[,])range.Value2;
        }

        /// <summary>
        /// Formats the font in a cell, bold italic and underline take a bool as a value.
        /// Fontsize font color and font type are all nullable so you can write null if you dont want to specify
        /// </summary>
        public void FormatRangeFont(string from, string to, bool bold, bool italic, bool underline, double? fontSize, Color? fontColor, string fontName)
        {
            range = worksheet.get_Range(from, to);

            range.Font.Bold = bold;
            range.Font.Italic = italic;
            range.Font.Underline = underline;

            if (fontSize != null)
                range.Font.Size = fontSize;
            if (fontColor != null)
                range.Font.Color = ColorTranslator.ToOle(fontColor.Value);
            if (!string.IsNullOrEmpty(fontName))
                range.Font.Name = fontName;
        }

        /// <summary>
        /// Возвращает значение ячейки
        /// </summary>
        public string GetCellValue(string location)
        {
            return worksheet.get_Range(location, missing).Value2.ToString();
        }

        /// <summary>
        /// Установка значения ячейки
        /// </summary>
        public void SetCellValue(string location, string value)
        {
            range = worksheet.get_Range(location, missing);
            range.Value2 = value;
        }
        public void SetCellValue(string location, string value, string styleName)
        {
            range = worksheet.get_Range(location, missing);
            range.Value2 = value;
            range.Style = styleName;
        }
        public void SetCellValue(int row, int column, string value, string styleName)
        {
            range = worksheet.get_Range(worksheet.Cells[row, column], missing);
            range.Value2 = value;
            range.Style = styleName;
        }
        public void SetCellValue(int row, int column, int rowSpan, int columnSpan, string value, string styleName)
        {
            dynamic range = worksheet.Cells[row, column] as Excel.Range;
            range.Style = styleName;

            range.Style.Borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            range.Style.Borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            range.Style.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            range.Style.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            range.Style.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            range.Style.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlLineStyleNone;


            range.Value2 = value;
            SetRange(row, column, row + rowSpan - 1, column + columnSpan - 1);
            if (rowSpan >1 | columnSpan >1)
                MergeRangeR1C1(row, column, row + rowSpan-1, column + columnSpan-1);
        }

        /// <summary>
        /// Formats the font in a cell, bold italic and underline take a bool as a value.
        /// Fontsize font color and font type are all nullable so you can write null if you dont want to specify
        /// </summary>
        public void FormatCellFont(string location, bool bold, bool italic, bool underline, double? fontsize, Color? fontcolor, string fontname)
        {
            range = worksheet.get_Range(location, missing);

            range.Font.Bold = bold;
            range.Font.Italic = italic;
            range.Font.Underline = underline;

            if (fontsize != null)
                range.Font.Size = fontsize;
            if (fontcolor != null)
                range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontcolor.Value);
            if (!string.IsNullOrEmpty(fontname))
                range.Font.Name = fontname;
        }

        public void SetRowHeight(int rowIndex, double height)
        {
            range = worksheet.Rows[rowIndex] as Excel.Range;
            if (height == 0)
                excelApp.Rows[rowIndex].AutoFit();
            else
                range.EntireRow.RowHeight = height;
        }

        public void SetColumnWidth(int columnIndex, double width)
        {
            range = worksheet.Columns[columnIndex] as Excel.Range;
            if (width == 0)
                excelApp.Columns[columnIndex].AutoFit();
            else
                range.EntireColumn.ColumnWidth = width;
        }

        /// <summary>
        /// Отображать ли сообщения на экране
        /// </summary>
        /// <param name="alerts"></param>
        public void DisplayAlerts(bool alerts)
        {
            excelApp.DisplayAlerts = alerts;
        }

        /// <summary>
        /// Установка видимости документа
        /// </summary>
        private void SetVisibility(bool visibility)
        {
            excelApp.Visible = visibility;
        }

        /// <summary>
        /// Печать текущего листа
        /// </summary>
        public void PrintWorksheet(int copies)
        {
            worksheet.PrintOut(missing, missing, copies, missing, true, missing, missing, missing);
        }

        /// <summary>
        /// Печать всех листов 
        /// </summary>
        public void PrintAllWorksheets(int copies)
        {
            int count = WorksheetCount;

            for (int i = 1; i < count + 1; i++)
            {
                Excel.Worksheet worksheetPrint = (Excel.Worksheet)sheets.get_Item(i);
                worksheetPrint.PrintOut(missing, missing, copies, missing, true, missing, missing, missing);
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// Возвращает имя текущего листа книги
        /// </summary>
        public string CurrentWorksheet
        {
            get { return worksheet.Name; }
        }
        
        /// <summary>
        /// Возвращает адрес текущего диапазона
        /// </summary>
        public string GetRangeAddress
        {
            get
            {
                if (range == null)
                    return "null";
                else
                    return range.get_AddressLocal(range.Rows.Count, range.Columns.Count, Excel.XlReferenceStyle.xlA1, null, worksheet.UsedRange).Replace("$", "");
            }
        }

        /// <summary>
        /// Возвращает количество листов в книге
        /// </summary>
        public int WorksheetCount
        {
            get { return workbook.Worksheets.Count; }
        }

        /// <summary>
        /// Возвращает список имен листов
        /// </summary>
        public List<string> WorksheetNames
        {
            get
            {
                Excel.Worksheet worksheetName;

                List<string> names = new List<string>();

                for (int i = 0; i < workbook.Sheets.Count; i++)
                {
                    worksheetName = (Excel.Worksheet)sheets.get_Item(i + 1);
                    names.Add(worksheetName.Name);
                }

                return names;
            }
        }

        /// <summary>
        /// Returns whether a worksheet has any values or not. Cells with white space are considered to be empty
        /// </summary>
        public bool WorksheetBlank
        {
            get
            {
                Excel.Range rangeBlank = worksheet.UsedRange;

                if (rangeBlank.Count == 1 && rangeBlank.Value2 == null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

    }

    /// <summary>
    /// Границы ячейки
    /// </summary>
    public enum BorderLineStyleType
    {
        Continuous = Excel.XlLineStyle.xlContinuous,
        Dash = Excel.XlLineStyle.xlDash,
        DashDot = Excel.XlLineStyle.xlDashDot,
        DashDotDot = Excel.XlLineStyle.xlDashDotDot,
        Dot = Excel.XlLineStyle.xlDot,
        Double = Excel.XlLineStyle.xlDouble,
        SlantDashDot = Excel.XlLineStyle.xlSlantDashDot,
        None = Excel.XlLineStyle.xlLineStyleNone
    }

    public enum BorderWeights
    {
        Hairline = Excel.XlBorderWeight.xlHairline,
        Medium = Excel.XlBorderWeight.xlMedium,
        Thick = Excel.XlBorderWeight.xlThick,
        Thin = Excel.XlBorderWeight.xlThin,
        None = 0
    }

    public struct BorderType
    {
        public BorderWeights Weight;
        public BorderLineStyleType LineStyle;
    }

    public struct CellBorders
    {
        public BorderType Left;
        public BorderType Right;
        public BorderType Top;
        public BorderType Bottom;

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
        Bottom = Excel.XlVAlign.xlVAlignBottom,
        Center = Excel.XlVAlign.xlVAlignCenter,
        Distributed = Excel.XlVAlign.xlVAlignDistributed,
        Justify = Excel.XlVAlign.xlVAlignJustify,
        Top = Excel.XlVAlign.xlVAlignTop
    }

    /// <summary>
    /// Горизонтальное выравнивание
    /// </summary>
    public enum HAlign
    {
        Right = Excel.XlHAlign.xlHAlignRight,
        Left = Excel.XlHAlign.xlHAlignLeft,
        Justify = Excel.XlHAlign.xlHAlignJustify,
        Distributed = Excel.XlHAlign.xlHAlignDistributed,
        Center = Excel.XlHAlign.xlHAlignCenter,
        None = Excel.XlHAlign.xlHAlignGeneral,
        Fill = Excel.XlHAlign.xlHAlignFill,
        CenterAcrossSelection = Excel.XlHAlign.xlHAlignCenterAcrossSelection
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

    public enum PageOrientation
    {
        Landskape = Excel.XlPageOrientation.xlLandscape,
        Portrait = Excel.XlPageOrientation.xlPortrait
    }

    public enum PaperSize
    {
        PaperA4 = Excel.XlPaperSize.xlPaperA4,
        PaperA3 = Excel.XlPaperSize.xlPaperA3
    }

    public struct WorkSheetPageSettings
    {
        public PageOrientation Orientation;
        public PaperSize PaperSize;
        public int FitToPagesWide;
        public int FitToPagesTall;
        public double LeftMarginMM;
        public double RightMarginMM;
        public double TopMarginMM;
        public double BottomMarginMM;
        public int fromRow;
        public int fromColumn;
        public int toRow;
        public int toColumn;
    }

}
