using Xunit;
using Domain.Interfaces;
using Domain.Models;
using REST_Api.ApiModels;
using REST_Api.Controllers;
using System.Linq;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using REST_Api;

namespace XUnitTests
{
    public class AdminsControllerTest
    {
        private readonly AdminsController _controller;
        //private readonly ILogger<UsersControllerTest> _logger;
        Mock<ITicketRepo> mockRepo = new Mock<ITicketRepo>();
        private Domain.Models.Admins admin = new Domain.Models.Admins
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Admin",
            Email = "tester@email.com",
            Password = "password",
            SupportLevel = 1
        };

        List<Domain.Models.Admins> admins = new List<Domain.Models.Admins>();

        public AdminsControllerTest(/*ILogger<UsersControllerTest> logger*/)
        {
            //Arrange

            admins.Add(admin);
            admins.AsEnumerable();

            mockRepo.Setup(repo => repo.GetAdminsAsync("Admin"))
           .ReturnsAsync(admins);
            mockRepo.Setup(repo => repo.GetAdminByIdAsync(admin.Id))
            .ReturnsAsync(admin);
            mockRepo.Setup(repo => repo.GetAdminByLoginAsync(admin.Email, admin.Password))
            .ReturnsAsync(admin);
            mockRepo.Setup(repo => repo.GetAdminByEmailAsync(admin.Email))
            .ReturnsAsync(admin);
            mockRepo.Setup(repo => repo.AddAdminAsync(admin))
            .Verifiable("admin was not added");
            mockRepo.Setup(repo => repo.UpdateAdminAsync(admin.Id, admin))
            .ReturnsAsync(admin);
            mockRepo.Setup(repo => repo.UpdateAdminPasswordAsync(admin.Id, admin.Password))
            .ReturnsAsync(admin);
            mockRepo.Setup(repo => repo.SaveAsync())
           .ReturnsAsync(true);
            mockRepo.Setup(repo => repo.DeleteAdminAsync(1))
           .Verifiable("item was not removed");
            _controller = new AdminsController(mockRepo.Object);
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [Fact]
        public async void Test1()
        {
            var result1 = await _controller.GetByIdAsync(1);
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            Assert.Same(Mapper.MapAdmins(admin).ToString(), goodRequestResult.Value.ToString());

            var result2 = await _controller.GetByIdAsync(2);
            Assert.IsType<NotFoundObjectResult>(result2);

        }


        [Fact]
        public async void Test2()
        {
            var result1 = await _controller.GetByLoginAsync("tester@email.com", "password");
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            Assert.Same(Mapper.MapAdmins(admin).ToString(), goodRequestResult.Value.ToString());

            var result2 = await _controller.GetByLoginAsync("tester@email.com", "1234");
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async void Test3()
        {
            var admin2 = new REST_Api.ApiModels.Admins
            {
                Id = 2,
                FirstName = "Test3",
                LastName = "Admin",
                Email = "tester3@email.com",
                Password = "password",
            };
            var result1 = await _controller.PostAsync(admin2);
            Assert.IsType<OkObjectResult>(result1);


            admin2.Email = "tester@email.com";
            var result2 = await _controller.PostAsync(admin2);
            Assert.IsType<BadRequestObjectResult>(result2);
        }

        [Fact]
        public async void Test4()
        {
            var admin2 = new REST_Api.ApiModels.Admins
            {
                Id = 2,
                FirstName = "Test3",
                LastName = "Admin",
                Email = "tester3@email.com",
                Password = "password",
            };
            var result1 = await _controller.PutAsync(1, admin2);
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            Assert.Same(Mapper.MapAdmins(admin2).ToString(), goodRequestResult.Value.ToString());

            var result2 = await _controller.PutAsync(4, admin2);
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async void Test5()
        {
            var admin2 = new REST_Api.ApiModels.Admins
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Admin",
                Email = "tester@email.com",
                Password = "new password",
            };
            var result1 = await _controller.PutToChangePasswordAsync(1, admin2.Password);
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            //Assert.Same(Mapper.MapAdmins(admin2).ToString(), goodRequestResult.Value.ToString());


            var result2 = await _controller.PutToChangePasswordAsync(5, "1234");
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async void Test6()
        {
            var result1 = await _controller.DeleteAsync(1);
            Assert.IsType<OkObjectResult>(result1);

            var result2 = await _controller.DeleteAsync(5);
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async void Test7()
        {
            var result1 = await _controller.GetAsync("Admin");
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            Assert.Same(admins.Select(Mapper.MapAdmins).ToString(), goodRequestResult.Value.ToString());

            var result2 = await _controller.GetAsync("404");
            Assert.IsType<NotFoundObjectResult>(result2);
        }
    }

}
