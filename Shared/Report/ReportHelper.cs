using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Shared.Report
{
    public static class ReportHelper
    {
        public static string IndicationsToString(float? indications)
        {
            if (indications != null & indications.HasValue)
            {
                return indications.Value.ToString("F0");
            }
            else
                return "нет";
        }

        public static string DateToString(DateTime? date)
        {
            if (date != null & date.HasValue) return date.Value.ToShortDateString() + " " + date.Value.ToLongTimeString();
            else return "неизвестно";
        }

        public static void ReportToExcel(Report report, ref Grid grid)
        {

        }

    }
}
