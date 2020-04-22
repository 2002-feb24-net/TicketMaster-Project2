using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// A repository managing data access for Users, Admins, Tickets, and Comment objects.
    /// </summary>
    public interface ITicketRepo
    {
        /// <summary>
        /// Get all users with deferred execution.
        /// </summary>
        /// <returns>The collection of users</returns>
        Task<IEnumerable<Users>> GetUsersAsync(string lastName = null);

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <returns>The user</returns>
        Task<Users> GetUserByIdAsync(int userId);

        /// <summary>
        /// Add a useer.
        /// </summary>
        /// <param name="user">The user</param>
        void AddUserAsync(Users user);

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="user">The user with changed values</param>
        Task<Domain.Models.Users> UpdateUserAsync(int id, Users user);

        /// <summary>
        /// Delete a user by ID. 
        /// </summary>
        /// <param int="userId">The ID of the user</param>
        void DeleteUserAsync(int userId);

        /// <summary>
        /// Get a user by their login email and password.
        /// </summary>
        /// <returns>The user</returns>
        Task<Users> GetUserByLoginAsync(string email, string password);

        /// <summary>
        /// Get a user by their login email and password.
        /// </summary>
        /// <returns>The user</returns>
        Task<Domain.Models.Users> GetUserByEmailAsync(string email);

        /// <summary>
        /// Add an administrator.
        /// </summary>
        /// <param name="admin">The administrator</param>
        Task<Admins> AddAdminAsync(Admins admin);

        /// <summary>
        /// Update an admin's info.
        /// </summary>
        /// <param name="admin">The administrator with changed values</param>
        Task<Admins> UpdateAdminAsync(int adminId, Admins admin);

        /// <summary>
        /// Delete a administrator by ID.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        void DeleteAdminAsync(int adminId);

        /// <summary>
        /// Get all administrators with deferred execution.
        /// </summary>
        /// <returns>The collection of administrators</returns>
        Task<IEnumerable<Admins>> GetAdminsAsync(string lastName = null);

        /// <summary>
        /// Get a administrator by ID.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        Task<Admins> GetAdminByIdAsync(int adminId);

        /// <summary>
        /// Get a administrator by their login email and password.
        /// </summary>
        /// <returns>The administrator</returns>
        Task<Admins> GetAdminByLoginAsync(string email, string password);

        /// <summary>
        /// Get an admin by their login email.
        /// </summary>
        /// <returns>The admin</returns>
        Task<Domain.Models.Admins> GetAdminByEmailAsync(string email);

        /// <summary>
        /// Get all tickets with deferred execution.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        Task<IEnumerable<Tickets>> GetTicketsAsync(string search = null);

        /// <summary>
        /// Get a ticket by id.
        /// </summary>
        /// <param int="id">The id of the user</param>
        /// <returns>The tickets</returns>
        Task<Tickets> GetTicketByIdAsync(int id);

        /// <summary>
        /// Get a ticket by user ID.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        Task<IEnumerable<Tickets>> GetTicketsByUserAsync(int userId);

        /// <summary>
        /// Add a ticket and associate it with a user.
        /// </summary>
        /// <param name="ticket">The ticket</param>
        void AddTicketAsync(Tickets ticket);

        /// <summary>
        /// Update a ticket.
        /// </summary>
        /// <param name="ticket">The ticket with changed values</param>
        void UpdateTicketAsync(int id, Tickets ticket);

        /// <summary>
        /// Close an open ticket.
        /// </summary>
        /// <param int="id">The id of the ticket to close</param>
        void CloseTicketAsync(int id);

        /// <summary>
        /// Delete a ticket by ID.
        /// </summary>
        /// <param int="ticketId">The ID of the ticket</param>
        void DeleteTicketAsync(int ticketId);

        /// <summary>
        /// Get all tickets according to administrator Id.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        /// <returns>The collection of tickets</returns>
        Task<IEnumerable<Tickets>> GetTicketsByAdminAsync(int adminId);

        /// <summary>
        /// Get all tickets according to store Id.
        /// </summary>
        /// <param int="storeId">The ID of the store</param>
        /// <returns>The collection of tickets</returns>
        Task<IEnumerable<Tickets>> GetTicketsByStoreAsync(int storeId);

        /// <summary>
        /// Get the last ticket created.
        /// </summary>
        /// <returns>The ticket</returns>
        Task<Tickets> GetLatestTicketAsync();

        /// <summary>
        /// Search all tickets by datetime.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        //IEnumerable<Tickets> GetTicketsByDatetime(string datetime);

        /// <summary>
        /// Get a store by id.
        /// </summary>
        /// <param int="storeId">The ID of the store</param>
        /// <returns>The store</returns>
        Task<Stores> GetStoreByIdAsync(int storeId);

        /// <summary>
        /// Add a comment to an existing ticket, and associate it with a administrator.
        /// </summary>
        /// <param name="comment">The comment</param>
        /// <param int="ticketId">The if of the ticket</param>
        void AddComment(Comments comment, Tickets ticket);

        /// <summary>
        /// Delete a comment by ID.
        /// </summary>
        /// <param int="commentId">The ID of the comment</param>
        void DeleteComment(int commentId);

        /// <summary>
        /// Get all comments according to ticket Id.
        /// </summary>
        /// <param int="ticketId">The ID of the ticket</param>
        /// <returns>The collection of comments</returns>
        IEnumerable<Comments> GetCommentsByTicketId(int ticketId);

        /// <summary>
        /// Get all comments according to administrator Id.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        /// <returns>The collection of comments</returns>
        IEnumerable<Comments> GetCommentsByAdminId(int adminId);

        /// <summary>
        /// Persist changes to the data source.
        /// </summary>
        Task<bool> SaveAsync();
    }
}
