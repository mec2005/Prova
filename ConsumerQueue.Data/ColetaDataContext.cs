using ConsumerQueue.Domain.Interfaces;
using ConsumerQueue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ConsumerQueue.Data
{
    public class ColetaDataContext : DbContext, IUnitOfWork
    {
        protected readonly string _cnn;
        public DbSet<QueueEntry> QueueEntry { get; set; }
        public IConfiguration Configuration { get; set; }

        public ColetaDataContext(string cnn)
        {
            _cnn = cnn;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
                        
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString(_cnn));            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QueueEntry>().HasKey(c => c.Id);
        }

        public void Save()
        {
            base.SaveChanges();
        }
    }
}
