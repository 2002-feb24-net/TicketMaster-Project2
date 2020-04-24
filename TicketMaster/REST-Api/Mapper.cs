using REST_Api.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_Api
{

    public static class Mapper
    {
        /// <summary>
        /// Maps an Entity Framework user DAO to a business model,
        /// </summary>
        /// <param name="u">The user DAO.</param>
        /// <returns>The user business model.</returns>
        public static Domain.Models.Users MapUsers(Users u)
        {
            return u is null ? null : new Domain.Models.Users
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
        public static Users MapUsers(Domain.Models.Users u)
        {
            return u is null ? null : new Users
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
        public static Domain.Models.Admins MapAdmins(Admins admin)
        {
            return admin is null ? null : new Domain.Models.Admins
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
        public static Admins MapAdmins(Domain.Models.Admins admin)
        {
            return admin is null ? null : new Admins
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
        /// including all comments if present.
        /// </summary>
        /// <param name="t">The ticket DAO.</param>
        /// <returns>The ticket business model.</returns>
        public static Domain.Models.Tickets MapTickets(Tickets t)
        {
            return t is null ? null : new Domain.Models.Tickets
            {
                Id = t.Id,
                Title = t.Title,
                Category = t.Category,
                DatetimeOpened = t.DatetimeOpened,
                DatetimeModified = t.DatetimeModified,
                DatetimeClosed = t.DatetimeClosed,
                Priority = t.Priority,
                UserRequesterName = t.UserRequesterName,
                Details = t.Details,
                AdminAssignedName = t.AdminAssignedName,
                UserId = t.UserId,
                AdminId = t.AdminId,
                StoreId = t.StoreId,
                Completed = t.Completed,
                //Comments = t.Comments.Select(MapComments).ToList()
            };
        }


        /// <summary>
        /// Maps a ticket business model to a DAO for Entity Framework,
        /// including all comments if present.
        /// </summary>
        /// <param name="t">The ticket business model.</param>
        /// <returns>The ticket DAO.</returns>
        public static Tickets MapTickets(Domain.Models.Tickets t)
        {
            return t is null ? null : new Tickets
            {
                Id = t.Id,
                Title = t.Title,
                Category = t.Category,
                DatetimeOpened = t.DatetimeOpened,
                DatetimeModified = t.DatetimeModified,
                DatetimeClosed = t.DatetimeClosed,
                Priority = t.Priority,
                Details = t.Details,
                UserId = t.UserId,
                UserRequesterName = t.UserRequesterName,
                AdminId = t.AdminId,
                AdminAssignedName = t.AdminAssignedName,
                StoreId = t.StoreId,
                Completed = t.Completed,
                //Comments = t.Comments.Select(MapComments).ToList()
            };
        }

        ///// <summary>
        ///// Maps an Entity Framework comment DAO to a business model,
        ///// </summary>
        ///// <param name="c">The comment DAO.</param>
        ///// <returns>The comment business model.</returns>
        //public static Domain.Models.Comments MapComments(Comments c)
        //{
        //    return c is null ? null : new Domain.Models.Comments
        //    {
        //        Id = c.Id,
        //        TicketId = c.TicketId,
        //        Comment = c.Comment,
        //        Datetime = c.Datetime,
        //        AdminId = c.AdminId
        //    };
        //}

        ///// <summary>
        ///// Maps a comment business model to a DAO for Entity Framework,
        ///// </summary>
        ///// <param name="c">The comment business model.</param>
        ///// <returns>The comment DAO.</returns>
        //public static Comments MapComments(Domain.Models.Comments c)
        //{
        //    return c is null ? null : new Comments
        //    {
        //        Id = c.Id,
        //        TicketId = c.TicketId,
        //        Comment = c.Comment,
        //        Datetime = c.Datetime,
        //        AdminId = c.AdminId
        //    };
        //}

    }
}
