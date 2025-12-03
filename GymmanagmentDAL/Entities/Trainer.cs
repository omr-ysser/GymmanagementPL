using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagmentDAL.Entities
{
    public class Trainer:GymUser
    {
        //HireDate=CreateAt of BaseEntity
        public Specialties Specialties { get; set; }
        public ICollection<Sessions> TrainerSessions { get; set; } = null!;
    }
}
