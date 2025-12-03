using GymmanagmentDAL.Entities;
using GymmanagmentDAL.Entities.Data.Contexts;
using GymmanagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagmentDAL.Repositories.Classes
{
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;
        public PlanRepository(GymDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Plan> GetAll()=>_dbContext.Plan.ToList();


        public Plan? GetById(int id)=> _dbContext.Plan.Find(id);

        public int Update(Plan Plan)
        {
            _dbContext.Plan.Update(Plan);
            return _dbContext.SaveChanges();
        }
    }
}
