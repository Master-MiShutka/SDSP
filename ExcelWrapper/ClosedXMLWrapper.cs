using System;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

using ClosedXML.Excel;

namespace ClosedXMLWrapper
{
   
    /// <summary>
    /// 
    /// </summary>
    public class Wrapper
    {
        #region Variables

        ClosedXML.Excel.XLWorkbook activeWorkBook;
        ClosedXML.Excel.IXLWorksheet activeSheet;
        ClosedXML.Excel.IXLRange activeRange;

        #endregion

        #region Constructor
        /// <summary>
        /// �����������
        /// </summary>
        public Wrapper()
        {
            activeWorkBook = new XLWorkbook();
            AddWorksheet("���� 1");            

            // ��������
            activeWorkBook.Properties.Author = "������� ���� ������ ��������";
            activeWorkBook.Properties.Title = "�������� ������ ����";
            activeWorkBook.Properties.Category = "�������� ������ ����";
            activeWorkBook.Properties.Keywords = "����, ���";
            activeWorkBook.Properties.Comments = "��� ������������";
            activeWorkBook.Properties.Company = "����� ����� �������������� ��������� ��";

            //
            activeWorkBook.ShowGridLines = false;
            activeWorkBook.EventTracking = XLEventTracking.Disabled;
		}
        #endregion

        #region Methods

        /// <summary>
        /// ���������� �������� ���������
        /// </summary>
        public void Save()
        {
            activeWorkBook.EventTracking = XLEventTracking.Enabled;
            activeWorkBook.Save();
        }

        /// <summary>
        /// ���������� �������� ���������
        /// </summary>
        public void SaveAs(string fileName)
        {
            activeWorkBook.SaveAs(fileName);
        }

        /// <summary>
        /// ��������� �������������� �� ���������
        /// </summary>
        public void SetDefaultStyles()
        {
            activeWorkBook.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            activeWorkBook.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }

        /// <summary>
        /// ���������� �����
        /// </summary>
        /// <param name="name"></param>
        public void AddWorksheet(string name)
        {
            activeSheet = activeWorkBook.Worksheets.Add(name);
            activeRange = activeSheet.Range("A1");
        }

        private void SetPageSetupHeaderFooterStyle(IXLRichString text,
                                                   XLColor Color,
                                                   string FontName,
                                                   double FontSize,
                                                   bool Bold = false,
                                                   bool Italic = false)
        {
            text.FontColor = Color;
            text.FontName = FontName;
            text.FontSize = FontSize;
            text.Bold = Bold;
            text.Italic = Italic;
        }

        /// <summary>
        /// ��������� ���������� ��������
        /// </summary>
        /// <param name="pageSettings"></param>
        public void SetPageSettings(WorkSheetPageSettings pageSettings)
        {
            activeSheet.PageSetup.CenterHorizontally = true;

            SetPageSetupHeaderFooterStyle(activeSheet.PageSetup.Header.Right.AddText(XLHFPredefinedText.Date),
                XLColor.DarkGray, "Arial", 10, false, true);
            SetPageSetupHeaderFooterStyle(activeSheet.PageSetup.Header.Right.AddText(" - "),
                XLColor.DarkGray, "Arial", 10, false, true);
            SetPageSetupHeaderFooterStyle(activeSheet.PageSetup.Header.Right.AddText(XLHFPredefinedText.Time),
                XLColor.DarkGray, "Arial", 10, false, true);

            SetPageSetupHeaderFooterStyle(activeSheet.PageSetup.Footer.Left.AddText("������� �� ���"),
                XLColor.DarkGray, "Arial", 10, false, true);
            SetPageSetupHeaderFooterStyle(activeSheet.PageSetup.Footer.Center.AddText("__________"),
                XLColor.DarkGray, "Arial", 10, false, true);
            SetPageSetupHeaderFooterStyle(activeSheet.PageSetup.Footer.Right.AddText("���� �.�."),
                XLColor.DarkGray, "Arial", 10, false, true);

            activeSheet.PageSetup.PageOrientation = (XLPageOrientation)pageSettings.Orientation;
            activeSheet.PageSetup.AdjustTo(pageSettings.Zoom);
            activeSheet.PageSetup.PaperSize = (XLPaperSize)pageSettings.PaperSize;
            activeSheet.PageSetup.VerticalDpi = 600;
            activeSheet.PageSetup.HorizontalDpi = 600;

            if (pageSettings.Zoom != 100)
            {
                activeSheet.PageSetup.PagesWide = pageSettings.FitToPagesWide;
                activeSheet.PageSetup.PagesTall = pageSettings.FitToPagesTall;
            }
            else
                activeSheet.PageSetup.SetScale(pageSettings.Zoom);

            activeSheet.PageSetup.Margins.SetLeft(pageSettings.LeftMarginMM);
            activeSheet.PageSetup.Margins.SetRight(pageSettings.RightMarginMM);
            activeSheet.PageSetup.Margins.SetTop(pageSettings.TopMarginMM);
            activeSheet.PageSetup.Margins.SetBottom(pageSettings.BottomMarginMM);

            activeSheet.PageSetup.PrintAreas.Clear();
            activeSheet.PageSetup.PrintAreas.Add(activeSheet.Range(pageSettings.fromRow,
                                                                   pageSettings.fromColumn,
                                                                   pageSettings.toRow,
                                                                   pageSettings.toColumn).ToString());
        }

