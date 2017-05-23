using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheWorld.Models
{
    public class WordContext : DbContext
    {
        private IConfigurationRoot _config;

        public WordContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Trip> Stops { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);

            optionBuilder.UseSqlServer(_config["ConnStrings:TheWorld"]);
        }
    }
}
