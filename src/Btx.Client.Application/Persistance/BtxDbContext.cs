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

        public BtxDbContext()
        {
            _databaseFilePath = BtxSetting.DATABASE_FULL_FILE_PATH;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Filename={_databaseFilePath}");
        }

        public static void InitDatabase()
        {
            var context = new BtxDbContext();

            if (!Directory.Exists(BtxSetting.DATA_FOLDER_PATH))
            {
                Directory.CreateDirectory(BtxSetting.DATA_FOLDER_PATH);
            }

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
