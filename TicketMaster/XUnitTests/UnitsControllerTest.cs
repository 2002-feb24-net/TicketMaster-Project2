using System;
using Xunit;
using DataAccess.Entities;
using Domain.Interfaces;
using Domain.Models;
using REST_Api.ApiModels;
using REST_Api.Controllers;
using System.Collections;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DataAccess.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace XUnitTests
{
    public class UsersControllerTest
    {
        private readonly UsersController _controller;
        private readonly ILogger<UsersControllerTest> _logger;
        public UsersControllerTest(ILogger<UsersControllerTest> logger)
        {
            //Arrange
            var mockRepo = new Mock<ITicketRepo>();
            var user = new Domain.Models.Users
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Email = "testemail@email.com",
                Password = "password",
            }; 
            mockRepo.Setup(repo => repo.GetUserByIdAsync(user.Id))
            .ReturnsAsync(user);
            _controller = new UsersController(mockRepo.Object);
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [Fact]
        public async void Test1()
        {
            
            var result1 = await _controller.GetByIdAsync(1);
            Assert.IsType<OkObjectResult>(result1);

            var result2 = await _controller.GetByIdAsync(2);
            Assert.IsType<NotFoundObjectResult>(result2);

            //Verify the result:
            //Assert.NotNull(actualResult);
        }

        //[Fact]
        //public void Test2()
        //{
        //    // Define a test input and output value:
        //    //var expectedResult =

        //    //string input = "stewart";
        //    // Run the method under test:
        //    //var actualResult = _user.GetbyIdAsync();
        //    // Verify the result:
        //    //Assert.NotNull(actualResult);
        //}



        //[Fact]
        //public void Add_writes_to_database()
        //{

        //    var usersController = new UsersController();


        //    // In-memory database only exists while the connection is open
        //    var connection = new SqliteConnection("DataSource=:memory:");
        //    connection.Open();

        //    try
        //    {
        //        var options = new DbContextOptionsBuilder<TicketContext>()
        //            .UseSqlite(connection)
        //            .Options;
        //        var context = new TicketContext(options);
        //        context.Database.EnsureCreated();
        //        var new ILogger<>
        //        var repo = new TicketRepo(context, );


        //        // Create the schema in the database
        //        using (var context = new TicketContext(options))
        //        {
        //            context.Database.EnsureCreated();
        //        }



        //        // Run the test against one instance of the context
        //        using (var context = new TicketContext(options))
        //        {
        //            var service = new UsersController(context);
        //            service.Add("https://example.com");
        //            context.SaveChanges();
        //        }

        //        // Use a separate instance of the context to verify correct data was saved to database
        //        using (var context = new TicketContext(options))
        //        {
        //            Assert.Equal(1, context.Blogs.Count());
        //            Assert.Equal("https://example.com", context.Blogs.Single().Url);
        //        }
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}

        //[Fact]
        //public void Find_searches_url()
        //{
        //    // In-memory database only exists while the connection is open
        //    var connection = new SqliteConnection("DataSource=:memory:");
        //    connection.Open();

        //    try
        //    {
        //        var options = new DbContextOptionsBuilder<TicketContext>()
        //            .UseSqlite(connection)
        //            .Options;

        //        // Create the schema in the database
        //        using (var context = new TicketContext(options))
        //        {
        //            context.Database.EnsureCreated();
        //        }

        //        // Insert seed data into the database using one instance of the context
        //        using (var context = new TicketContext(options))
        //        {
        //            context.Blogs.Add(new Blog { Url = "https://example.com/cats" });
        //            context.Blogs.Add(new Blog { Url = "https://example.com/catfish" });
        //            context.Blogs.Add(new Blog { Url = "https://example.com/dogs" });
        //            context.SaveChanges();
        //        }

        //        // Use a clean instance of the context to run the test
        //        using (var context = new TicketContext(options))
        //        {
        //            var service = new BlogService(context);
        //            var result = service.Find("cat");
        //            Assert.Equal(2, result.Count());
        //        }
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}

    }

}

