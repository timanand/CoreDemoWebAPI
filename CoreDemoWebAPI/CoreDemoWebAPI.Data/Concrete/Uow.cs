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


        // 09/02/2022 - BEGIN

        // Constructor
        //public Uow(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        // public IStaffRepository StaffRepository => new StaffRepository(_configuration);


        private readonly string _connectionString;

        public Uow(IConfiguration configuration, IProvider provider)
        {
            _configuration = configuration;
            _connectionString = provider.ConnData;
        }

        public IStaffRepository StaffRepository => new StaffRepository(_connectionString);

        // 09/02/2022 - END



    }

}
