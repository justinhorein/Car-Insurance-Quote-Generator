using CarInsuranceMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (Car_InsuranceEntities db = new Car_InsuranceEntities())
            {
                var quotes = db.Quotes;
                var quoteList = new List<Quote>();
                foreach (var quote in quotes)
                {
                    var entry = new Quote();
                    entry.FirstName = quote.FirstName;
                    entry.LastName = quote.LastName;
                    entry.EmailAddress = quote.EmailAddress;
                    entry.Quote1 = quote.Quote1;
                    quoteList.Add(entry);
                }

                return View(quoteList);
            }
        }
    }
}