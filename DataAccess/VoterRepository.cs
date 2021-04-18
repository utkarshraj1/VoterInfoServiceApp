using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess
{
    public class VoterRepository
    {
        private VISDBContext db;

        public VoterRepository()
        {
            db = new VISDBContext();
        }

        //To check the validity of the admin user
        public AdminUser ValidateAdmin(string email, string pwd)
        {
            AdminUser adminUser = null;

            try
            {
                adminUser = db.AdminUsers.Where(v => v.EmailId == email
                                                && v.Password == pwd).FirstOrDefault();
            }

            catch
            {
                adminUser = null;
            }

            return adminUser;
        }

        #region CRUD Methods for Voters

        //To add a voter (POST) (Create)
        public bool AddVoter(Voter vInfo)
        {
            if(vInfo == null)
            {
                return false;
            }

            try
            {
                db.Voters.Add(vInfo);
                db.SaveChanges();
            }

            catch
            {
                return false;
            }

            return true;
        }

        //Find the record of the voter using PK (GET) (Read)
        public Voter FindVoter(int vId)
        {
            Voter voter = null;

            try
            {
                voter = db.Voters.Find(vId);
            }

            catch
            {
                voter = null;
            }

            return voter;
        }

        //To update a Voter's details (POST) (Update)
        public bool UpdateVoterDetails(Voter vInfo)
        {
            if(vInfo == null)
            {
                return false;
            }

            try
            {
                db.Entry(vInfo).State = EntityState.Modified;
                db.SaveChanges();
            }

            catch
            {
                return false;
            }

            return true;
        }

        //To delete a voter from the list (POST) (Delete)
        public bool DeleteVoterDetails(int vId)
        {
            try
            {
                Voter voter = FindVoter(vId);
                db.Voters.Remove(voter);
                db.SaveChanges();
            }

            catch
            {
                return false;
            }

            return true;
        }

        //To get the voter list (GET)
        public List<Voter> GetVotersList()
        {
            try
            {
                return db.Voters.ToList();
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
    }
}
