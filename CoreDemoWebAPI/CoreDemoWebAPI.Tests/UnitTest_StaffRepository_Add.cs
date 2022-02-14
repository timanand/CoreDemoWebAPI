using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using CoreDemoWebAPI.Data;
using Moq;
using Microsoft.Extensions.Configuration;
// AANA
using CoreDemoWebAPI.Data;
using CoreDemoWebAPI.Domain;



namespace CoreDemoWebAPI.Tests
{
    public class UnitTest_StaffRepository_Add
    {

        [Fact]
        public void Add_Returns_true()
        {
            StaffMember staffMember = new StaffMember { Id = 1, FirstName = "Arvinder", LastName = "Anand", Title = "Mr" };

            // Arrange
            Mock<IStaffRepository> mock = new Mock<IStaffRepository>();
            mock.Setup(x => x.Add(staffMember)).Returns(true);

            // Act
            bool rtn = mock.Object.Add(staffMember);

            // Asset
            Assert.True(rtn);
        }



        [Fact]
        public void Add_Returns_false()
        {
            StaffMember staffMember = new StaffMember { Id = 1, FirstName = "Arvinder", LastName = "Anand", Title = "Mr" };

            // Arrange
            Mock<IStaffRepository> mock = new Mock<IStaffRepository>();
            mock.Setup(x => x.Add(staffMember)).Returns(false) ;

            // Act
            bool rtn = mock.Object.Add(staffMember);

            // Asset
            Assert.False(rtn);
        }


    }

}
