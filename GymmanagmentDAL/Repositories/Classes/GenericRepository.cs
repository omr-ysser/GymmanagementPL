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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext _dbContext;
        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(TEntity entity)
        {
           _dbContext.Set<TEntity>().Add(entity);
              return _dbContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()=>_dbContext.Set<TEntity>().ToList();


        public TEntity? GetById(int id)=> _dbContext.Set<TEntity>().Find(id);


        public int Update(TEntity entity)
        {
           _dbContext.Set<TEntity>().Update(entity);
              return _dbContext.SaveChanges();
        }
    }
}
