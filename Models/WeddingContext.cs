using System;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }

        public DbSet<RegisterUser> User { get; set; }

        public DbSet<Wedding> Wedding { get; set; }

        public DbSet<UserWedding> UserWedding { get; set; }
    }

}