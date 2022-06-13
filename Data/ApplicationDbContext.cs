using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ProgramowanieZaawansowaneLicencje.Models;

namespace LicensesSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProgramowanieZaawansowaneLicencje.Models.Employee> Employee { get; set; }
        public DbSet<ProgramowanieZaawansowaneLicencje.Models.License> License { get; set; }
    }
}
