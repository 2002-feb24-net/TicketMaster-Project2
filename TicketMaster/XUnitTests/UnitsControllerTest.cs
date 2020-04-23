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

namespace XUnitTests
{
    public class UsersControllerTest
    {
        //private readonly ITicketRepo _repo;
        //private readonly UsersController _user;

        //public UserTests(ITicketRepo repo, UsersController user)
        //{
        //    _repo = repo;
        //    _user = user;
        //}

        //[Fact]
        //public void Test1()
        //{            
        //    // Define a test input and output value:
        //    //var expectedResult = new IEnumerable<Domain.Models.Users>;
            
        //    //string input = "stewart";
        //    // Run the method under test:
        //    //var actualResult = _user.GetAsync();
        //    // Verify the result:
        //    //Assert.NotNull(actualResult);    
        //}

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

