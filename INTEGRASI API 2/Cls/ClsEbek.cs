using INTEGRASI_API_2.Constanta;
using INTEGRASI_API_2.Models;
using INTEGRASI_API_2.ViewModels;
using INTEGRASI_API_2.ViewModels.Chart;
using INTEGRASI_API_2.ViewModels.Ebek;
using INTEGRASI_API_2.Helpers;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.Cls
{
    public class ClsEbek
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();

        public VW_ALL_USER GetUserData(string Nrp)
        {
            var userData = db.VW_ALL_USERs.Where(x => x.EMPLOYEE_ID == Nrp).FirstOrDefault();
            return userData;
        }

        public StatusResponseVM SubmitEbekData(EbekRequestsVM ebekRequestsVM)
        {
            var response = new StatusResponseVM();
            try
            {
                TBL_T_EBEK newEbekData = new TBL_T_EBEK()
                {
                    NRP = ebekRequestsVM.Nrp,
                    SUBMIT = true,
                    SIGN_LOCATION = ebekRequestsVM.Location,
                    SUBMITDATE = DateTime.Now,
                    ISHADFAMILY = !ebekRequestsVM.NoFamily,
                    ISHADMIKAD = !ebekRequestsVM.NoMikad,
                    ISHADCOI = !ebekRequestsVM.NoCoi,
                };

                db.TBL_T_EBEKs.InsertOnSubmit(newEbekData);
                db.SubmitChanges();

                if (!ebekRequestsVM.NoFamily && ebekRequestsVM.HadFamily.Count > 0)
                {
                    List<TBL_T_EBEK_FAMILY> listEbekFamily = new List<TBL_T_EBEK_FAMILY>();
                    foreach (var family in ebekRequestsVM.HadFamily)
                    {
                        TBL_T_EBEK_FAMILY newEbekFamily = new TBL_T_EBEK_FAMILY()
                        {
                            EBEK_ID = newEbekData.ID,
                            NAME = family.MemberName,
                            POSITION = family.MemberPos,
                            DEPT = family.MemberDept,
                            RELATION = family.MemberRelation
                        };
                        listEbekFamily.Add(newEbekFamily);
                    }
                    db.TBL_T_EBEK_FAMILies.InsertAllOnSubmit(listEbekFamily);
                    db.SubmitChanges();
                }

                if (!ebekRequestsVM.NoMikad && ebekRequestsVM.HadMikad.Count > 0)
                {
                    List<TBL_T_EBEK_MIKAD> listEbekMikad = new List<TBL_T_EBEK_MIKAD>();
                    foreach (var mikad in ebekRequestsVM.HadMikad)
                    {
                        TBL_T_EBEK_MIKAD newEbekMikad = new TBL_T_EBEK_MIKAD()
                        {
                            EBEK_ID = newEbekData.ID,
                            NAME = mikad.MemberName,
                            POSITION = mikad.MemberPos,
                            DEPT = mikad.MemberDept,
                            RELATION = mikad.MemberRelation
                        };
                        listEbekMikad.Add(newEbekMikad);
                    }
                    db.TBL_T_EBEK_MIKADs.InsertAllOnSubmit(listEbekMikad);
                }

                if (!ebekRequestsVM.NoCoi && ebekRequestsVM.HadCoi != null)
                {
                    List<TBL_T_EBEK_COI> listEbekCoi = new List<TBL_T_EBEK_COI>();
                    TBL_T_EBEK_COI coiBusinessRelation = new TBL_T_EBEK_COI()
                    {
                        EBEK_ID = newEbekData.ID,
                        CATEGORY = CoiConstant.BusinessRelation,
                        COMPANY = ebekRequestsVM.HadCoi.BusinessRelation.MemberCompany,
                        POSITION = ebekRequestsVM.HadCoi.BusinessRelation.MemberPosition,
                        INFORMATION = ebekRequestsVM.HadCoi.BusinessRelation.Detail,
                    };
                    listEbekCoi.Add(coiBusinessRelation);

                    TBL_T_EBEK_COI coiInvestationNonPublic = new TBL_T_EBEK_COI()
                    {
                        EBEK_ID = newEbekData.ID,
                        CATEGORY = CoiConstant.InvestationNonPublic,
                        COMPANY = ebekRequestsVM.HadCoi.InvestationNonPublic.MemberCompany,
                        POSITION = ebekRequestsVM.HadCoi.InvestationNonPublic.MemberPosition,
                        INFORMATION = ebekRequestsVM.HadCoi.InvestationNonPublic.Detail,
                    };
                    listEbekCoi.Add(coiInvestationNonPublic);

                    TBL_T_EBEK_COI coiInvestationSupplier = new TBL_T_EBEK_COI()
                    {
                        EBEK_ID = newEbekData.ID,
                        CATEGORY = CoiConstant.InvestationSupplier,
                        COMPANY = ebekRequestsVM.HadCoi.InvestationSupplier.MemberCompany,
                        POSITION = ebekRequestsVM.HadCoi.InvestationSupplier.MemberPosition,
                        INFORMATION = ebekRequestsVM.HadCoi.InvestationSupplier.Detail,
                    };
                    listEbekCoi.Add(coiInvestationSupplier);

                    TBL_T_EBEK_COI coiAnotherBusiness = new TBL_T_EBEK_COI()
                    {
                        EBEK_ID = newEbekData.ID,
                        CATEGORY = CoiConstant.AnotherBusiness,
                        COMPANY = ebekRequestsVM.HadCoi.AnotherBusiness.MemberCompany,
                        POSITION = ebekRequestsVM.HadCoi.AnotherBusiness.MemberPosition,
                        INFORMATION = ebekRequestsVM.HadCoi.AnotherBusiness.Detail,
                    };
                    listEbekCoi.Add(coiAnotherBusiness);

                    TBL_T_EBEK_COI coiSideJob = new TBL_T_EBEK_COI()
                    {
                        EBEK_ID = newEbekData.ID,
                        CATEGORY = CoiConstant.SideJob,
                        COMPANY = ebekRequestsVM.HadCoi.SideJob.MemberCompany,
                        POSITION = ebekRequestsVM.HadCoi.SideJob.MemberPosition,
                        INFORMATION = ebekRequestsVM.HadCoi.SideJob.Detail,
                    };
                    listEbekCoi.Add(coiSideJob);

                    db.TBL_T_EBEK_COIs.InsertAllOnSubmit(listEbekCoi);
                    db.SubmitChanges();
                }

                response.Message = "Data EBEK berhasil tersubmit";
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Terjadi kesalahan: {ex.Message}";
                return response;
            }
        }

        public List<VW_EBEK_REPORT> GetDataTableEbek()
        {
            var listDataEbek = db.VW_EBEK_REPORTs.ToList();
            return listDataEbek;
        }

        public List<VW_EBEK_REPORT> GetFilteredDataTableEbek(EbekFilterRequestsVM requestsVM)
        {
            var listDataEbek = db.VW_EBEK_REPORTs.AsQueryable();
            var result = new List<VW_EBEK_REPORT>();
            if (!requestsVM.Nama.IsNullOrWhiteSpace())
            {
                listDataEbek = listDataEbek.Where(x => x.NAME.Contains(requestsVM.Nama));
            }

            if (!requestsVM.District.IsNullOrWhiteSpace())
            {
                listDataEbek = listDataEbek.Where(x => x.SITE == requestsVM.District);
            }

            if (!requestsVM.Dept.IsNullOrWhiteSpace())
            {
                listDataEbek = listDataEbek.Where(x => x.DEPT == requestsVM.Dept);
            }

            return listDataEbek.ToList();
        }

        public ChartResponseVM GetChartEbek(ChartRequestVM requests)
        {
            var dataInit = ChartHelper.InitChartDataSet();
            ChartResponseVM dataResponse = new ChartResponseVM()
            {
                labels = new List<string>(),
                datasets = new List<ChartDataSet>()
            };

            var baseData = db.VW_ALL_USERs.Where(x => x.DSTRCT_CODE != null).ToList();

            if (!requests.Dept.IsNullOrWhiteSpace() && !requests.District.IsNullOrWhiteSpace())
            {
                baseData = baseData.Where(x => x.DEPT_DESC == requests.Dept && x.DSTRCT_CODE == requests.District).ToList();
                var customLabel = ($"{requests.District} - {requests.Dept}");
                dataResponse.labels.Add(customLabel);

                foreach (var dataSet in dataInit)
                {
                    var countData = baseData.Where(x => x.EBEK_SUBMIT == dataSet.isSubmit).Count();
                    var newData = new ChartDataSet
                    {
                        label = dataSet.label,
                        backgroundColor = dataSet.backgroundColor,
                        data = new List<int>()
                    };
                    int totalData = 0;
                    if (dataSet.isSubmit)
                    {
                        totalData = baseData.Where(x => x.EBEK_SUBMIT == dataSet.isSubmit).Count();
                    }
                    else
                    {
                        totalData = baseData.Where(x => x.EBEK_SUBMIT == dataSet.isSubmit || x.EBEK_SUBMIT == null).Count();
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
                        data = new List<int>()
                    };

                    foreach (var district in listDistrict)
                    {
                        int totalData = 0;
                        if (dataSet.isSubmit)
                        {
                            totalData = baseData.Where(x => x.DSTRCT_CODE == district && x.EBEK_SUBMIT == dataSet.isSubmit).Count();
                        }
                        else
                        {
                            totalData = baseData.Where(x => x.DSTRCT_CODE == district && (x.EBEK_SUBMIT == dataSet.isSubmit || x.EBEK_SUBMIT == null)).Count();
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
                        data = new List<int>()
                    };

                    foreach (var dept in listDept)
                    {
                        int totalData = 0;
                        if (dataSet.isSubmit)
                        {
                            totalData = baseData.Where(x => x.DEPT_DESC == dept && x.EBEK_SUBMIT == dataSet.isSubmit).Count();
                        }
                        else
                        {
                            totalData = baseData.Where(x => x.DEPT_DESC == dept && (x.EBEK_SUBMIT == dataSet.isSubmit || x.EBEK_SUBMIT == null)).Count();
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
                        data = new List<int>()
                    };

                    foreach (var district in listDistrict)
                    {
                        int totalData = 0;
                        if (dataSet.isSubmit)
                        {
                            totalData = baseData.Where(x => x.DSTRCT_CODE == district && x.EBEK_SUBMIT == dataSet.isSubmit).Count();
                        }
                        else
                        {
                            totalData = baseData.Where(x => x.DSTRCT_CODE == district && (x.EBEK_SUBMIT == dataSet.isSubmit || x.EBEK_SUBMIT == null)).Count();
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