        /// <summary>
        /// ���������� �����
        /// </summary>
        /// <param name="styleName">�������� �����</param>
        /// <param name="cellStyle">����� ������</param>
        public void SetCellStyle(CellStyle cellStyle, IXLStyle style)
        {
            style.Alignment.WrapText = cellStyle.WrapText;
            style.Alignment.Horizontal = (XLAlignmentHorizontalValues)cellStyle.HorizontalAlignment;
            style.Alignment.Vertical = (XLAlignmentVerticalValues)cellStyle.VerticalAlignment;

            style.Fill.BackgroundColor = XLColor.FromColor(cellStyle.Background);
            style.Font.FontColor = XLColor.FromColor(cellStyle.Foreground);

            style.Font.FontName = cellStyle.FontStyle.FontFamily;
            style.Font.FontSize = cellStyle.FontStyle.FontSize;
            style.Font.Italic = cellStyle.FontStyle.FontStyle == FontStyles.Italic;
            style.Font.Bold = cellStyle.FontStyle.FontWeight == FontWeights.Bold;

            style.Border.InsideBorder = XLBorderStyleValues.None;
            style.Border.OutsideBorder = XLBorderStyleValues.None;
            style.Border.DiagonalBorder = XLBorderStyleValues.None;
        }

        /// <summary>
        /// �������� ����� � ��������� ��������
        /// </summary>
        /// <param name="index">�����</param>
        public void DeleteWorksheet(int index)
        {
            if (index < 1)
                throw new Exception("Index out of bounds");
            if (index > activeWorkBook.Worksheets.Count)
                throw new Exception("Index out of bounds");

            activeWorkBook.Worksheets.Delete(index);
        }

        /// <summary>
        /// �������� ����� �� �����
        /// </summary>
        /// <param name="workSheetName"></param>
        public void DeleteWorksheet(string workSheetName)
        {
            activeWorkBook.Worksheets.Delete(workSheetName);
        }

        /// <summary>
        /// ��������� �������� ����� �� �����
        /// </summary>
        /// <param name="workSheetName">��������</param>
        public bool SetCurrentWorksheet(string workSheetName)
        {
            return activeWorkBook.Worksheets.TryGetWorksheet(workSheetName, out activeSheet);
        }

        /// <summary>
        /// ��������� �������� ����� �� �������
        /// </summary>
        /// <param name="index">�����</param>
        public void SetCurrentWorksheet(int index)
        {
            if (index < 1)
                throw new Exception("Index out of bounds");
            if (index > activeWorkBook.Worksheets.Count)
                throw new Exception("Index out of bounds");

            activeSheet = activeWorkBook.Worksheets.Worksheet(index);
        }

