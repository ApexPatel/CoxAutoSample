using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CoxAutoSample.BLL;
using System.Data;
using System.IO;

namespace CoxAutoSample.Models
{
    public class CarsModel
    {
        public int DealNumber { get; set; }
        public string CustomerName { get; set; }
        public string DealershipName { get; set; }
        public string Vehicle { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public List<CarsModel> carLst { get; set; }

        public List<CarsModel> UplaodCarDetails(HttpPostedFileBase FileUpload)
        {
            DataTable dt = new DataTable();

            carLst = new List<CarsModel>();

            //Check whether we have a file 
            if (FileUpload != null && FileUpload.ContentLength > 0)
            {
                //Storing file at ~/App_Data/Uploads
                string fileName = Path.GetFileName(FileUpload.FileName);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/Uploads"), fileName);
                        
                try
                {
                    FileUpload.SaveAs(path);

                    //Decoding CSV file and storing it into DataTable
                    dt = Utility.ImportCSV(path);
                                        
                    //Storing records from DataTable to Car List
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CarsModel cls = new CarsModel();
                        cls.DealNumber = dt.Rows[i]["DealNumber"] != null ? Convert.ToInt32(dt.Rows[i]["DealNumber"].ToString().Trim()) : 0;
                        cls.CustomerName = dt.Rows[i]["CustomerName"].ToString().Trim();
                        cls.DealershipName = dt.Rows[i]["DealershipName"].ToString().Trim();
                        cls.Vehicle = dt.Rows[i]["Vehicle"].ToString().Trim();
                        cls.Price = dt.Rows[i]["Price"] != null ? string.Format("CAD${0:#.00}", Convert.ToDouble(dt.Rows[i]["Price"].ToString().Replace("\"", ""))) : "CAD$0";
                        cls.Date = dt.Rows[i]["Date"] != null ? Convert.ToDateTime(dt.Rows[i]["Date"]).ToShortDateString() : DateTime.Now.ToShortDateString();

                        this.carLst.Add(cls);                        
                    }
                }
                catch (Exception ex)
                {
                    //Errors                    
                }
            }
            else
            {
                //Errors              
            }
            return carLst;
        }
    }
}