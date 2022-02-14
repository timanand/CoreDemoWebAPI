using System;

using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using CoreDemoWebAPI.Data;
using CoreDemoWebAPI.Domain;


namespace CoreDemoWebAPI.Tests
{

    public class UnitTest_StaffRepository_GetById
    {
        int _defaultID = 1;
        string _defaultFirstName = "Arvinder";
        string _defaultLastName = "Anand";
        string _defaultTitle = "Mr";

        Mock<IStaffRepository> mock;
        StaffMember staffMember;


        // Constructor is the Initialise in xUnit
        public UnitTest_StaffRepository_GetById()
        {
            mock = new Mock<IStaffRepository>();

            // Arrange
            mock.Setup(x => x.GetById(1)).Returns(new StaffMember { Id = _defaultID, FirstName = _defaultFirstName, LastName = _defaultLastName, Title = _defaultTitle });

            // Act
            staffMember = mock.Object.GetById(1);
        }



        [Fact]
        public void GetById_Match_Id()
        {
            Assert.Equal(_defaultID, staffMember.Id);
        }


        [Fact]
        public void GetById_Match_FirstName()
        {
            Assert.Equal(_defaultFirstName, staffMember.FirstName);
        }


        [Fact]
        public void GetById_Match_LastName()
        {
            Assert.Equal(_defaultLastName, staffMember.LastName);
        }


        [Fact]
        public void GetById_Match_Title()
        {
            Assert.Equal(_defaultTitle, staffMember.Title);
        }


    }
}