        /// <summary>
        /// ��������� ������ �������� ���������
        /// </summary>
        /// <param name="borders">�������</param>
        public void SetBorders(CellBorders borders)
        {
            if (borders.Left.Weight != BorderWeights.None)
                activeRange.Style.Border.LeftBorder = (XLBorderStyleValues)borders.Left.LineStyle | (XLBorderStyleValues)borders.Left.Weight;

            if (borders.Right.Weight != BorderWeights.None)
                activeRange.Style.Border.RightBorder = (XLBorderStyleValues)borders.Right.LineStyle | (XLBorderStyleValues)borders.Right.Weight;

            if (borders.Top.Weight != BorderWeights.None)
                activeRange.Style.Border.TopBorder = (XLBorderStyleValues)borders.Top.LineStyle | (XLBorderStyleValues)borders.Top.Weight;

            if (borders.Bottom.Weight != BorderWeights.None)
                activeRange.Style.Border.BottomBorder = (XLBorderStyleValues)borders.Bottom.LineStyle | (XLBorderStyleValues)borders.Bottom.Weight;

            activeRange.Style.Border.DiagonalBorder = XLBorderStyleValues.None;
            activeRange.Style.Border.DiagonalBorder = XLBorderStyleValues.None;
        }

        /// <summary>
        /// ����������� ����� ���������
        /// </summary>
        /// <param name="from">������� � ������</param>
        /// <param name="to">�� ������</param>
        public void MergeRange(string from, string to)
        {
            activeSheet.Range(from, to).Merge();
        }

        /// <summary>
        /// ����������� ����� ���������
        /// </summary>
        /// <param name="fromR">������� �� ������</param>
        /// <param name="fromC">������� �� �������</param>
        /// <param name="toR">���������� �������</param>
        /// <param name="toC">���������� ��������</param>
        public void MergeRangeR1C1(int fromR, int fromC, int toR, int toC)
        {
            activeSheet.Range(fromR, fromC, toR, toC).Merge();
        }

        /// <summary>
        /// ��������� �������� ���������
        /// </summary>
        /// <param name="from">� ������</param>
        /// <param name="to">�� ������</param>
        public void SetRange(string from, string to)
        {
            activeRange = activeSheet.Range(from, to);
        }

        /// <summary>
        /// ��������� �������� ���������
        /// </summary>
        /// <param name="fromR">������� �� ������</param>
        /// <param name="fromC">������� �� �������</param>
        /// <param name="toR">���������� �������</param>
        /// <param name="toC">���������� ��������</param>
        public void SetRange(int fromR, int fromC, int toR, int toC)
        {
            activeRange = activeSheet.Range(fromR, fromC, toR, toC);
        }

        /// <summary>
        /// ��������� �������� ���������
        /// </summary>
        /// <param name="value">��������</param>
        public void SetRangeValue(string value)
        {
            activeRange.Value = value;
        }

        /// <summary>
        /// ��������� �������� ���������
        /// </summary>
        /// <param name="value">��������</param>
        public void SetRangeValue(string[,] value)
        {
            activeRange.Value = value;
        }

        /// <summary>
        /// ���������� �������� ������
        /// </summary>
        /// <param name="location">�����</param>
        public object GetCellValue(string location)
        {
            return activeSheet.Cell(location).Value;
        }

        /// <summary>
        /// ���������� �������� ��������� � ���� �������
        /// </summary>
        /// <param name="location">�����</param>
        /// <returns>��������� ������</returns>
        public string[,] GetRangeValue(string location)
        {
            return (string[,])activeSheet.Cell(location).Value;
        }

