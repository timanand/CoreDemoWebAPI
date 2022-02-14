using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDemoWebAPI.Data
{


    public class Provider : IProvider
    {

        string _provider = "";

        public Provider(string provider)
        {
            _provider = provider;
        }

        public string ConnData
        {

            get
            {
                return _provider;
            }

        }
    }

}
