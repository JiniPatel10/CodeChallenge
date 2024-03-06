using Microsoft.EntityFrameworkCore;
using Rockfast.ApiDatabase.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockfast.ApiDatabase
{
    public class ApiDbContext : DbContext
    {
        #region Variables

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        #endregion
        #region Constructor

        public ApiDbContext(DbContextOptions options)
            :base(options)
        {
            this.Database.EnsureCreated();
        }
        #endregion
        #region methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<User>()
    .HasMany(u => u.Todos)
    .WithOne(x => x.User)
    .HasForeignKey(t => t.UserId);


        }
        #endregion
    }
}
