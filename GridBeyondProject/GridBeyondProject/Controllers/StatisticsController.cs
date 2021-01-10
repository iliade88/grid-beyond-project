using GridBeyondProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridBeyondProject.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult Index()
        {
            MarketPriceDBContext db = new MarketPriceDBContext();
            Statistics statisticsData = new Statistics(db.MarketPrices.ToList());

            return View(statisticsData);
        }
    }
}