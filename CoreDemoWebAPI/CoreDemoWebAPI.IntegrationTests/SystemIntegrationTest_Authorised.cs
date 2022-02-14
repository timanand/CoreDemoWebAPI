using CoreDemoWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

// IntegrationTests3.cs, CustomWebApplicationFactory.cs and FakeJwtManager.cs all work to

namespace CoreDemoWebAPI.IntegrationTests
{

    public class SystemIntegrationTest_Authorised : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        public SystemIntegrationTest_Authorised(CustomWebApplicationFactory<Startup> factory)
        {
            httpClient = factory.CreateClient();
        }

        // ...code...


        [Fact]
        public async Task GetWithAuthorization_Successful()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/staffmembers/read");

            request.Content = new StringContent(JsonSerializer.Serialize(new
            {
                term = "MFA",
                definition = "An authentication process that considers multiple factors."
            }), Encoding.UTF8, "application/json");

            var accessToken = FakeJwtManager.GenerateJwtToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Act
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var staffMembers = JsonSerializer.Deserialize<List<StaffMember>>(content);


        }



        [Fact]
        public async Task AddStaffMemberWithAuthorization_Successful()
        {

            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/staffmembers/create");

            request.Content = new StringContent(JsonSerializer.Serialize(new
            {
                FirstName = "ADDDDD",
                LastName = "ADDDDDD",
                Title = "Mr"
            }), Encoding.UTF8, "application/json");

            var accessToken = FakeJwtManager.GenerateJwtToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Act
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            response.EnsureSuccessStatusCode();

            // Add further checks here !!! AANA TODO !!!
            //var content = await response.Content.ReadAsStringAsync();

        }



        [Fact]
        public async Task UpdateStaffMemberWithAuthorization_Successful()
        {

            string staffId = "24";

            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/staffmembers/update/" + staffId);

            request.Content = new StringContent(JsonSerializer.Serialize(new
            {
                Id= staffId,
                FirstName = "gDDDDDy",
                LastName = "gDDDDDDy",
                Title = "Mr"
            }), Encoding.UTF8, "application/json");

            var accessToken = FakeJwtManager.GenerateJwtToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Act
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            response.EnsureSuccessStatusCode();

        }


        [Fact]
        public async Task AddStaffMemberWithAuthorization_MissingData_FirstName_BadRequest()
        {

            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/staffmembers/create");

            request.Content = new StringContent(JsonSerializer.Serialize(new
            {
                //FirstName = "ADDDDD",
                LastName = "ADDDDDD",
                Title = "Mr"
            }), Encoding.UTF8, "application/json");

            var accessToken = FakeJwtManager.GenerateJwtToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Act
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var content = await response.Content.ReadAsStringAsync();

            var expected = HttpStatusCode.BadRequest;

            // Assert
            Assert.Equal(expected, response.StatusCode);
        }



        [Fact]
        public async Task AddStaffMemberWithAuthorization_MissingData_LastName_BadRequest()
        {

            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/staffmembers/create");

            request.Content = new StringContent(JsonSerializer.Serialize(new
            {
                FirstName = "ADDDDD",
                //LastName = "ADDDDDD",
                Title = "Mr"
            }), Encoding.UTF8, "application/json");

            var accessToken = FakeJwtManager.GenerateJwtToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Act
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var content = await response.Content.ReadAsStringAsync();

            var expected = HttpStatusCode.BadRequest;

            // Assert
            Assert.Equal(expected, response.StatusCode);
        }


        [Fact]
        public async Task AddStaffMemberWithAuthorization_MissingData_Title_BadRequest()
        {

            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/staffmembers/create");

            request.Content = new StringContent(JsonSerializer.Serialize(new
            {
                FirstName = "ADDDDD",
                //LastName = "ADDDDDD",
                //Title = "Mr"
            }), Encoding.UTF8, "application/json");

            var accessToken = FakeJwtManager.GenerateJwtToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Act
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var content = await response.Content.ReadAsStringAsync();

            var expected = HttpStatusCode.BadRequest;

            // Assert
            Assert.Equal(expected, response.StatusCode);
        }


        [Fact]
        public async Task AddStaffMemberWithAuthorization_MissingData_FirstName_LastName_Title_BadRequest()
        {

            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/staffmembers/create");

            request.Content = new StringContent(JsonSerializer.Serialize(new
            {
                //FirstName = "ADDDDD",
                //LastName = "ADDDDDD",
                //Title = "Mr"
            }), Encoding.UTF8, "application/json");

            var accessToken = FakeJwtManager.GenerateJwtToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Act
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var content = await response.Content.ReadAsStringAsync();

            var expected = HttpStatusCode.BadRequest;

            // Assert
            Assert.Equal(expected, response.StatusCode);
        }



    }
}
