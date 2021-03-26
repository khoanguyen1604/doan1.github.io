using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using QuanLyNhanSu.Reports;
using System.IO;
using QuanLyNhanSu.DAL;

namespace QuanLyNhanSu.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        QLNHANSUContext db = new QLNHANSUContext();
        DataSet3 ds = new DataSet3();
        public ActionResult ReportBangLuong(string thang)
        {
            if (thang == null || thang == "")
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;
                reportViewer.Width = 1500;
                reportViewer.Height = 1500;

                var connectionString = ConfigurationManager.ConnectionStrings["quanlynhansu1"].ConnectionString;


                SqlConnection conx = new SqlConnection(connectionString);
                SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM BangLuongs", conx);

                adp.Fill(ds, ds.BangLuongs.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Report8.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));


                ViewBag.ReportViewer = reportViewer;
            }
            else
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;
                reportViewer.Width = 1500;
                reportViewer.Height = 1500;
                DateTime Thang = DateTime.Parse(thang.ToString());
                var connectionString = ConfigurationManager.ConnectionStrings["DBQuanLyNhanSu4ConnectionString"].ConnectionString;
                string sql = "Select * from BangLuongs Where Thang ='" + Thang.ToString("yyyy-MM-dd") + "'";

                SqlConnection conx = new SqlConnection(connectionString);
                SqlDataAdapter adp = new SqlDataAdapter(sql, conx);

                adp.Fill(ds, ds.BangLuongs.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Report8.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                ViewBag.ReportViewer = reportViewer;
            }
            return View();
        }
        public ActionResult Report(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/Report8.rdlc");
            ReportDataSource reportdatasource = new ReportDataSource();
            reportdatasource.Name = "DataSet1";
            reportdatasource.Value = db.BangLuongs.ToList();
            localreport.DataSources.Add(reportdatasource);

            string reporttype = ReportType;
            string mineType;
            string encoding;
            string FileNameExtension;
            
            if (ReportType == "Excel")
            {
                FileNameExtension = "xlsx";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localreport.Render(reporttype, "", out mineType, out encoding, out FileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename = bangluong-report." + FileNameExtension);
            return File(renderedByte, FileNameExtension);
        }
    }
}