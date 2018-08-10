using FileCabinet.Dal.Contracts.Domain;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FileCabinet.Dal.EF
{
    public class FileCabinetDbContext : DbContext
    {
        public FileCabinetDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new FileCabinetContextInitializer());
        }

        public virtual DbSet<File> Files { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Conventions
                .Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder
                .Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));

            modelBuilder
                .Entity<User>()
                .HasMany(prop => prop.Bookmarks)
                .WithMany(prop => prop.BookmarkedUsers);

            base.OnModelCreating(modelBuilder);
        }
    }
}