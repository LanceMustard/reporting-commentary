using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReportingCommentary.Models
{
    public class CommentaryContext : DbContext
    {
        public CommentaryContext(DbContextOptions<CommentaryContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<CBS> CBSs { get; set; }
        public DbSet<ReportingPeriod> ReportingPeriods { get; set; }
        public DbSet<ReportingItem> ReportingItems { get; set; }

    }
}
