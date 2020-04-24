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
using System;

namespace XUnitTests
{
    public class TicketsControllerTest
    {
        private readonly TicketsController _controller;
        //private readonly ILogger<UsersControllerTest> _logger;
        Mock<ITicketRepo> mockRepo = new Mock<ITicketRepo>();
        private Domain.Models.Tickets ticket = new Domain.Models.Tickets
        {
            Id = 1,
            Title = "Test",
            Details = "Ticket",
            UserId = 1,
            DatetimeOpened = DateTime.Now,
            Priority = "low",
            Completed = "Closed",
            AdminId = 1,
            StoreId = 1,
        };

        private Domain.Models.Admins admin = new Domain.Models.Admins
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Admin",
            Email = "tester@email.com",
            Password = "password",
            SupportLevel = 1
        };

        private Domain.Models.Users user = new Domain.Models.Users
        {
            Id = 1,
            FirstName = "Test",
            LastName = "User",
            Email = "tester@email.com",
            Password = "password",
        };

        private Domain.Models.Stores store = new Domain.Models.Stores
        {
            Id = 1,
            Address = "Test",
            City = "User",
            State = "tester@email.com",
            PhoneNumber = 1234567890,
        };

        List<Domain.Models.Tickets> list = new List<Domain.Models.Tickets>();

