using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using CoxAutoSample.Models;

namespace CoxAutoSample.Controllers
{
    public class CarsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase FileUpload)
        {
            CarsModel objCarsModel = new CarsModel();
            return View(objCarsModel.UplaodCarDetails(FileUpload));
        }

        

    }
}
