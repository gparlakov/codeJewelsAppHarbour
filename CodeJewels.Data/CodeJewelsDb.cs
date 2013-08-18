using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using CodeJewels.Models;

namespace CodeJewels.Data
{
    public class CodeJewelsDb : DbContext
    {
        public CodeJewelsDb()
            :base("CodeJewelsDb")
        {
            InitializeCloudConnectionString();
        }
  
        private void InitializeCloudConnectionString()
        {
            var connectionString = ConfigurationManager.AppSettings["CodeJewelsConnectionString"];
            if (connectionString != null)
            {
                this.Database.Connection.ConnectionString = connectionString;
            }
        }

        public DbSet<CodeJewel> CodeJewels { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeJewel>().HasKey(c => c.Id);
            modelBuilder.Entity<CodeJewel>().Property(c => c.AuthorEMail).IsRequired().HasMaxLength(250).IsVariableLength();
            modelBuilder.Entity<CodeJewel>().Property(c => c.Code).IsRequired().HasMaxLength(500).IsVariableLength();

            modelBuilder.Entity<Vote>().HasKey(v => v.Id).Property(v => v.IsUpVote).IsRequired();
            modelBuilder.Entity<Vote>().Property(v => v.Author).IsRequired().HasMaxLength(250).IsVariableLength();

            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