        /// <summary>
        /// ��������� �������� � ����� ������
        /// </summary>
        /// <param name="location">�����</param>
        /// <param name="value">��������</param>
        /// <param name="style">��� �����</param>
        public void SetCellValue(string location, string value, CellStyle style)
        {
            activeRange = activeSheet.Range(location);
            activeRange.Value = value;
            SetCellStyle(style, activeRange.Style);
        }
        /// <summary>
        /// ��������� �������� � ����� ������
        /// </summary>
        /// <param name="row">����� ������</param>
        /// <param name="column">����� �������</param>
        /// <param name="value">��������</param>
        /// <param name="style">��� �����</param>
        public void SetCellValue(int row, int column, string value, CellStyle style)
        {
            activeRange = activeSheet.Range(row, column, row, column);
            activeRange.Value = value;
            SetCellStyle(style, activeRange.Style);
        }
        /// <summary>
        /// ��������� �������� � ����� ������ � ���������� ��������
        /// </summary>
        /// <param name="row">����� ������</param>
        /// <param name="column">����� �������</param>
        /// <param name="rowSpan">������ ��������</param>
        /// <param name="columnSpan">������ ���������</param>
        /// <param name="value">��������</param>
        /// <param name="style">��� �����</param>
        public void SetCellValue(int row, int column, int rowSpan, int columnSpan, string value, CellStyle style)
        {
            activeRange = activeSheet.Range(row, column, row, column);
            SetCellStyle(style, activeRange.Style);

            activeRange.Style.Border.LeftBorder = XLBorderStyleValues.None;
            activeRange.Style.Border.RightBorder = XLBorderStyleValues.None;
            activeRange.Style.Border.TopBorder = XLBorderStyleValues.None;
            activeRange.Style.Border.BottomBorder = XLBorderStyleValues.None;

            activeRange.Style.Border.DiagonalBorder = XLBorderStyleValues.None;
            activeRange.Style.Border.DiagonalBorder = XLBorderStyleValues.None;

            activeRange.Value = value;
            SetRange(row, column, row + rowSpan - 1, column + columnSpan - 1);
            if (rowSpan > 1 | columnSpan > 1)
                MergeRangeR1C1(row, column, row + rowSpan - 1, column + columnSpan - 1);
        }

        /// <summary>
        /// ��������� ������ ������
        /// </summary>
        /// <param name="rowIndex">����� ������</param>
        /// <param name="height">������</param>
        public void SetRowHeight(int rowIndex, double height)
        {
            if (height == 0)
                activeSheet.Row(rowIndex).AdjustToContents();
            else
                activeSheet.Row(rowIndex).Height = height;
        }
        /// <summary>
        /// ��������� ������ �����
        /// </summary>
        /// <param name="fromRowIndex">�� ������</param>
        /// <param name="toRowIndex">�� ������</param>
        /// <param name="height">������</param>
        public void SetRowHeight(int fromRowIndex, int toRowIndex, double height)
        {
            if (height == 0)
                activeSheet.Rows(fromRowIndex, toRowIndex).AdjustToContents();
            else
                activeSheet.Rows(fromRowIndex, toRowIndex).Height = height;
        }

        /// <summary>
        /// ��������� ������ �������
        /// </summary>
        /// <param name="columnIndex">����� �������</param>
        /// <param name="width">������</param>
        public void SetColumnWidth(int columnIndex, double width)
        {
            if (width == 0)
                activeSheet.Column(columnIndex).AdjustToContents();
            else
                activeSheet.Column(columnIndex).Width = width;
        }
        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        /// <param name="fromColumnIndex">�� �������</param>
        /// <param name="toColumnIndex">�� �������</param>
        /// <param name="width">������</param>
        public void SetColumnWidth(int fromColumnIndex, int toColumnIndex, double width)
        {
            if (width == 0)
                activeSheet.Columns(fromColumnIndex, toColumnIndex).AdjustToContents();
            else
                activeSheet.Columns(fromColumnIndex, toColumnIndex).Width = width;
        }

        #endregion
    }

    /// <summary>
    /// ��� ����� ������ ������
    /// </summary>
    public enum BorderLineStyleType
    {
        Continuous = 0,
        Dash = XLBorderStyleValues.Dashed,
        DashDot = XLBorderStyleValues.DashDot,
        DashDotDot = XLBorderStyleValues.DashDotDot,
        Dot = XLBorderStyleValues.Dotted,
        Double = XLBorderStyleValues.Double,
        SlantDashDot = XLBorderStyleValues.SlantDashDot,
        None = XLBorderStyleValues.None
    }

    /// <summary>
    /// ������� ����� ������ ������
    /// </summary>
    public enum BorderWeights
    {
        Hairline = XLBorderStyleValues.Hair,
        Medium = XLBorderStyleValues.Medium,
        Thick = XLBorderStyleValues.Thick,
        Thin = XLBorderStyleValues.Thin,
        None = XLBorderStyleValues.None
    }

