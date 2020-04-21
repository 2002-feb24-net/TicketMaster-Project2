using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Domain.Models;
using DataAccess;
using System.Threading.Tasks;

namespace DataAccess.Repositories
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
        public async Task<IEnumerable<Domain.Models.Users>> GetUsersAsync(string search = null)
        {
            IQueryable<DataAccess.Entities.Users> items = _dbContext.Users
                .AsNoTracking();

            var list = await items.ToListAsync();

            if(search != null)
            {
                list.Where(u => u.LastName.Contains(search));
            }
            
            return list.Select(Mapper.MapUsers);
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <returns>The user</returns>
        public async Task<Domain.Models.Users> GetUserByIdAsync(int userId)
        {
            DataAccess.Entities.Users item = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (item is null)
            {
                return null;
                //throw new ArgumentException("User does not exist");
            }

            return Mapper.MapUsers(item);
        }

        /// <summary>
        /// Add a useer.
        /// </summary>
        /// <param name="user">The user</param>
        public async void AddUserAsync(Domain.Models.Users user)
        {
            if (user.Id != 0)
            {
                _logger.LogWarning("User to be added has an ID ({userId}) already: ignoring.", user.Id);
            }

            _logger.LogInformation("Adding user");

            var entity = Mapper.MapUsers(user);

            entity.Id = 0;
            await _dbContext.Users.AddAsync(entity);
            
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="user">The user with changed values</param>
        public async Task<Domain.Models.Users> UpdateUserAsync(int id, Domain.Models.Users user)
        {
            _logger.LogInformation("Updating user with ID {userId}", id);

            DataAccess.Entities.Users currentEntity = await _dbContext.Users.FindAsync(id);
            var newEntity = Mapper.MapUsers(user);
            newEntity.Id = id;
            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            return user;
        }

        /// <summary>
        /// Delete a user by ID. 
        /// </summary>
        /// <param int="userId">The ID of the user</param>
        public async void DeleteUserAsync(int userId)
        {
            _logger.LogInformation("Deleting user with ID {userId}", userId);
            DataAccess.Entities.Users entity = await _dbContext.Users.FindAsync(userId);
            _dbContext.Users.Remove(entity);
        }

        /// <summary>
        /// Get a user by their login email and password.
        /// </summary>
        /// <returns>The user</returns>
        public async Task<Domain.Models.Users> GetUserByLoginAsync(string email, string password)
        {
            DataAccess.Entities.Users item = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return Mapper.MapUsers(item);
        }

        /// <summary>
        /// Get a user by their login email and password.
        /// </summary>
        /// <returns>The user</returns>
        public async Task<Domain.Models.Users> GetUserByEmailAsync(string email)
        {
            DataAccess.Entities.Users item = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            //if (item is null)
            //{
            //    throw new ArgumentException("User does not exist");
            //}

            return Mapper.MapUsers(item);
        }

        /// <summary>
        /// Add an administrator.
        /// </summary>
        /// <param name="admin">The administrator</param>
        public async Task<Domain.Models.Admins> AddAdminAsync(Domain.Models.Admins admin)
        {
            if (admin.Id != 0)
            {
                _logger.LogWarning("Administrator to be added has an ID ({adminId}) already: ignoring.", admin.Id);
            }

            _logger.LogInformation("Adding administrator");

            var entity = Mapper.MapAdmins(admin);

            entity.Id = 0;
            await _dbContext.Admins.AddAsync(entity);
            return admin;
        }

        /// <summary>
        /// Update an admin's info.
        /// </summary>
        /// <param name="admin">The administrator with changed values</param>
        public async Task<Domain.Models.Admins> UpdateAdminAsync(int adminId, Domain.Models.Admins admin)
        {
            _logger.LogInformation("Updating administrator with ID {adminId}", adminId);

            DataAccess.Entities.Admins currentEntity = await _dbContext.Admins.FindAsync(adminId);
            var newEntity = Mapper.MapAdmins(admin);
            newEntity.Id = adminId;

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            return admin;
        }

        /// <summary>
        /// Delete a administrator by ID.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        public async void DeleteAdminAsync(int adminId)
        {
            _logger.LogInformation("Deleting administrator with ID {adminId}", adminId);
            DataAccess.Entities.Admins entity = await _dbContext.Admins.FindAsync(adminId);
            _dbContext.Admins.Remove(entity);
        }

        /// <summary>
        /// Get all administrators with deferred execution.
        /// </summary>
        /// <returns>The collection of administrators</returns>
        public async Task<IEnumerable<Domain.Models.Admins>> GetAdminsAsync(string search = null)
        {
            IQueryable<DataAccess.Entities.Admins> items =  _dbContext.Admins
                .AsNoTracking();

            var list = await items.ToListAsync();

            if(search != null)
            {
                list.Where(a => a.LastName.Contains(search));
            }

            return list.Select(Mapper.MapAdmins);
        }

        /// <summary>
        /// Get a administrator by ID.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        public async Task<Domain.Models.Admins> GetAdminByIdAsync(int adminId)
        {
            DataAccess.Entities.Admins item = await _dbContext.Admins
                .FirstOrDefaultAsync(a => a.Id == adminId);

            return Mapper.MapAdmins(item);
        }

        /// <summary>
        /// Get a administrator by their login email and password.
        /// </summary>
        /// <returns>The administrator</returns>
        public async Task<Domain.Models.Admins> GetAdminByLoginAsync(string email, string password)
        {
            DataAccess.Entities.Admins item = await _dbContext.Admins
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);

            return Mapper.MapAdmins(item);
        }

        /// <summary>
        /// Get an admin by their login email.
        /// </summary>
        /// <returns>The admin</returns>
        public async Task<Domain.Models.Admins> GetAdminByEmailAsync(string email)
        {
            DataAccess.Entities.Admins item = await _dbContext.Admins
                .FirstOrDefaultAsync(a => a.Email == email);

            //if (item is null)
            //{
            //    throw new ArgumentException("User does not exist");
            //}

            return Mapper.MapAdmins(item);
        }

        /// <summary>
        /// Get all tickets with deferred execution.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        public async Task<IEnumerable<Domain.Models.Tickets>> GetTicketsAsync(string search = null)
        {
            IQueryable<DataAccess.Entities.Tickets> items = _dbContext.Tickets
                //.Include(c => c.Comments)
                .AsNoTracking();

            var list = await items.ToListAsync();

            if (search != null)
            {
                 list.Where(a => a.Title.Contains(search) || a.Details.Contains(search));
            }

            return list.Select(Mapper.MapTickets);
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
        public async Task<bool> SaveAsync()
        {
            _logger.LogInformation("Saving changes to the database");
            int written = await _dbContext.SaveChangesAsync();
            return written > 0;
        }

    }
}
