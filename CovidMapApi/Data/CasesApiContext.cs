using CovidMapApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMapApi.Data
{
    public class CasesApiContext:DbContext
    {
        public CasesApiContext(DbContextOptions<CasesApiContext> options)
            :base(options)
        {

        }

        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<VirusCase> VirusCases { get; set; }

    }
}
