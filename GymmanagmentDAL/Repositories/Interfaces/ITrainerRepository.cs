using GymmanagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagmentDAL.Repositories.Interfaces
{
    internal interface ITrainerRepository:IGenericRepository<Trainer>
    {
        int Test(int id);
    }
}
