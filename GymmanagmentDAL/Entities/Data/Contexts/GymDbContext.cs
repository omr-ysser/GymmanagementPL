using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagmentDAL.Entities.Data.Contexts
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options):base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=GymManagemnet;Trusted_Connection=True;TrustServerCertificate=True;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        #region DbSets
        public DbSet<Member> Member { get; set; } = null!;
        public DbSet<Trainer> Trainer { get; set; } = null!;
        public DbSet<Plan> Plan { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Sessions> Sessions { get; set; } = null!;
        public DbSet<MemberShip> MemberShip { get; set; } = null!;
        public DbSet<MemberSession> MemberSession { get; set; } = null!;
        public DbSet<HealthRecord> HealthRecord { get; set; } = null!;
        #endregion
    }
}
