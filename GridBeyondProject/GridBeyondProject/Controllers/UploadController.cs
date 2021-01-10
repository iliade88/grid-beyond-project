using GridBeyondProject.Models;
using GridBeyondProject.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridBeyondProject.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = TempData["SaveFileMessage"];
            return View();
        }

        public ActionResult DeleteAll()
        {
            MarketPriceDBContext db = new MarketPriceDBContext();
            db.MarketPrices.RemoveRange(db.MarketPrices.ToList());
            db.SaveChanges();

            TempData["SaveFileMessage"] = "Everything deleted!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    UploadService.ProcessFile(path);

                    TempData["SaveFileMessage"] = "File uploaded successfully";
                    return RedirectToAction("Index", "Statistics");
                }
                catch (Exception ex)
                {
                    TempData["SaveFileMessage"] = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                TempData["SaveFileMessage"] = "You have not specified a file.";
            }

            return RedirectToAction("Index");
        }
    }
}