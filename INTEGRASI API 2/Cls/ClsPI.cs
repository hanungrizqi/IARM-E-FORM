using INTEGRASI_API_2.Helpers;
using INTEGRASI_API_2.Models;
using INTEGRASI_API_2.ViewModels;
using INTEGRASI_API_2.ViewModels.Chart;
using INTEGRASI_API_2.ViewModels.Ebek;
using INTEGRASI_API_2.ViewModels.PI;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INTEGRASI_API_2.Cls
{
    public class ClsPI
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();

        public bool IsUserHasSubmitPi(string Nrp)
        {
            var userData = db.VW_ALL_USERs.Where(x => x.EMPLOYEE_ID == Nrp).FirstOrDefault();
            return userData.PI_SUBMIT == null ? false : userData.PI_SUBMIT.Value;
        }

        public PiDataResponseVM GetDataPI(string Nrp)
        {
            PiDataResponseVM responseVM = new PiDataResponseVM();
            var userData = db.VW_ALL_USERs.Where(x => x.EMPLOYEE_ID == Nrp).FirstOrDefault();
            responseVM.PiNo = GenerateNoPI();
            responseVM.Nrp = userData.EMPLOYEE_ID;
            responseVM.Name = userData.NAME;
            responseVM.Dept = userData.DEPT_DESC;
            responseVM.IsSubmitted = userData.PI_SUBMIT.HasValue ? userData.PI_SUBMIT.Value : false;
            return responseVM;
        }

        public string GenerateNoPI()
        {
            int number;
            string piNumber;
            var nowDateMonth = DateTime.Now.ToString("MM");
            var nowDateYear = DateTime.Now.ToString("yyyy");
            var findPiNumber = "INTEG" + "/" + nowDateYear + "/" + nowDateMonth;
            var latestPiNumber = db.TBL_T_PIs.Where(x => x.PI_NO.Contains(findPiNumber))
                                                .OrderByDescending(x => x.PI_NO)
                                                .Select(x => x.PI_NO)
                                                .FirstOrDefault();
            if (latestPiNumber == null)
            {
                piNumber = "06/0001/INTEG/" + nowDateYear + "/" + nowDateMonth;
            }
            else
            {
                number = (int.Parse(latestPiNumber.Substring(3, 4)) + 1);
                piNumber = "06/" + number.ToString().PadLeft(4, '0') + "/INTEG/" + nowDateYear + "/" + nowDateMonth;
            }
            return piNumber;
        }

        public StatusResponseVM SubmitPaktaIntegritas(SubmitPIDataRequestVM requestVM)
        {
            try
            {
                TBL_T_PI newData = new TBL_T_PI()
                {
                    NRP = requestVM.UserNrp,
                    SUBMIT = true,
                    SIGN_LOCATION = requestVM.Location,
                    SUBMITDATE = DateTime.Now
                };
                db.TBL_T_PIs.InsertOnSubmit(newData);
                db.SubmitChanges();

                var responseVM = new StatusResponseVM()
                {
                    Success = true,
                    Message = "Data berhasil disubmit"
                };

                return responseVM;
            }
            catch (Exception ex)
            {
                var responseVM = new StatusResponseVM()
                {
                    Success = false,
                    Message = $"Terjadi kesalahan: {ex.Message}"
                };
                return responseVM;
            }
        }

        public List<VW_PI_REPORT> GetDataTablePI()
        {
            var listDataPI = db.VW_PI_REPORTs.ToList();
            return listDataPI;
        }

        public List<VW_PI_REPORT> GetFilteredDataTablePI(PIFilterRequestsVM requestsVM)
        {
            var listDataPI = db.VW_PI_REPORTs.AsQueryable();
            if (!requestsVM.Nama.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.NAME.Contains(requestsVM.Nama));
            }

            if (!requestsVM.District.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.SITE == requestsVM.District);
            }

            if (!requestsVM.Dept.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.DEPT == requestsVM.Dept);
            }

            return listDataPI.ToList();
        }

        public ChartResponseVM GetChartPI(ChartRequestVM requests)
        {
            var dataInit = ChartHelper.InitChartDataSet();
            ChartResponseVM dataResponse = new ChartResponseVM()
            {
                labels = new List<string>(),
                datasets = new List<ChartDataSet>()
            };

            var baseData = db.VW_ALL_USERs.Where(x => x.DSTRCT_CODE != null && x.SECTION_HEAD != null).ToList();

            if (!requests.Dept.IsNullOrWhiteSpace() && !requests.District.IsNullOrWhiteSpace())
            {
                baseData = baseData.Where(x => x.DEPT_DESC == requests.Dept && x.DSTRCT_CODE == requests.District).ToList();
                var customLabel = ($"{requests.District} - {requests.Dept}");
                dataResponse.labels.Add(customLabel);

                foreach (var dataSet in dataInit)
                {
                    var countData = baseData.Where(x => x.PI_SUBMIT == dataSet.isSubmit).Count();
                    var newData = new ChartDataSet
                    {
                        label = dataSet.label,
                        backgroundColor = dataSet.backgroundColor,
                        categoryPercentage = dataSet.categoryPercentage,
                        data = new List<int>()
                    };
                    int totalData = 0;
                    if (dataSet.isSubmit)
                    {
                        totalData = baseData.Where(x => x.PI_SUBMIT == dataSet.isSubmit).Count();
                    }
                    else
                    {
                        totalData = baseData.Where(x => x.PI_SUBMIT == dataSet.isSubmit || x.EBEK_SUBMIT == null).Count();
                    }
                    newData.data.Add(totalData);
                    dataResponse.datasets.Add(newData);
                }

                return dataResponse;
            }
            else if (!requests.Dept.IsNullOrWhiteSpace())
            {
                baseData = baseData.Where(x => x.DEPT_DESC == requests.Dept).ToList();
                var listDistrict = baseData.Select(x => x.DSTRCT_CODE).Distinct().ToList();
                dataResponse.labels = listDistrict;

                foreach (var dataSet in dataInit)
                {
                    var newData = new ChartDataSet
                    {
                        label = dataSet.label,
                        backgroundColor = dataSet.backgroundColor,
                        categoryPercentage = dataSet.categoryPercentage,
                        data = new List<int>()
                    };

                    foreach (var district in listDistrict)
                    {
                        int totalData = 0;
                        if (dataSet.isSubmit)
                        {
                            totalData = baseData.Where(x => x.DSTRCT_CODE == district && x.PI_SUBMIT == dataSet.isSubmit).Count();
                        }
                        else
                        {
                            totalData = baseData.Where(x => x.DSTRCT_CODE == district && (x.PI_SUBMIT == dataSet.isSubmit || x.PI_SUBMIT == null)).Count();
                        }
                        newData.data.Add(totalData);
                    }
                    dataResponse.datasets.Add(newData);
                }

                return dataResponse;
            }
            else if (!requests.District.IsNullOrWhiteSpace())
            {
                baseData = baseData.Where(x => x.DSTRCT_CODE == requests.District).ToList();
                var listDept = baseData.Select(x => x.DEPT_DESC).Distinct().ToList();
                dataResponse.labels = listDept;

                foreach (var dataSet in dataInit)
                {
                    var newData = new ChartDataSet
                    {
                        label = dataSet.label,
                        backgroundColor = dataSet.backgroundColor,
                        categoryPercentage = dataSet.categoryPercentage,
                        data = new List<int>()
                    };

                    foreach (var dept in listDept)
                    {
                        int totalData = 0;
                        if (dataSet.isSubmit)
                        {
                            totalData = baseData.Where(x => x.DEPT_DESC == dept && x.PI_SUBMIT == dataSet.isSubmit).Count();
                        }
                        else
                        {
                            totalData = baseData.Where(x => x.DEPT_DESC == dept && (x.PI_SUBMIT == dataSet.isSubmit || x.PI_SUBMIT == null)).Count();
                        }
                        newData.data.Add(totalData);
                    }
                    dataResponse.datasets.Add(newData);
                }
                return dataResponse;
            }
            else
            {
                var listDistrict = baseData.Select(x => x.DSTRCT_CODE).Distinct().ToList();
                dataResponse.labels = listDistrict;

                foreach (var dataSet in dataInit)
                {
                    var newData = new ChartDataSet
                    {
                        label = dataSet.label,
                        backgroundColor = dataSet.backgroundColor,
                        categoryPercentage = dataSet.categoryPercentage,
                        data = new List<int>()
                    };

                    foreach (var district in listDistrict)
                    {
                        int totalData = 0;
                        if (dataSet.isSubmit)
                        {
                            totalData = baseData.Where(x => x.DSTRCT_CODE == district && x.PI_SUBMIT == dataSet.isSubmit).Count();
                        }
                        else
                        {
                            totalData = baseData.Where(x => x.DSTRCT_CODE == district && (x.PI_SUBMIT == dataSet.isSubmit || x.PI_SUBMIT == null)).Count();
                        }
                        newData.data.Add(totalData);
                    }
                    dataResponse.datasets.Add(newData);
                }

                return dataResponse;
            }
        }
    }
}