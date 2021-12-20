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

        //StaffMember GetById(int id);

        void Add(StaffMember staffMember);

        void Edit(StaffMember staffMember);

        void Delete(int id);

        //List<StaffMember> SearchEmployees(string search);

    }

}
