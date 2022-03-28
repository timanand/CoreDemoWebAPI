using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreDemoWebAPI.Domain;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace CoreDemoWebAPI.Data
{

    public class StaffRepository : IStaffRepository
    {

        private readonly DbHelper _DbHelper;


        // 09/02/2022 - BEGIN

        // Constructor
        //public StaffRepository(IConfiguration configuration)
        //{
        //    string connectionstring = configuration.GetConnectionString("StaffConnex");
        //    _DbHelper = new DbHelper(connectionstring);
        //}


        //Constructor
        public StaffRepository(string connectionstring)
        {
            //string connectionstring = configuration.GetConnectionString("StaffConnex");
            //connectionstring = "Data Source=FYD8ZD3\\SQLEXPRESS; Initial Catalog=CoreDemoADONET; integrated security=false;user id=arvinder;password=TeachApple6732$#;";
            _DbHelper = new DbHelper(connectionstring);
        }

        // 09/02/2022 - END




        public bool Add(StaffMember staffMember)
        {
            bool err = false;
            IDbConnection con = null;

            try
            {
                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("INSERT INTO StaffMembers(FirstName, LastName, Title) VALUES(@FirstName, @LastName, @Title)", con);
                DbHelper.AddParameter(cmd, "@FirstName", staffMember.FirstName);
                DbHelper.AddParameter(cmd, "@LastName", staffMember.LastName);
                DbHelper.AddParameter(cmd, "@Title", staffMember.Title);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
            }

            if (err) return false;
            else return true;
        }

        public bool Delete(int? id)
        {
            bool err = false;
            IDbConnection con = null;

            try
            {
                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("DELETE FROM StaffMembers WHERE Id=" + id.ToString(), con);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
            }

            if (err) return false;
            else return true;

        }

        public List<StaffMember> GetAll(string sortBy = "Id")
        {

            IDbConnection con = null;
            List<StaffMember> listStaff = new List<StaffMember>();

            try
            {
                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("SELECT * FROM StaffMembers ORDER BY " + sortBy, con);
                IDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    StaffMember staffMember = new StaffMember();
                    staffMember.Id = (int)reader.GetValue(reader.GetOrdinal("Id"));
                    staffMember.FirstName = (String)reader.GetValue(reader.GetOrdinal("FirstName"));
                    staffMember.LastName = (String)reader.GetValue(reader.GetOrdinal("LastName"));
                    staffMember.Title = (String)reader.GetValue(reader.GetOrdinal("Title"));


                    listStaff.Add(staffMember);
                }

            }

            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
            }

            return listStaff;


        }

        public StaffMember GetById(int id)
        {
            IDbConnection con = null;
            StaffMember staffMember = new StaffMember();

            try
            {
                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("SELECT * FROM StaffMembers WHERE Id=" + id.ToString(), con);
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    staffMember.Id = (int)reader.GetValue(reader.GetOrdinal("Id"));
                    staffMember.FirstName = (String)reader.GetValue(reader.GetOrdinal("FirstName"));
                    staffMember.LastName = (String)reader.GetValue(reader.GetOrdinal("LastName"));
                    staffMember.Title = (String)reader.GetValue(reader.GetOrdinal("Title"));
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
            }

            return staffMember;

        }

        public bool Edit(StaffMember staffMember)
        {
            bool err = false;

            IDbConnection con = null;

            try
            {
                con = DbHelper.CreateConnection();
                con.Open();

                string sql = "UPDATE StaffMembers SET " +
                             "FirstName= '" + staffMember.FirstName + "'," +
                             "LastName= '" + staffMember.LastName + "'," +
                             "Title= '" + staffMember.Title + "' " +
                             "WHERE Id=" + staffMember.Id.ToString();

                IDbCommand cmd = DbHelper.CreateCommand(sql, con);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
            }

            if (err) return false;
            else return true;
        }


        public List<StaffMember> SearchEmployees(string search)
        {
            IDbConnection con = null;
            List<StaffMember> listStaff = new List<StaffMember>();

            try
            {

                con = DbHelper.CreateConnection();
                con.Open();


                string sql = "SELECT * FROM StaffMembers WHERE FirstName LIKE '%" + search + "%' OR " +
                             "LastName LIKE '%" + search + "%' OR " +
                             "Id LIKE '%" + search + "%'";

                IDbCommand cmd = DbHelper.CreateCommand(sql, con);
                IDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    StaffMember staffMember = new StaffMember();
                    staffMember.Id = (int)reader.GetValue(reader.GetOrdinal("Id"));
                    staffMember.FirstName = (String)reader.GetValue(reader.GetOrdinal("FirstName"));
                    staffMember.LastName = (String)reader.GetValue(reader.GetOrdinal("LastName"));
                    staffMember.Title = (String)reader.GetValue(reader.GetOrdinal("Title"));

                    listStaff.Add(staffMember);
                    }

            }

            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
            }

            return listStaff;

        }



        public bool LocateUserSecurity(string userId, string password)
        {
            int noRecs = 0;

            IDbConnection con = null;
            StaffMember staffMember = new StaffMember();

            try
            {
                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("SELECT COUNT(*) AS c FROM UserSecurity WHERE UserId='" + userId + "' AND Password='" + password + "'", con);
                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    noRecs = (int)reader.GetValue(reader.GetOrdinal("c"));
                }

            }

            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
            }

            if (noRecs == 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }



    }
}
