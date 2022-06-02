using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Media;

using EPPlusWrapper;

namespace Shared.Report
{
    public partial class ReportExporter
    {
        public static void ReportToExcel(Report report, string ReportFileName)
        {
            if (File.Exists(ReportFileName))
            {
                File.Delete(ReportFileName);
            }

            if (report == null) throw new ArgumentNullException("Report is null");
            if (report.ColumnDefinition == null) throw new ArgumentNullException("Reports ColumnDefinition is null");
            if (report.RowDefinition == null) throw new ArgumentNullException("Reports RowDefinition is null");

            int rows = report.RowDefinition.Count, columns = report.ColumnDefinition.Count;

            Wrapper wrapper = new Wrapper();
            
            WorkSheetPageSettings ps = new WorkSheetPageSettings()
            {
                fromRow = 1,
                fromColumn = 1,
                toColumn = report.ColumnDefinition.Count,
                toRow = report.RowDefinition.Count,
                LeftMarginMM = 15,
                BottomMarginMM = 15,
                RightMarginMM = 15,
                TopMarginMM = 20,
                PaperSize = PaperSize.PaperA4,
                Orientation = PageOrientation.Landskape,
                FitToPagesWide = 1
            };

            //wrapper.SetPageSettings(ps);

            //
            LoadStyles();

            wrapper.SetCellValue("A1", report.Header, 
                    GetCellStyleFromItemStyle(ReportItemStyle.TableHeaderCellStyle.ToTextBoxStyle(resDictStyles)));
            wrapper.MergeRangeR1C1(1, 1, 1, columns);

            // ширина столбцов
            for (int index = 0; index < report.ColumnDefinition.Count; index++)
            {
                switch (report.ColumnDefinition[index].Width.GridUnitType)
                {
                    case GridUnitType.Auto:
                        wrapper.SetColumnWidth(index + 1, 0);
                        break;
                    case GridUnitType.Pixel:
                        wrapper.SetColumnWidth(index + 1, report.ColumnDefinition[index].Width.Value);
                        break;
                    case GridUnitType.Star:
                        wrapper.SetColumnWidth(index + 1, 0);
                        break;
                }
            }
            wrapper.SetRowHeight(1, 35);

            // ячейки
            foreach (ReportItem item in report.Items)
            {
                AddCellToExcel(ref wrapper, item.Caption, item.Row + 1, item.Column + 1, item.RowSpan, item.ColumnSpan,
                               item.Border, item.Style);
            }
           
            // высота строк
            for (int index = 1; index < report.RowDefinition.Count; index++)
            {
                wrapper.SetRowHeight(index + 1, 0);
            }

            wrapper.SaveAs(ReportFileName);
        }

        private static void AddCellToExcel(ref Wrapper wrapper,
                                            string content,
                                            int row, int column,
                                            int rowSpan, int columnSpan,
                                            Thickness bordersWeight,
                                            ReportItemStyle cellStyleName)
        {
            CellStyle cellStyle = GetCellStyleFromItemStyle(cellStyleName.ToTextBoxStyle(resDictStyles));

            wrapper.SetCellValue(row, column, rowSpan, columnSpan, content, cellStyle);
            
            CellBorders borders = new CellBorders();

            borders.Left = CreateBorderFromThickness(bordersWeight.Left);
            borders.Right = CreateBorderFromThickness(bordersWeight.Right);
            borders.Top = CreateBorderFromThickness(bordersWeight.Top);
            borders.Bottom = CreateBorderFromThickness(bordersWeight.Bottom);

            wrapper.SetBorders(borders);
        }

        private static BorderType CreateBorderFromThickness(double thickness)
        {
            BorderType border = new BorderType();
            if (thickness == 0.0)
            {
                border.LineStyle = BorderLineStyleType.None;
                border.Weight = BorderWeights.None;
            } else
                if (thickness == 1.0)
                {
                    border.LineStyle = BorderLineStyleType.None;
                    border.Weight = BorderWeights.Thin;
                }
                else
                    if (thickness == 2.0)
                    {
                        border.LineStyle = BorderLineStyleType.None;
                        border.Weight = BorderWeights.Medium;
                    }
                    else
                        {
                            border.LineStyle = BorderLineStyleType.None;
                            border.Weight = BorderWeights.None;
                        }
            return border;
        }