    /// <summary>
    /// �������
    /// </summary>
    public struct BorderType
    {
        public BorderWeights Weight;
        public BorderLineStyleType LineStyle;
    }

    /// <summary>
    /// ������� ������
    /// </summary>
    public struct CellBorders
    {
        public BorderType Left;
        public BorderType Right;
        public BorderType Top;
        public BorderType Bottom;

        /// <summary>
        /// ��������� �������� �� ���������
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
    /// ������������ ������������
    /// </summary>
    public enum VAlign
    {
        Bottom = XLAlignmentVerticalValues.Bottom,
        Center = XLAlignmentVerticalValues.Center,
        Distributed = XLAlignmentVerticalValues.Distributed,
        Justify = XLAlignmentVerticalValues.Justify,
        Top = XLAlignmentVerticalValues.Top
    }

    /// <summary>
    /// �������������� ������������
    /// </summary>
    public enum HAlign
    {
        Right = XLAlignmentHorizontalValues.Right,
        Left = XLAlignmentHorizontalValues.Left,
        Justify = XLAlignmentHorizontalValues.Left,
        Distributed = XLAlignmentHorizontalValues.Distributed,
        Center = XLAlignmentHorizontalValues.Center,
        None = XLAlignmentHorizontalValues.General,
        Fill = XLAlignmentHorizontalValues.Fill,
        CenterAcrossSelection = XLAlignmentHorizontalValues.CenterContinuous
    }

    /// <summary>
    /// ����� ������
    /// </summary>
    public struct CellFontStyle
    {
        public string FontFamily;
        public double FontSize;
        public FontWeight FontWeight;
        public System.Windows.FontStyle FontStyle;
    }

    /// <summary>
    /// ����� ������
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
        /// ��������� �������� �� ���������
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
    /// ���������� �������� �����
    /// </summary>
    public enum PageOrientation
    {
        /// <summary>
        /// �����������
        /// </summary>
        Landskape = ClosedXML.Excel.XLPageOrientation.Landscape,
        /// <summary>
        /// �������
        /// </summary>
        Portrait = ClosedXML.Excel.XLPageOrientation.Portrait
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    public enum PaperSize
    {
        /// <summary>
        /// ������ ������� �4
        /// </summary>
        PaperA4 = ClosedXML.Excel.XLPaperSize.A4Paper,
        /// <summary>
        /// ������ ������� �3
        /// </summary>
        PaperA3 = ClosedXML.Excel.XLPaperSize.A3Paper
    }

    /// <summary>
    /// ��������� �������� �����
    /// </summary>
    public struct WorkSheetPageSettings
    {
        /// <summary>
        /// ���������
        /// </summary>
        public PageOrientation Orientation;
        /// <summary>
        /// ������
        /// </summary>
        public PaperSize PaperSize;
        /// <summary>
        /// ������� ���������� �� ��������� ����� ������ � ������
        /// </summary>
        public int FitToPagesWide;
        /// <summary>
        /// ������� ���������� �� ��������� ����� ������ � ������
        /// </summary>
        public int FitToPagesTall;
        /// <summary>
        /// ����� ������, ��
        /// </summary>
        public double LeftMarginMM;
        /// <summary>
        /// ������ ������, ��
        /// </summary>
        public double RightMarginMM;
        /// <summary>
        /// ������ ������, ��
        /// </summary>
        public double TopMarginMM;
        /// <summary>
        /// ������ �����, ��
        /// </summary>
        public double BottomMarginMM;
        /// <summary>
        /// ������� ������, ������� �� ������
        /// </summary>
        public int fromRow;
        /// <summary>
        /// ������� ������, ������� �� �������
        /// </summary>
        public int fromColumn;
        /// <summary>
        /// ������� ������, �� ������
        /// </summary>
        public int toRow;
        /// <summary>
        /// ������� ������, �� �������
        /// </summary>
        public int toColumn;
        /// <summary>
        /// �������
        /// </summary>
        public int Zoom;
    }
}