using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shared.Report
{
    public class Report
    {
        public string Header { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Collection <ReportItem> Items { get; set; }

        public Collection<RowDefinition> RowDefinition { get; set; }
        public Collection<ColumnDefinition> ColumnDefinition { get; set; }

        public Report()
        {
            RowDefinition = new Collection<RowDefinition>();
            ColumnDefinition = new Collection<ColumnDefinition>();
            Items = new Collection<ReportItem>();
        }
    }

    public struct ReportItem
    {
        public string Caption;
        public ReportItemStyle Style;
        public int Row;
        public int Column;
        public int RowSpan;
        public int ColumnSpan;
        public Thickness Border;
    }

    public enum ReportItemStyle
    {
        BasicCellStyle,
        TableHeaderCellStyle,
        IntCellStyle,
        NullIntCellStyle,
        StringCellStyle,
        NullStringCellStyle,
        HeaderCellStyle,
        HeaderNumbersCellStyle,
        StringSummaryCellStyle,
        IntSummaryCellStyle
    }
}