        private static CellStyle GetCellStyleFromItemStyle(Style itemStyle)
        {
            CellStyle cellStyle = new CellStyle();
            cellStyle.SetDefaultCellStyle();

            ParseStyle(ref itemStyle, ref cellStyle);

            return cellStyle;
        }

        private static void ParseStyle(ref Style style, ref CellStyle cellStyle)
        {
            Style baseStyle;
            if (style.BasedOn != null)
            {
                baseStyle = style.BasedOn;
                ParseStyle(ref baseStyle, ref cellStyle);
            }
            
            foreach (Setter setter in style.Setters)
            {
                switch (setter.Property.Name)
                {
                    case "Foreground":
                        cellStyle.Foreground = System.Drawing.ColorTranslator.FromHtml(setter.Value.ToString());
                        break;
                    case "Background":
                        cellStyle.Background = System.Drawing.ColorTranslator.FromHtml(setter.Value.ToString());
                        break;
                    case "Padding":
                        break;
                    case "Margin":
                        break;
                    case "FontFamily":
                        cellStyle.FontStyle.FontFamily = setter.Value.ToString();
                        break;
                    case "FontSize":
                        cellStyle.FontStyle.FontSize = (double)setter.Value;
                        Graphics g = Graphics.FromHwnd(IntPtr.Zero);
                        cellStyle.FontStyle.FontSize = cellStyle.FontStyle.FontSize * 72 / g.DpiX;
                        g.Dispose();
                        break;
                    case "TextAlignment":
                        TextAlignment ta;
                        if (Enum.IsDefined(typeof(TextAlignment), setter.Value))
                            ta = (TextAlignment)setter.Value;
                        else
                            ta = TextAlignment.Center;
                        switch (ta)
                        {
                            case TextAlignment.Center:
                                cellStyle.HorizontalAlignment = HAlign.Center;
                                break;
                            case TextAlignment.Left:
                                cellStyle.HorizontalAlignment = HAlign.Left;
                                break;
                            case TextAlignment.Right:
                                cellStyle.HorizontalAlignment = HAlign.Right;
                                break;
                        }
                        break;
                    case "HorizontalContentAlignment":
                        break;
                    case "FontWeight":
                        FontWeight fw = (FontWeight)setter.Value;
                        cellStyle.FontStyle.FontWeight = fw;
                        break;
                    case "FontStyle":
                        System.Windows.FontStyle fs = (System.Windows.FontStyle)setter.Value;
                        cellStyle.FontStyle.FontStyle = fs;
                        break;
                    case "TextWrapping":
                        TextWrapping tw;
                        if (Enum.IsDefined(typeof(TextWrapping), setter.Value))
                            tw = (TextWrapping)setter.Value;
                        else
                            tw = TextWrapping.NoWrap;
                        cellStyle.WrapText = tw == TextWrapping.Wrap;
                        break;
                    case "VerticalAlignment":
                        VerticalAlignment va;
                        if (Enum.IsDefined(typeof(VerticalAlignment), setter.Value))
                            va = (VerticalAlignment)setter.Value;
                        else
                            va = VerticalAlignment.Center;
                        switch (va)
                        {
                            case VerticalAlignment.Center:
                                cellStyle.VerticalAlignment = VAlign.Center;
                                break;
                            case VerticalAlignment.Bottom:
                                cellStyle.VerticalAlignment = VAlign.Bottom;
                                break;
                            case VerticalAlignment.Top:
                                cellStyle.VerticalAlignment = VAlign.Top;
                                break;
                            case VerticalAlignment.Stretch:
                                cellStyle.VerticalAlignment = VAlign.Justify;
                                break;
                        }
                        break;
                    case "HorizontalAlignment":
                        break;
                }
            }        
        }
    }
}
