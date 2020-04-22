using System;
using Xunit;
using DataAccess.Entities;
using Domain.Interfaces;
using Domain.Models;
using REST_Api.ApiModels;
using REST_Api.Controllers;
using System.Collections;

namespace XUnitTests
{
    public class UserTests
    {
        private readonly ITicketRepo _repo;
        private readonly UsersController _user;

        public UserTests(ITicketRepo repo, UsersController user)
        {
            _repo = repo;
            _user = user;
        }

        [Fact]
        public void Test1()
        {            
            // Define a test input and output value:
            //var expectedResult = new IEnumerable<Domain.Models.Users>;
            
            //string input = "stewart";
            // Run the method under test:
            //var actualResult = _user.GetAsync();
            // Verify the result:
            //Assert.NotNull(actualResult);    
        }

        [Fact]
        public void Test2()
        {
            // Define a test input and output value:
            //var expectedResult =

            //string input = "stewart";
            // Run the method under test:
            //var actualResult = _user.GetbyIdAsync();
            // Verify the result:
            //Assert.NotNull(actualResult);
        }

    }
}
