﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database_reader_V2.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace Database_reader_V2.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index(string searchString,string search_radio)
        {
            string sqlquery;
            // ReportDBHandle dbhandle = new ReportDBHandle();
            string mainconn = ConfigurationManager.ConnectionStrings["ReportConn"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            if (searchString == null || search_radio == "all")
            {
                 sqlquery = "select * from [dbo].[RPT_Lots_Report]";
            }
            else if(searchString!=null && search_radio==null)
            {
                sqlquery = "select * from [dbo].[RPT_Lots_Report] JOIN  [dbo].[RPT_Lots_Info] ON ([dbo].[RPT_Lots_Info].[Lot_Id] = [dbo].[RPT_Lots_Report].[Rep_Lot_Id]) where [dbo].[RPT_Lots_Info].[Lot_Code] like '" + searchString + "'";
               
            }
            else if(search_radio == "last_week")
            {
                //sqlquery = "select [dbo].[RPT_Lots_Report].[Rep_Lot_Id],[dbo].[RPT_Lots_Report].[Rep_Id],[dbo].[RPT_Lots_Report].[Rep_PDFToCreate],[dbo].[RPT_Lots_Report].[Rep_PDFCreated],[dbo].[RPT_Lots_Report].[Rep_Status],[dbo].[RPT_Lots_Report].[Rep_Type],[dbo].[RPT_Lots_Info].[Lot_Stop] as Stop_date" +
                //    " from [dbo].[RPT_Lots_Report],[dbo].[RPT_Lots_Info]" +

                //    "where [dbo].[RPT_Lots_Info].[Lot_Stop] >= DATEADD(day,-7, GETDATE())";
                sqlquery = "select * from [dbo].[RPT_Lots_Report] JOIN[dbo].[RPT_Lots_Info] ON([dbo].[RPT_Lots_Info].[Lot_Id] = [dbo].[RPT_Lots_Report].[Rep_Lot_Id]) where [dbo].[RPT_Lots_Info].[Lot_Stop]>= DATEADD(day, -7, GETDATE()) ";
            }
            else if(search_radio == "date_selection" && searchString!=null)
            {
                sqlquery = "select [dbo].[RPT_Lots_Report].[Rep_Lot_Id],[dbo].[RPT_Lots_Report].[Rep_Id],[dbo].[RPT_Lots_Report].[Rep_PDFToCreate],[dbo].[RPT_Lots_Report].[Rep_PDFCreated],[dbo].[RPT_Lots_Report].[Rep_Status],[dbo].[RPT_Lots_Report].[Rep_Type]" +
                    " from [dbo].[RPT_Lots_Report],[dbo].[RPT_Lots_Info]" +

                    " where [dbo].[RPT_Lots_Info].[Lot_Stop] ='" + searchString + "'";
            }
            else
            {
                sqlquery = "select * from [dbo].[RPT_Lots_Report]"; //modificare
            }
            //sqlquery = "select * from [dbo].[RPT_Lots_Report]";

            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sqlconn.Close();
            //ModelState.Clear();
            List<Report> ec = new List<Report>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                ec.Add(new Report
                {
                    Rep_Id = Convert.ToString(dr["Rep_Id"]),
                    Rep_Lot_Id = Convert.ToString(dr["Rep_Lot_Id"]),
                    Rep_PDFStatus = Convert.ToInt32(dr["Rep_PDFStatus"]),
                    Rep_Status = Convert.ToInt32(dr["Rep_Status"]),
                    Rep_Type = Convert.ToInt32(dr["Rep_Type"]),
                }
                    );
            }
            sqlconn.Close();
            ModelState.Clear();
            return View(ec);
            // return View();
        }

        // GET: Report/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(int id)
        {
            ReportDBHandle dbhandle = new ReportDBHandle();
            ModelState.Clear();
            return View(dbhandle.ViewLotId());
        }
    }
}
