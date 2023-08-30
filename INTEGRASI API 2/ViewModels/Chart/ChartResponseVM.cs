using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.ViewModels.Chart
{
    public class ChartResponseVM
    {
        public List<string> labels { get; set; }
        public List<ChartDataSet> datasets { get; set; }
    }

    public class ChartDataSet
    {
        public string label { get; set; }
        public List<int> data { get; set; }
        public string backgroundColor { get; set; }
        public double categoryPercentage { get; set; }

    }

    public class InitDataSet
    {
        public string label { get; set; }
        public string backgroundColor { get; set; }
        public double categoryPercentage { get; set; }
        public bool isSubmit { get; set; }
    }
}