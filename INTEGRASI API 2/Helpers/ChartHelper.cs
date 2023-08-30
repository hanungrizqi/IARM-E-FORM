using INTEGRASI_API_2.ViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.Helpers
{
    public class ChartHelper
    {
        public static List<InitDataSet> InitChartDataSet()
        {
            var list = new List<InitDataSet>
            {
                new InitDataSet
                {
                    label = "Sudah Submit",
                    backgroundColor = "rgb(75, 192, 192)",
                    categoryPercentage = 0.5,
                    isSubmit = true
                    
                },
                new InitDataSet
                {
                    label = "Belum Submit",
                    backgroundColor = "rgb(255, 99, 132)",
                    categoryPercentage = 0.75,
                    isSubmit = false
                }
            };
            return list;
        }
    }
}