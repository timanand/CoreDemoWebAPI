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


        // Constructor
        public StaffRepository(IConfiguration configuration)
        {
            string connectionstring = configuration.GetConnectionString("StaffConnex");

            _DbHelper = new DbHelper(connectionstring);
        }


        public void Add(StaffMember staffMember)
        {

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
        }

        public void Delete(int id)
        {

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

        }

        public List<StaffMember> GetAll()
        {

            IDbConnection con = null;
            List<StaffMember> listStaff = new List<StaffMember>();

            try
            {
                con = DbHelper.CreateConnection();
                con.Open();

                IDbCommand cmd = DbHelper.CreateCommand("SELECT * FROM StaffMembers", con);
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

        public void Edit(StaffMember staffMember)
        {

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




    }
}
