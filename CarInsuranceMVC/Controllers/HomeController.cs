using CarInsuranceMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Submit(string FirstName, string LastName, string EmailAddress, DateTime DOB, int? CarYear, 
            string CarMake, string CarModel, bool DUI, int? SpeedTickets, bool Coverage)
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(EmailAddress) 
                || string.IsNullOrEmpty(DOB.ToString()) || string.IsNullOrEmpty(CarYear.ToString()) || string.IsNullOrEmpty(CarMake)
                || string.IsNullOrEmpty(CarModel) || string.IsNullOrEmpty(SpeedTickets.ToString()))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                double rate = 50;
                int age = DateTime.Now.Year - DOB.Year;

                if (age < 25)
                {
                    rate += 25;
                }
                if (age < 18)
                {
                    rate += 100;
                }
                if (age >= 100)
                {
                    rate += 25;
                }
                if (CarYear < 2000)
                {
                    rate += 25;
                }
                if (CarYear >= 2015)
                {
                    rate += 25;
                }
                if (CarMake == "Porsche" || CarMake == "porsche")
                {
                    rate += 25;
                }
                if ((CarMake == "Porsche" || CarMake == "porsche") && (CarModel == "911 Carrera" || CarModel == "911 carrera"))
                {
                    rate += 25;
                }
                rate += Convert.ToInt32(SpeedTickets) * 10;
                if (DUI)
                {
                    rate += rate * .25;
                }
                if (Coverage)
                {
                    rate += rate * .5;
                }
                rate = Math.Round(rate, 2);

                using (Car_InsuranceEntities db = new Car_InsuranceEntities())
                {
                    var quotes = db.Quotes;
                    var quote = new Quote();

                    quote.FirstName = FirstName;
                    quote.LastName = LastName;
                    quote.EmailAddress = EmailAddress;
                    decimal? rateDec = Convert.ToDecimal(rate);
                    quote.Quote1 = rateDec;
                    quotes.Add(quote);
                    db.SaveChanges();
                    return View("Quote", quote);
                }      
            }
        }
    }
}