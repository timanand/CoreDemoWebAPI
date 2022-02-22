using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreDemoWebAPI.Domain;


namespace CoreDemoWebAPI.Data
{
    public interface IStaffRepository
    {
        List<StaffMember> GetAll();


        bool Add(StaffMember staffMember);

        bool Edit(StaffMember staffMember);

        bool Delete(int id);

        bool LocateUserSecurity(string userId, string password);

        StaffMember GetById(int id);

        List<StaffMember> SearchEmployees(string search);

    }

}
