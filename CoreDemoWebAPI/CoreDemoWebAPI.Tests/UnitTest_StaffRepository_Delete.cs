using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using CoreDemoWebAPI.Data;
using CoreDemoWebAPI.Domain;


namespace CoreDemoWebAPI.Tests
{
    public class UnitTest_StaffRepository_Delete
    {

        [Fact]
        public void Delete_Returns_true()
        {
            StaffMember staffMember = new StaffMember { Id = 1, FirstName = "Arvinder", LastName = "Anand", Title = "Mr" };

            // Arrange
            Mock<IStaffRepository> mock = new Mock<IStaffRepository>();
            mock.Setup(x => x.Delete(1)).Returns(true);

            // Act
            bool rtn = mock.Object.Delete(1);

            // Asset
            Assert.True(rtn);
        }



        [Fact]
        public void Delete_Returns_false()
        {
            StaffMember staffMember = new StaffMember { Id = 1, FirstName = "Arvinder", LastName = "Anand", Title = "Mr" };

            // Arrange
            Mock<IStaffRepository> mock = new Mock<IStaffRepository>();
            mock.Setup(x => x.Delete(1)).Returns(false);

            // Act
            bool rtn = mock.Object.Delete(1);

            // Asset
            Assert.False(rtn);
        }


    }

}
