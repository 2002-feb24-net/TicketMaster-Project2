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
        IEnumerable<Users> GetUsers(string search = null);

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <returns>The user</returns>
        Users GetUserById(int userId);

        /// <summary>
        /// Add a useer.
        /// </summary>
        /// <param name="user">The user</param>
        void AddUser(Users user);

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="user">The user with changed values</param>
        void UpdateUser(Users user);

        /// <summary>
        /// Delete a user by ID. 
        /// </summary>
        /// <param int="userId">The ID of the user</param>
        void DeleteUser(int userId);

        /// <summary>
        /// Get a user by their login email and password.
        /// </summary>
        /// <returns>The user</returns>
        Users GetUserByLogin(string email, string password);

        /// <summary>
        /// Add an administrator.
        /// </summary>
        /// <param name="admin">The administrator</param>
        void AddAdmin(Admins admin);

        /// <summary>
        /// Update an admin's info.
        /// </summary>
        /// <param name="admin">The administrator with changed values</param>
        void UpdateAdmin(Admins admin);

        /// <summary>
        /// Delete a administrator by ID.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        void DeleteAdmin(int adminId);

        /// <summary>
        /// Get all administrators with deferred execution.
        /// </summary>
        /// <returns>The collection of administrators</returns>
        IEnumerable<Admins> GetAdmins(string search = null);

        /// <summary>
        /// Get a administrator by ID.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        Admins GetAdminById(int adminId);

        /// <summary>
        /// Get a administrator by their login email and password.
        /// </summary>
        /// <returns>The administrator</returns>
        Admins GetAdminByLogin(string email, string password);

        /// <summary>
        /// Get all tickets with deferred execution.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        IEnumerable<Tickets> GetTickets(string search = null);

        /// <summary>
        /// Get a ticket by ID.
        /// </summary>
        /// <returns>The ticket</returns>
        Tickets GetTicketById(int ticketId);

        /// <summary>
        /// Add a ticket and associate it with a user.
        /// </summary>
        /// <param name="ticket">The ticket</param>
        void AddTicket(Tickets ticket);

        /// <summary>
        /// Update a ticket.
        /// </summary>
        /// <param name="ticket">The ticket with changed values</param>
        void UpdateTicket(Tickets ticket);

        /// <summary>
        /// Delete a ticket by ID.
        /// </summary>
        /// <param int="ticketId">The ID of the ticket</param>
        void DeleteTicket(int ticketId);

        /// <summary>
        /// Get all tickets according to user Id.
        /// </summary>
        /// <param int="userId">The ID of the user</param>
        /// <returns>The collection of tickets</returns>
        IEnumerable<Tickets> GetTicketsByUserId(int userId);

        /// <summary>
        /// Get all tickets according to administrator Id.
        /// </summary>
        /// <param int="adminId">The ID of the administrator</param>
        /// <returns>The collection of tickets</returns>
        IEnumerable<Tickets> GetTicketsByAdminId(int adminId);

        /// <summary>
        /// Get all tickets according to store Id.
        /// </summary>
        /// <param int="storeId">The ID of the store</param>
        /// <returns>The collection of tickets</returns>
        IEnumerable<Tickets> GetTicketsByStoreId(int storeId);

        /// <summary>
        /// Search all tickets by text string.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        IEnumerable<Tickets> GetTicketsByText(string search);

        /// <summary>
        /// Search all tickets by datetime.
        /// </summary>
        /// <returns>The collection of tickets</returns>
        //IEnumerable<Tickets> GetTicketsByDatetime(string datetime);

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
        void Save();
    }
}
