using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Database_reader_V2.Models
{
    public class ReportDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["reportconn"].ToString();
            con = new SqlConnection(constring);
        }

        // **view report**
        public List<Report> GetReport()
        {
            connection();
            List<Report> reportlist = new List<Report>();

            SqlCommand cmd = new SqlCommand("GetReportDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                reportlist.Add(
                    new Report
                    {
                        Rep_Id = Convert.ToString(dr["Rep_Id"]),
                        Rep_Lot_Id = Convert.ToString(dr["Rep_Lot_Id"]),
                        Rep_PDFToCreate = Convert.ToBoolean(dr["Rep_PDFToCreate"]),
                        Rep_PDFCreated = Convert.ToBoolean(dr["Rep_PDFCreated"]),
                        Rep_Status = Convert.ToInt32(dr["Rep_Status"]),
                        Rep_Type = Convert.ToString(dr["Rep_Type"])

                    });
            }
            return reportlist;
        }

        // **search by id report**
        public List<Report> ViewLotId()
        {
            connection();
            List<Report> reportlist = new List<Report>();

            SqlCommand cmd = new SqlCommand("Search_by_lot_Id", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                reportlist.Add(
                    new Report
                    {
                        Rep_Id = Convert.ToString(dr["Rep_Id"]),
                        Rep_Lot_Id = Convert.ToString(dr["Rep_Lot_Id"]),
                        Rep_PDFToCreate = Convert.ToBoolean(dr["Rep_PDFToCreate"]),
                        Rep_PDFCreated = Convert.ToBoolean(dr["Rep_PDFCreated"]),
                        Rep_Status = Convert.ToInt32(dr["Rep_Status"]),
                        Rep_Type = Convert.ToString(dr["Rep_Type"])

                    });
            }
            return reportlist;
        }

    }
}