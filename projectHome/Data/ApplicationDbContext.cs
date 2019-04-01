using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mor1.Models.ReqQuoteModels;

namespace mor1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<mor1.Models.ReqQuoteModels.ReqQuoteForm> ReqQuoteForm { get; set; }
    }
}
