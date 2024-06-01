using System;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using api.Models;
using System.Reflection.Emit;

namespace api.Data
{
	public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Biodata> Biodatas { get; set; }
        public DbSet<SidikJari> SidikJaries { get; set; }
    }
}

