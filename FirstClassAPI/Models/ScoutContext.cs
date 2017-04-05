using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstClassAPI.Models
{
    public class ScoutContext : DbContext
    {
        public ScoutContext(DbContextOptions<ScoutContext> options)
            : base(options)
        {
        }

        public DbSet<Scout> Scouts { get; set; }

    }
}