using Btx.Client.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Btx.Client.Application.Persistance
{
    public class BtxDbContext : DbContext
    {
        private string _databaseFilePath;

        public DbSet<BtxMessage> BtxMessages { get; set; }

        public DbSet<BtxUser> BtxUsers { get; set; }

        public DbSet<BtxThread> BtxThreads { get; set; }
        
        public BtxDbContext()
        {
            _databaseFilePath = BtxSetting.DATABASE_FULL_FILE_PATH;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseSqlite($"Filename={_databaseFilePath}");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BtxThread>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<BtxMessage>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<BtxMessage>()
                .HasOne(a => a.Thread)
                .WithMany(a => a.Messages)
                .HasForeignKey(a => a.ThreadId);
            
            base.OnModelCreating(modelBuilder);
            
        }

        public static void InitDatabase()
        {
            if (!Directory.Exists(BtxSetting.DATA_FOLDER_PATH))
            {
                Directory.CreateDirectory(BtxSetting.DATA_FOLDER_PATH);
            }

            var context = new BtxDbContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }

        public void EnsureCreated()
        {
            this.Database.EnsureCreated();
            this.Database.Migrate();
        }
    }
}
