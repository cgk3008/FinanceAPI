using HouseFinanceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseFinanceAPI.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }





        //public JsonResult GetChartDataAjax(string type)
        //{
        //    if (type == "Current")
        //    {
        //        var trx = db.Households.Find(User.Identity.GetHouseholdId())
        //        .Accounts.SelectMany(t => t.Transactions)
        //        .Where(t => t.Date.Month == DateTime.Now.Month);
        //        List<ChartData> data = new List<ChartData>();
        //        foreach (Transaction t in trx)
        //        {
        //            data.Add(new ChartData() { X = t.Category.Name, Y = t.Amount.ToString() });
        //        }
        //        return Json(data);
        //    }
        //    else if (type == "Last")
        //    {
        //        var trx = db.Households.Find(User.Identity.GetHouseholdId())
        //        .Accounts.SelectMany(t => t.Transactions)
        //        .Where(t => t.Date.Month == DateTime.Now.Month - 1);
        //        List<ChartData> data = new List<ChartData>();
        //        foreach (Transaction t in trx)
        //        {
        //            data.Add(new ChartData() { X = t.Category.Name, Y = t.Amount.ToString() });
        //        }
        //        return Json(data);
        //    }
        //    return null;
        //}


        //public JsonResult FusionDataAjax()
        //{
        //    var trx = db.Households.Find(User.Identity.GetHouseholdId())
        //    .Accounts.SelectMany(t => t.Transactions);
        //    //.Where(t => t.Date.Month == DateTime.Now.Month);
        //    List<FusionData> data = new List<FusionData>();
        //    foreach (Transaction t in trx)
        //    {
        //        data.Add(new FusionData() { Label = t.Category.Name, Value = t.Amount.ToString() });
        //    }
        //    return Json(data);
        //}
        //public JsonResult AccountDataAjax()
        //{
        //    var trx = db.Households.Find(User.Identity.GetHouseholdId())
        //    .Accounts;
        //    List<FusionData> data = new List<FusionData>();
        //    foreach (PersonalAccount t in trx)
        //    {
        //        data.Add(new FusionData() { Label = t.Name, Value = t.Balance.ToString() });
        //    }
        //    return Json(data);
        //}


    }
}
