// TestClass - Dependency Injection - BEGIN

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreDemoWebAPI.Data.Interfaces;
using Microsoft.Extensions.Configuration;


namespace CoreDemoWebAPI.Data.Concrete
{
    public class TestClass : ITestClass
    {
        private readonly IConfiguration _config;

        public TestClass(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString()
        {
            return _config.GetConnectionString("StaffConnex");
        }

    }
}

// TestClass - Dependency Injection - END