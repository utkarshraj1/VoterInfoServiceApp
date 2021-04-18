using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace VISApp.Controllers
{
    public class HomeController : Controller
    {
        VoterRepository voterRepository = new VoterRepository();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection loginForm)
        {
            string emailid = loginForm["txtEmailId"];
            string password = loginForm["txtPassword"];

            AdminUser adminUser = voterRepository.ValidateAdmin(emailid, password);

            if(adminUser == null)
            {
                ViewBag.ErrorMsg = "Invalid credentials, please try again";
                return View("Index");
            }

            else
            {
                Session["EmailId"] = emailid;
                Session["AdminId"] = adminUser.ID;

                return RedirectToAction("Index", "Voter");
            }
        }
    }
}