using System;

using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using CoreDemoWebAPI.Data;
using CoreDemoWebAPI.Domain;


namespace CoreDemoWebAPI.Tests
{

    public class UnitTest_StaffRepository_SearchEmployees
    {
        int _defaultID = 1;
        string _defaultFirstName = "Arvinder";
        string _defaultLastName = "Anand";
        string _defaultTitle = "Mr";


        List<StaffMember> listStaff = new List<StaffMember>();
        List<StaffMember> listStaffReturned;


        // Constructor
        public UnitTest_StaffRepository_SearchEmployees()
        {
            listStaff.Add(new StaffMember { Id = _defaultID, FirstName = _defaultFirstName, LastName = _defaultLastName, Title = _defaultTitle });

            StaffMember staffMember = new StaffMember { Id = 1, FirstName = "Arvinder", LastName = "Anand", Title = "Mr" };

            // Arrange
            Mock<IStaffRepository> mock = new Mock<IStaffRepository>();
            mock.Setup(x => x.SearchEmployees("")).Returns(listStaff);

            // Act
            listStaffReturned = mock.Object.SearchEmployees("");
        }


        [Fact]
        public void SearchEmployees_Returns_ListOfOneStaffMember()
        {
            // Assert
            // Expects only 1 item in listStaffReturned
            Assert.Single(listStaffReturned);
        }


        [Fact]
        public void SearchEmployees_StaffMemberInList_Match_Id()
        {
            // Ensure Id in listbox matches default Id
            Assert.Equal(_defaultID, listStaffReturned[0].Id);
        }




        [Fact]
        public void SearchEmployees_StaffMemberInList_Match_FirstName()
        {
            // Ensure FirstName in listbox matches default FirstName
            Assert.Equal(_defaultFirstName, listStaffReturned[0].FirstName);
        }



        [Fact]
        public void SearchEmployees_StaffMemberInList_Match_LastName()
        {
            // Ensure LastName in listbox matches default LastName
            Assert.Equal(_defaultLastName, listStaffReturned[0].LastName);
        }



        [Fact]
        public void SearchEmployees_StaffMemberInList_Match_Title()
        {
            // Ensure Title in listbox matches default Title
            Assert.Equal(_defaultTitle, listStaffReturned[0].Title);
        }



    }


}
