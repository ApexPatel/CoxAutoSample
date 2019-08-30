using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

namespace CoxAutoSample.BLL
{
    public class Utility
    {
        public static DataTable ImportCSV(string fileName)
        {   
            string line = string.Empty;
            string[] fileColumns;
            DataTable dt = new DataTable();
            DataRow row;

            //Set the File Name
            StreamReader sr = new StreamReader(fileName);

            try
            {
                //Split on comma
                Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");               

                //Read the header to capture column names
                line = sr.ReadLine();
                fileColumns = r.Split(line);

                Array.ForEach(fileColumns, s => dt.Columns.Add(new DataColumn(s)));


                //Read CVS file until it's empty
                while ((line = sr.ReadLine()) != null)
                {
                    row = dt.NewRow();

                    //Add current value to DataRow
                    row.ItemArray = r.Split(line);
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                //Errors  
            }
            finally 
            {
                sr.Dispose();
            }
            
            return dt;
        }
    }
}