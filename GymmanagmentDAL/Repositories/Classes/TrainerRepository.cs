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
    internal class TrainerRepository :GenericRepository<Trainer>,ITrainerRepository
    {
        private readonly GymDbContext _dbContext;
        public TrainerRepository(GymDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int Test(int id)
        {
            throw new NotImplementedException();
        }
    }
}