        public TicketsControllerTest(/*ILogger<UsersControllerTest> logger*/)
        {
            //Arrange

            list.Add(ticket);
            var tickets = list.AsEnumerable();
            
            mockRepo.Setup(repo => repo.GetTicketsAsync("Ticket"))
           .ReturnsAsync(tickets);
            mockRepo.Setup(repo => repo.GetAdminByIdAsync(admin.Id))
            .ReturnsAsync(admin);
            mockRepo.Setup(repo => repo.GetTicketsByAdminAsync(admin.Id))
            .ReturnsAsync(tickets);
            mockRepo.Setup(repo => repo.GetStoreByIdAsync(store.Id))
            .ReturnsAsync(store);
            mockRepo.Setup(repo => repo.GetTicketsByStoreAsync(store.Id))
            .ReturnsAsync(tickets);
            mockRepo.Setup(repo => repo.GetUserByIdAsync(user.Id))
            .ReturnsAsync(user);
            mockRepo.Setup(repo => repo.GetTicketsByUserAsync(user.Id))
            .ReturnsAsync(tickets);
            mockRepo.Setup(repo => repo.GetTicketByIdAsync(ticket.Id))
            .ReturnsAsync(ticket);
            mockRepo.Setup(repo => repo.AddTicketAsync(ticket))
            .Verifiable("ticket was not added");
            mockRepo.Setup(repo => repo.GetLatestTicketAsync())
            .ReturnsAsync(ticket);
            mockRepo.Setup(repo => repo.UpdateTicketAsync(ticket.Id, ticket))
            .Verifiable("ticket was not updated");
            mockRepo.Setup(repo => repo.CloseTicketAsync(ticket.Id))
            .Verifiable("ticket was not updated");
            mockRepo.Setup(repo => repo.ReassignTicketAsync(ticket.Id, admin.Id))
            .ReturnsAsync(ticket);
            mockRepo.Setup(repo => repo.SaveAsync())
           .ReturnsAsync(true);
            mockRepo.Setup(repo => repo.DeleteTicketAsync(ticket.Id))
           .Verifiable("item was not removed");
            _controller = new TicketsController(mockRepo.Object);
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [Fact]
        public async void Test1()
        {
            
            var tickets = list.AsEnumerable();
            var result1 = await _controller.GetAsync("Ticket");
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            Assert.Same(tickets.Select(Mapper.MapTickets).ToString(), goodRequestResult.Value.ToString());

            var result2 = await _controller.GetAsync("404");
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async void Test2()
        {
            var tickets = list.AsEnumerable();
            var result1 = await _controller.GetByIdAsync("admin", admin.Id);
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            Assert.Same(tickets.Select(Mapper.MapTickets).ToString(), goodRequestResult.Value.ToString());

            var result2 = await _controller.GetByIdAsync("store", store.Id);
            var goodRequest2Result = Assert.IsType<OkObjectResult>(result2);
            Assert.Same(tickets.Select(Mapper.MapTickets).ToString(), goodRequestResult.Value.ToString());

            var result3 = await _controller.GetByIdAsync("user", user.Id);
            var goodRequest3Result = Assert.IsType<OkObjectResult>(result3);
            Assert.Same(tickets.Select(Mapper.MapTickets).ToString(), goodRequestResult.Value.ToString());

            var result4 = await _controller.GetByIdAsync("ticket", ticket.Id);
            var goodRequest4Result = Assert.IsType<OkObjectResult>(result4);
            Assert.Same(Mapper.MapTickets(ticket).ToString(), goodRequest4Result.Value.ToString());

            var result5 = await _controller.GetByIdAsync("else", 2);
            Assert.IsType<BadRequestObjectResult>(result5);

        }


        //[Fact]
        //public async void Test2()
        //{
        //    var result1 = await _controller.GetByLoginAsync("tester@email.com", "password");
        //    var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
        //    Assert.Same(Mapper.MapTickets(ticket).ToString(), goodRequestResult.Value.ToString());

        //    var result2 = await _controller.GetByLoginAsync("tester@email.com", "1234");
        //    Assert.IsType<NotFoundObjectResult>(result2);
        //}

        [Fact]
        public async void Test3()
        {
            var ticket2 = new REST_Api.ApiModels.Tickets
            {
                Id = 2,
                Title = "Test",
                Details = "Ticket2",
                UserId = 1,
                DatetimeOpened = DateTime.Now,
                Priority = "low",
                Completed = "Closed"
            };
            var result1 = await _controller.PostAsync(ticket2);
            Assert.IsType<OkObjectResult>(result1);

        }

        [Fact]
        public async void Test4()
        {
            var ticket2 = new REST_Api.ApiModels.Tickets
            {
                Id = 1,
                Title = "Test",
                Details = "Ticket2",
                UserId = 2,
                DatetimeOpened = DateTime.Now,
                Priority = "low",
                Completed = "Closed"
            };
            var result1 = await _controller.PutAsync(ticket2);
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            Assert.Same(Mapper.MapTickets(ticket2).ToString(), goodRequestResult.Value.ToString());

            ticket2.Id = 3;
            var result2 = await _controller.PutAsync(ticket2);
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async void Test5()
        {
            var ticket2 = new REST_Api.ApiModels.Tickets
            {
                Id = 1,
                Title = "Test",
                Details = "Ticket",
                UserId = 1,
                DatetimeOpened = DateTime.Now,
                Priority = "low",
                Completed = "Closed"
            };
            var result1 = await _controller.PutToCloseTicketAsync(ticket2.Id);
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            //Assert.Same(Mapper.MapTickets(ticket2).ToString(), goodRequestResult.Value.ToString());

            var result2 = await _controller.PutToCloseTicketAsync(5);
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async void Test6()
        {
            var ticket2 = new REST_Api.ApiModels.Tickets
            {
                Id = 1,
                Title = "Test",
                Details = "Ticket",
                UserId = 1,
                DatetimeOpened = DateTime.Now,
                Priority = "low",
                Completed = "Closed",
                AdminId = 1,
                AdminAssignedName = "Me"
            };
            var result1 = await _controller.PutToReassignTicketAsync(ticket2.Id, admin.Id);
            var goodRequestResult = Assert.IsType<OkObjectResult>(result1);
            //Assert.Same(Mapper.MapTickets(ticket2).ToString(), goodRequestResult.Value.ToString());

            var result2 = await _controller.PutToReassignTicketAsync(5, 9);
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async void Test7()
        {
            var result1 = await _controller.DeleteAsync(ticket.Id);
            Assert.IsType<OkObjectResult>(result1);

            var result2 = await _controller.DeleteAsync(5);
            Assert.IsType<NotFoundObjectResult>(result2);
        }

        
    }

}

