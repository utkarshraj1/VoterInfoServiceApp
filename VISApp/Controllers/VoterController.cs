using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace VISApp.Controllers
{
    public class VoterController : Controller
    {
        VoterRepository voterRepository = new VoterRepository();

        [HttpGet]
        public ActionResult Index()
        {
            if(Session["Emailid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<DataAccess.Voter> entityVoter = voterRepository.GetVotersList();
            List<Models.Voter> mvcVoter = new List<Models.Voter>();

            foreach(var eVoter in entityVoter)
            {
                Models.Voter temp = new Models.Voter();

                temp.VoterId = eVoter.VoterId;
                temp.VoterName = eVoter.VoterName;
                temp.Age = eVoter.Age;
                temp.DOB = eVoter.DOB;
                temp.Gender = eVoter.Gender;
                temp.City = eVoter.City;
                temp.State = eVoter.State;
                temp.EmailId = eVoter.EmailId;
                temp.MobileNumber = eVoter.MobileNumber;

                mvcVoter.Add(temp);
            }
            return View(mvcVoter);
        }

        //Create Controller
        [HttpGet]
        public ActionResult Create()
        {
            if(Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Voter voter)
        {
            if(Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                DataAccess.Voter vInfo = new DataAccess.Voter()
                {
                    VoterName = voter.VoterName,
                    Age = voter.Age,
                    DOB = voter.DOB,
                    Gender = voter.Gender,
                    State = voter.State,
                    City = voter.City,
                    EmailId = voter.EmailId,
                    MobileNumber = voter.MobileNumber
                };

                VoterRepository repo = new VoterRepository();
                bool result = repo.AddVoter(vInfo);

                if (!result)
                {
                    return View("Error");
                }
            }

            return RedirectToAction("Index", "Voter");
        }

        //Update Controller
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if(Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            VoterRepository repo = new VoterRepository();
            var busEntity = voterRepository.FindVoter(id);

            Models.Voter voter = new Models.Voter()
            {
                VoterId = busEntity.VoterId,
                VoterName = busEntity.VoterName,
                Age = busEntity.Age,
                DOB = busEntity.DOB,
                Gender = busEntity.Gender,
                City = busEntity.City,
                State = busEntity.State,
                EmailId = busEntity.EmailId,
                MobileNumber = busEntity.MobileNumber
            };

            return View(voter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Voter voter)
        {
            if(Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DataAccess.Voter eVoter = new DataAccess.Voter()
            {
                VoterId = voter.VoterId,
                VoterName = voter.VoterName,
                Age = voter.Age,
                DOB = voter.DOB,
                Gender = voter.Gender,
                City = voter.City,
                State = voter.State,
                EmailId = voter.EmailId,
                MobileNumber = voter.MobileNumber
            };

            VoterRepository repo = new VoterRepository();
            bool result = repo.UpdateVoterDetails(eVoter);

            if (!result)
            {
                return View("Error");
            }

            return RedirectToAction("Index", "Voter");
        }

        //Delete Controller
        [HttpGet]
        public ActionResult Delete(int id)
        {
            VoterRepository repo = new VoterRepository();
            var bEntity = repo.FindVoter(id);

            Models.Voter voter = new Models.Voter()
            {
                VoterId = bEntity.VoterId,
                VoterName = bEntity.VoterName,
                Age = bEntity.Age,
                DOB = bEntity.DOB,
                Gender = bEntity.Gender,
                City = bEntity.City,
                State = bEntity.State,
                EmailId = bEntity.EmailId,
                MobileNumber = bEntity.MobileNumber
            };

            return View(voter);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            if(Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            VoterRepository repo = new VoterRepository();
            bool result = repo.DeleteVoterDetails(id);

            if (!result)
            {
                return View("Error");
            }

            return RedirectToAction("Index", "Voter");
        }

        //Individual details controller
        [HttpGet]
        public ActionResult Details(int id)
        {
            if(Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            VoterRepository repo = new VoterRepository();
            var bEntity = repo.FindVoter(id);

            Models.Voter voter = new Models.Voter()
            {
                VoterId = bEntity.VoterId,
                VoterName = bEntity.VoterName,
                Age = bEntity.Age,
                DOB = bEntity.DOB,
                Gender = bEntity.Gender,
                City = bEntity.City,
                State = bEntity.State,
                EmailId = bEntity.EmailId,
                MobileNumber = bEntity.MobileNumber
            };

            return View(voter);
        }

        //Log out controller
        [HttpGet]
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Index","Home");
        }
    }
}