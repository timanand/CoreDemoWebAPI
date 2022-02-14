using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Authorization;


namespace CoreDemoWebAPI.IntegrationTests

{

    public class TestClientProvider : IDisposable
    {

        private TestServer server;

        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = server.CreateClient();
        }


        public void Dispose()
        {
            // if server is not null then dispose
            server?.Dispose();

            // if client is not null then dispose
            Client?.Dispose();

        }
    }

}
