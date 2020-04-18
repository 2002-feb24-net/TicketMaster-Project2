using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public static class Mapper
    {
        /// <summary>
        /// Maps an Entity Framework user DAO to a business model,
        /// </summary>
        /// <param name="u">The user DAO.</param>
        /// <returns>The user business model.</returns>
        public static Domain.Models.Users MapUsers(Entities.Users u)
        {
            return new Domain.Models.Users
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address,
                City = u.City,
                State = u.State,
                Email = u.Email,
                Password = u.Password,
                PhoneNumber = u.PhoneNumber
            };
        }

        /// <summary>
        /// Maps a user business model to a DAO for Entity Framework,
        /// </summary>
        /// <param name="u">The user business model.</param>
        /// <returns>The user DAO.</returns>
        public static Entities.Users MapUsers(Domain.Models.Users u)
        {
            return new Entities.Users
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address,
                City = u.City,
                State = u.State,
                Email = u.Email,
                Password = u.Password,
                PhoneNumber = u.PhoneNumber
            };
        }

        /// <summary>
        /// Maps an Entity Framework administrator DAO to a business model,
        /// </summary>
        /// <param name="admin">The admin DAO.</param>
        /// <returns>The administrator business model.</returns>
        public static Domain.Models.Admins MapAdmins(Entities.Admins admin)
        {
            return new Domain.Models.Admins
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Password = admin.Password,
                CurrentTicket = admin.CurrentTicket,
                SupportLevel = admin.SupportLevel
            };
        }

        /// <summary>
        /// Maps a administrator business model to a DAO for Entity Framework,
        /// </summary>
        /// <param name="admin">The admin business model.</param>
        /// <returns>The administrator DAO.</returns>
        public static Entities.Admins MapAdmins(Domain.Models.Admins admin)
        {
            return new Entities.Admins
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Password = admin.Password,
                CurrentTicket = admin.CurrentTicket,
                SupportLevel = admin.SupportLevel
            };
        }

        /// <summary>
        /// Maps an Entity Framework ticket DAO to a business model,
        /// </summary>
        /// <param name="t">The ticket DAO.</param>
        /// <returns>The ticket business model.</returns>
        public static Domain.Models.Tickets MapTickets(Entities.Tickets t)
        {
            return new Domain.Models.Tickets
            {
                Id = t.Id,
                Title = t.Title,
                Category = t.Category,
                DatetimeOpened = t.DatetimeOpened,
                DatetimeModified = t.DatetimeModified,
                DatetimeClosed = t.DatetimeClosed,
                Deadline = t.Deadline,
                Priority = t.Priority,
                Details = t.Details,
                UserId = t.UserId,
                AdminId = t.AdminId,
                StoreId = t.StoreId,
                Completed = t.Completed
            };
        }


        /// <summary>
        /// Maps a ticket business model to a DAO for Entity Framework,
        /// </summary>
        /// <param name="t">The ticket business model.</param>
        /// <returns>The ticket DAO.</returns>
        public static Entities.Tickets MapTickets(Domain.Models.Tickets t)
        {
            return new Entities.Tickets
            {
                Id = t.Id,
                Title = t.Title,
                Category = t.Category,
                DatetimeOpened = t.DatetimeOpened,
                DatetimeModified = t.DatetimeModified,
                DatetimeClosed = t.DatetimeClosed,
                Deadline = t.Deadline,
                Priority = t.Priority,
                Details = t.Details,
                UserId = t.UserId,
                AdminId = t.AdminId,
                StoreId = t.StoreId,
                Completed = t.Completed
            };
        }

        /// <summary>
        /// Maps an Entity Framework comment DAO to a business model,
        /// </summary>
        /// <param name="c">The comment DAO.</param>
        /// <returns>The comment business model.</returns>
        public static Domain.Models.Comments MapComments(Entities.Comments c)
        {
            return new Domain.Models.Comments
            {
                Id = c.Id,
                TicketId = c.TicketId,
                Comment = c.Comment,
                Datetime = c.Datetime,
                AdminId = c.AdminId
            };
        }

        /// <summary>
        /// Maps a comment business model to a DAO for Entity Framework,
        /// </summary>
        /// <param name="c">The comment business model.</param>
        /// <returns>The comment DAO.</returns>
        public static Entities.Comments MapComments(Domain.Models.Comments c)
        {
            return new Entities.Comments
            {
                Id = c.Id,
                TicketId = c.TicketId,
                Comment = c.Comment,
                Datetime = c.Datetime,
                AdminId = c.AdminId
            };
        }

    }
}
