using System;
using System.Linq;

using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using CoreDemoWebAPI.Data;
using CoreDemoWebAPI.Domain;


namespace CoreDemoWebAPI.Tests
{
    public class UnitTest_StaffRepository_GetAll
    {
        int _defaultID = 1;
        string _defaultFirstName = "Arvinder";
        string _defaultLastName = "Anand";
        string _defaultTitle = "Mr";

        List<StaffMember> listStaff = new List<StaffMember>();
        Mock<IStaffRepository> mock;

        List<StaffMember> listStaffReturned;


        // Constructor is the Initialise in xUnit
        public UnitTest_StaffRepository_GetAll()
        {
            listStaff.Add(new StaffMember { Id = _defaultID, FirstName = _defaultFirstName, LastName = _defaultLastName, Title = _defaultTitle });
            mock = new Mock<IStaffRepository>();

            // Arrange
            mock.Setup(x => x.GetAll("")).Returns(listStaff);

            // Act
            listStaffReturned = mock.Object.GetAll();
        }



        [Fact]
        public void GetAll_Returns_ListOfOneStaffMember()
        {

            // Expects only 1 item in listStaffReturned
            Assert.Single(listStaffReturned);

            // Alternative Assert
            //int NoItemsReturned = listStaffReturned.Count;            
            //Assert.Equal(1, NoItemsReturned);
        }



        [Fact]
        public void GetAll_StaffMemberInList_Match_Id()
        {
            // Ensure Id in listbox matches default Id
            Assert.Equal(_defaultID, listStaffReturned[0].Id);
        }



        [Fact]
        public void GetAll_StaffMemberInList_Match_FirstName()
        {
            // Ensure FirstName in listbox matches default FirstName
            Assert.Equal(_defaultFirstName, listStaffReturned[0].FirstName);
        }



        [Fact]
        public void GetAll_StaffMemberInList_Match_LastName()
        {
            // Ensure LastName in listbox matches default LastName
            Assert.Equal(_defaultLastName, listStaffReturned[0].LastName);
        }



        [Fact]
        public void GetAll_StaffMemberInList_Match_Title()
        {
            // Ensure Title in listbox matches default Title
            Assert.Equal(_defaultTitle, listStaffReturned[0].Title);
        }


    }
}
