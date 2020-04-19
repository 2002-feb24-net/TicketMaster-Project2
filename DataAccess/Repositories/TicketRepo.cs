using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Domain.Models;
using DataAccess;

namespace REST_Api.Repositories
{
    public class TicketRepo : ITicketRepo
    {
        private readonly TicketContext _dbContext;
        private readonly ILogger<TicketRepo> _logger;

        public TicketRepo(TicketContext dbContext, ILogger<TicketRepo> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Get all users with deferred execution.
        /// </summary>
        /// <returns>The collection of users</returns>
        public IEnumerable<Domain.Models.Users> GetUsers(string search = null)
        {
            IEnumerable<DataAccess.Entities.Users> items = _dbContext.Users
                .AsNoTracking();

            items = items.Where(u => u.LastName.Contains(search)).AsEnumerable();

            return items.Select(Mapper.MapUsers);
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <returns>The user</returns>
        public Domain.Models.Users GetUserById(int userId)
        {
            DataAccess.Entities.Users item = _dbContext.Users
                .FirstOrDefault(u => u.Id == userId);

            return Mapper.MapUsers(item);
        }

        /// <summary>
        /// Add a useer.
        /// </summary>
        /// <param name="user">The user</param>
        public void AddUser(Domain.Models.Users user)
        {
            if (user.Id != 0)
            {
                _logger.LogWarning("User to be added has an ID ({userId}) already: ignoring.", user.Id);
            }

            _logger.LogInformation("Adding user");

            var entity = Mapper.MapUsers(user);

            entity.Id = 0;
            _dbContext.Users.Add(entity);
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="user">The user with changed values</param>
        public void UpdateUser(Domain.Models.Users user)
        {
            _logger.LogInformation("Updating user with ID {userId}", user.Id);

            DataAccess.Entities.Users currentEntity = _dbContext.Users.Find(user.Id);
            var newEntity = Mapper.MapUsers(user);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// Delete a user by ID. 
        /// </summary>
        /// <param int="userId">The ID of the user</param>
        public void DeleteUser(int userId)
        {
            _logger.LogInformation("Deleting user with ID {userId}", userId);
            DataAccess.Entities.Users entity = _dbContext.Users.Find(userId);
            _dbContext.Users.Remove(entity);
        }

        /// <summary>
        /// Get a user by their login email and password.
        /// </summary>
        /// <returns>The user</returns>
        public Domain.Models.Users GetUserByLogin(string email, string password)
        {
            DataAccess.Entities.Users item = _dbContext.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            return Mapper.MapUsers(item);
        }

        /// <summary>
        /// Add an administrator.
        /// </summary>
        /// <param name="admin">The administrator</param>
        public void AddAdmin(Domain.Models.Admins admin)
        {
            if (admin.Id != 0)
            {
                _logger.LogWarning("Administrator to be added has an ID ({adminId}) already: ignoring.", admin.Id);
            }

            _logger.LogInformation("Adding administrator");

            var entity = Mapper.MapAdmins(admin);

            entity.Id = 0;
            _dbContext.Admins.Add(entity);
        }

        /// <summary>
        /// Update an admin's info.
        /// </summary>
        /// <param name="admin">The administrator with changed values</param>
        public void UpdateAdmin(Domain.Models.Admins admin)
        {
            _logger.LogInformation("Updating administrator with ID {adminId}", admin.Id);

            DataAccess.Entities.Admins currentEntity = _dbContext.Admins.Find(admin.Id);
            var newEntity = Mapper.MapAdmins(admin);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// Delete a administrator by ID.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        public void DeleteAdmin(int adminId)
        {
            _logger.LogInformation("Deleting administrator with ID {adminId}", adminId);
            DataAccess.Entities.Admins entity = _dbContext.Admins.Find(adminId);
            _dbContext.Admins.Remove(entity);
        }

        /// <summary>
        /// Get all administrators with deferred execution.
        /// </summary>
        /// <returns>The collection of administrators</returns>
        public IEnumerable<Domain.Models.Admins> GetAdmins(string search = null)
        {
            IEnumerable<DataAccess.Entities.Admins> items = _dbContext.Admins
                .AsNoTracking();

            items = items.Where(a => a.LastName.Contains(search)).AsEnumerable();

            return items.Select(Mapper.MapAdmins);
        }

        /// <summary>
        /// Get a administrator by ID.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        public Domain.Models.Admins GetAdminById(int adminId)
        {
            DataAccess.Entities.Admins item = _dbContext.Admins
                .FirstOrDefault(a => a.Id == adminId);

            return Mapper.MapAdmins(item);
        }

        /// <summary>
        /// Get a administrator by their login email and password.
        /// </summary>
        /// <returns>The administrator</returns>
        public Domain.Models.Admins GetAdminByLogin(string email, string password)
        {
            DataAccess.Entities.Admins item = _dbContext.Admins
                .FirstOrDefault(a => a.Email == email && a.Password == password);

            return Mapper.MapAdmins(item);
        }

        /// <summary>
        /// Get all tickets with deferred execution.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        public IEnumerable<Domain.Models.Tickets> GetTickets(string search = null)
        {
            IEnumerable<DataAccess.Entities.Tickets> items = _dbContext.Tickets
                .Include(c => c.Comments)
                .AsNoTracking();

            items = items.Where(a => a.Title.Contains(search)).AsEnumerable();

            return items.Select(Mapper.MapTickets);
        }

        /// <summary>
        /// Get a ticket by ID.
        /// </summary>
        /// <returns>The ticket</returns>
        public Domain.Models.Tickets GetTicketById(int ticketId)
        {
            DataAccess.Entities.Tickets item = _dbContext.Tickets
                .Include(c => c.Comments)
                .FirstOrDefault(t => t.Id == ticketId);

            return Mapper.MapTickets(item);
        }

        /// <summary>
        /// Add a ticket and associate it with a user.
        /// </summary>
        /// <param name="ticket">The ticket</param>
        public void AddTicket(Domain.Models.Tickets ticket)
        {
            if (ticket.Id != 0)
            {
                _logger.LogWarning("Ticket to be added has an ID ({ticketId}) already: ignoring.", ticket.Id);
            }

            _logger.LogInformation("Adding Ticket");

            var entity = Mapper.MapTickets(ticket);

            entity.Id = 0;
            _dbContext.Tickets.Add(entity);
        }

        /// <summary>
        /// Update a ticket.
        /// </summary>
        /// <param name="ticket">The ticket with changed values</param>
        public void UpdateTicket(Domain.Models.Tickets ticket)
        {
            _logger.LogInformation("Updating ticket with ID {ticketId}", ticket.Id);

            DataAccess.Entities.Tickets currentEntity = _dbContext.Tickets.Find(ticket.Id);
            var newEntity = Mapper.MapTickets(ticket);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// Delete a ticket by ID.
        /// </summary>
        /// <param int="ticketId">The ID of the ticket</param>
        public void DeleteTicket(int ticketId)
        {
            _logger.LogInformation("Deleting ticket with ID {ticketId}", ticketId);
            DataAccess.Entities.Tickets entity = _dbContext.Tickets.Find(ticketId);
            _dbContext.Tickets.Remove(entity);
        }

        /// <summary>
        /// Get all tickets according to user Id.
        /// </summary>
        /// <param int="userId">The ID of the user</param>
        /// <returns>The collection of tickets</returns>
        public IEnumerable<Domain.Models.Tickets> GetTicketsByUserId(int userId)
        {
            IEnumerable<DataAccess.Entities.Tickets> items = _dbContext.Tickets
                .Include(c => c.Comments)
                .AsNoTracking();

            items = items.Where(t => t.UserId == userId).AsEnumerable();

            return items.Select(Mapper.MapTickets);
        }

        /// <summary>
        /// Get all tickets according to administrator Id.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        /// <returns>The collection of tickets</returns>
        public IEnumerable<Domain.Models.Tickets> GetTicketsByAdminId(int adminId)
        {
            IEnumerable<DataAccess.Entities.Tickets> items = _dbContext.Tickets
                .Include(c => c.Comments)
                .AsNoTracking();

            items = items.Where(t => t.AdminId == adminId).AsEnumerable();

            return items.Select(Mapper.MapTickets);
        }

        /// <summary>
        /// Get all tickets according to store Id.
        /// </summary>
        /// <param int="storeId">The ID of the store</param>
        /// <returns>The collection of tickets</returns>
        public IEnumerable<Domain.Models.Tickets> GetTicketsByStoreId(int storeId)
        {
            IEnumerable<DataAccess.Entities.Tickets> items = _dbContext.Tickets
                .Include(c => c.Comments)
                .AsNoTracking();

            items = items.Where(t => t.StoreId == storeId).AsEnumerable();

            return items.Select(Mapper.MapTickets);
        }

        /// <summary>
        /// Search all tickets by text string.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        public IEnumerable<Domain.Models.Tickets> GetTicketsByText(string search)
        {
            IEnumerable<DataAccess.Entities.Tickets> items = _dbContext.Tickets
                .Include(c => c.Comments)
                .AsNoTracking();

            items = items.Where(t => t.Title.Contains(search) 
               || t.Details.Contains(search)).AsEnumerable();

            return items.Select(Mapper.MapTickets);
        }

        /// <summary>
        /// Search all tickets by datetime.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        //public IEnumerable<Domain.Models.Tickets> GetTicketsByDatetime(string datetime)
        //{

        //}

        /// <summary>
        /// Add a comment to an existing ticket, and associate it with a administrator.
        /// </summary>
        /// <param name="comment">The comment</param>
        /// <param int="ticketId">The id of the ticket</param>
        public void AddComment(Domain.Models.Comments comment, Domain.Models.Tickets ticket)
        {
            if (comment.Id != 0)
            {
                _logger.LogWarning("Comment to be added has an ID ({commentId}) already: ignoring.", comment.Id);
            }

            _logger.LogInformation("Adding comment to ticket with ID {ticketId}", ticket.Id);

            if (ticket != null)
            {

                DataAccess.Entities.Tickets ticketEntity = _dbContext.Tickets
                    .Include(c => c.Comments)
                    .First(t => t.Id == ticket.Id);
                DataAccess.Entities.Comments newEntity = Mapper.MapComments(comment);
                ticketEntity.Comments.Add(newEntity);
  
                ticket.Comments.Add(comment);
            }
            else
            {
                var entity = Mapper.MapComments(comment);

                entity.Id = 0;
                _dbContext.Comments.Add(entity);
            }

            
        }

        /// <summary>
        /// Delete a comment by ID.
        /// </summary>
        /// <param int="commentId">The ID of the comment</param>
        public void DeleteComment(int commentId)
        {
            _logger.LogInformation("Deleting comment with ID {commentId}", commentId);
            DataAccess.Entities.Comments entity = _dbContext.Comments.Find(commentId);
            _dbContext.Comments.Remove(entity);
        }

        /// <summary>
        /// Get all comments according to ticket Id.
        /// </summary>
        /// <param int="ticketId">The ID of the ticket</param>
        /// <returns>The collection of comments</returns>
        public IEnumerable<Domain.Models.Comments> GetCommentsByTicketId(int ticketId)
        {
            IEnumerable<DataAccess.Entities.Comments> items = _dbContext.Comments
                .AsNoTracking();

            items = items.Where(c => c.TicketId == ticketId).AsEnumerable();

            return items.Select(Mapper.MapComments);
        }

        /// <summary>
        /// Get all comments according to administrator Id.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        /// <returns>The collection of comments</returns>
        public IEnumerable<Domain.Models.Comments> GetCommentsByAdminId(int adminId)
        {
            IEnumerable<DataAccess.Entities.Comments> items = _dbContext.Comments
                .AsNoTracking();

            items = items.Where(c => c.AdminId == adminId).AsEnumerable();

            return items.Select(Mapper.MapComments);
        }

        /// <summary>
        /// Persist changes to the data source.
        /// </summary>
        public void Save()
        {
            _logger.LogInformation("Saving changes to the database");
            _dbContext.SaveChanges();
        }

    }
}
