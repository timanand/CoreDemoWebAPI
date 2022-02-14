using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using System.Net;
using CoreDemoWebAPI.Domain;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;


namespace CoreDemoWebAPI.IntegrationTests
{
    public class SystemIntegrationTest_ReturnsUnauthorizedResult
    {

        [Fact]
        public async Task GetAsync_ReadStaffMembers_InvalidScope_ReturnsUnauthorizedResult()
        {

            // Arrange
            using (var client = new TestClientProvider().Client)
            {
                // Act
                var response = await client.GetAsync("/api/staffmembers/read");
                var expected = HttpStatusCode.Unauthorized;

                // Assert
                Assert.Equal(expected, response.StatusCode);
            }

        }


        [Fact]
        public async Task GetAsync_AddStaffMembers_InvalidScope_ReturnsUnauthorizedResult()
        {

            // Arrange
            using (var client = new TestClientProvider().Client)
            {
                // Act
                var response = await client.PostAsync("/api/staffmembers/create"
                    , new StringContent(
                        JsonConvert.SerializeObject(new StaffMember() { FirstName = "BBBBBBB", LastName = "BBBBBB", Title = "Mr" })
                        ,Encoding.UTF8, "application/json"));

                var expected = HttpStatusCode.Unauthorized;

                // Assert
                Assert.Equal(expected, response.StatusCode);


            }
        }


        [Fact]
        public async Task GetAsync_UpdateStaffMembers_InvalidScope_ReturnsUnauthorizedResult()
        {

            // Arrange
            int StaffMemberId = 21;

            using (var client = new TestClientProvider().Client)
            {
                // Act
                var response = await client.PutAsync("/api/staffmembers/update/" + StaffMemberId.ToString()
                    , new StringContent(
                        JsonConvert.SerializeObject(new StaffMember() { Id = StaffMemberId, FirstName = "Belinda", LastName = "Lancaster", Title = "Miss" })
                        , Encoding.UTF8, "application/json"));

                var expected = HttpStatusCode.Unauthorized;

                // Assert
                Assert.Equal(expected, response.StatusCode);


            }
        }


        [Fact]
        public async Task GetAsync_DeleteStaffMember_InvalidScope_ReturnsUnauthorizedResult()
        {

            // Arrange
            int StaffMemberId = 21;

            using (var client = new TestClientProvider().Client)
            {
                // Act
                //var response = await client.PutAsync("/api/staffmembers/delete/" + StaffMemberId.ToString()
                //    , new StringContent(
                //        JsonConvert.SerializeObject(new StaffMember() { Id = StaffMemberId, FirstName = "Belinda", LastName = "Lancaster", Title = "Miss" })
                //        , Encoding.UTF8, "application/json"));

                var response = await client.DeleteAsync("/api/staffmembers/delete/" + StaffMemberId.ToString());

                var expected = HttpStatusCode.Unauthorized;

                // Assert
                Assert.Equal(expected, response.StatusCode);


            }
        }



    }
}
