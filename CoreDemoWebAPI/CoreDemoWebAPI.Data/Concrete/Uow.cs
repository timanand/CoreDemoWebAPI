using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace CoreDemoWebAPI.Data
{

    public class Uow : IUow
    {
        private readonly IConfiguration _configuration;


        // Constructor
        public Uow(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IStaffRepository StaffRepository => new StaffRepository(_configuration);

    }

}
