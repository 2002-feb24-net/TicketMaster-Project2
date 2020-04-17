using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccess.Interfaces;

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




    }
}
