using INTEGRASI_API_2.Helpers;
using INTEGRASI_API_2.Models;
using INTEGRASI_API_2.ViewModels;
using INTEGRASI_API_2.ViewModels.Chart;
using INTEGRASI_API_2.ViewModels.Ebek;
using INTEGRASI_API_2.ViewModels.PI;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web;
using INTEGRASI_API_2.Constants;
using System.Configuration;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;

namespace INTEGRASI_API_2.Cls
{
    public class ClsPI
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();
        public string _cloudUploadUrl = ConfigurationManager.AppSettings["fileUploadPath"].ToString();
        public string _environment = ConfigurationManager.AppSettings["Environment"];

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

        public async Task<StatusResponseVM> SubmitPaktaIntegritas(SubmitPIDataRequestVM requestVM)
        {
            try
            {
                await Task.Run(() =>
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
                });

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

        public async Task<string> SignDocumentPI(string nrp/*, string name, string dept, string location, DateTime submitdate*/)
        {

            PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("Calibri", 12f), true);

            var userData = db.VW_PI_REPORTs.Where(x => x.NRP == nrp).FirstOrDefault();
            var insertData = db.TBL_T_PIs.Where(x => x.NRP == nrp).FirstOrDefault();

            // prepare document file
            var filePath = string.Empty;
            var signPath = string.Empty;
            var documentLinks = string.Empty;
            var ctx = HttpContext.Current;
            filePath = ctx.Server.MapPath($"~/File/PI_PDF.pdf");

            string newDocumentFile = null;

            await Task.Run(() =>
            {
                PdfDocument pdfDocument = new PdfDocument();
                pdfDocument.LoadFromFile(filePath);

                PdfPageBase pdfPage = pdfDocument.Pages[0];

                var newDocumentURl = System.IO.Path.GetDirectoryName(filePath);
                var currentDocumentFileName = System.IO.Path.GetFileName(filePath);
                string newDocumentLocation = ctx.Server.MapPath("~/Content/PI/");
                string newDocumentFileName = nrp + ".pdf";

                if (!Directory.Exists(newDocumentLocation))
                {
                    Directory.CreateDirectory(newDocumentLocation);
                }

                newDocumentFile = System.IO.Path.Combine(newDocumentLocation, newDocumentFileName);

                if (File.Exists(newDocumentFile))
                {
                    File.Delete(newDocumentFile);
                }

                pdfPage.Canvas.DrawString(userData.NRP, font, PdfBrushes.Black, new PointF(190, 198));
                pdfPage.Canvas.DrawString(userData.NAME, font, PdfBrushes.Black, new PointF(190, 175));
                pdfPage.Canvas.DrawString(userData.DEPT, font, PdfBrushes.Black, new PointF(190, 221));
                pdfPage.Canvas.DrawString(userData.SIGN_LOCATION, font, PdfBrushes.Black, new PointF(350, 672));
                pdfPage.Canvas.DrawString(userData.SUBMITDATE.Value.ToString("dd-MM-yyyy"), font, PdfBrushes.Black, new PointF(400, 672));
                pdfPage.Canvas.DrawString("(" + userData.NAME + ")", font, PdfBrushes.Black, new PointF(390, 747));

                pdfDocument.SaveToFile(newDocumentFile);

                newDocumentFile = newDocumentFile.Substring(newDocumentFile.LastIndexOf(@"Content\"));

                insertData.DOCUMENT_LINK = newDocumentFile;
                db.SubmitChanges();
            });

            return newDocumentFile;
        }
    }
}